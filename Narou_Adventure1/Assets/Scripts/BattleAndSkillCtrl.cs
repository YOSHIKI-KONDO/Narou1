using System;
using System.Collections.Generic;
using UnityEngine;
using static UsefulMethod;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// slotKinds  　　...２次元配列で、現在のスロットに格納されているスキルを格納している。
/// skills     　　...SKILLの配列。長さは種類の数。
/// dungeons   　　...DUNGEONの配列。長さは種類の数。
/// enemys     　　...ENEMYの配列。長さは種類の数。
/// currentEnemys ...現在出現している敵の配列。
/// enemysCmps 　　...ゲーム上に存在するSliderなどのオブジェクトをまとめた配列。
///                  currentEnemysと同期させている。
/// dunKind       ...現在のダンジョンの種類。
/// skillKind     ...現在選択しているスキルの種類。
/// </summary>
public class BattleAndSkillCtrl : BASE {
    /// <summary>
    /// [0,0], [1,0], [2,0], [3,0],
    /// [0,1], [1,1], [2,1], [3,1],
    /// [0,2], [1,2], [2,2], [3,2],
    /// </summary>
    public readonly int ROW_SLOT = 4;
    public readonly int COLUMN_SLOT = 3;
    public SkillKind[,] slotKinds;
    public SKILL[] skills;// = new SKILL[Enum.GetNames(typeof(SkillKind)).Length];
    [SerializeField]SkillSlot[] slots_ins;//InspectorからAddする
    [SerializeField] SkillSlot[] slots_menu;//menuに常に出ているスロット
    SkillSlot[,] slots;
    public Toggle skillActiveToggle;
    public Toggle skillDungeonToggle;
    int currentRow;

    public DungeonKind dunKind;
    public SkillKind skillKind;


    /* battle */
    public DUNGEON[] dungeons;// = new DUNGEON[Enum.GetNames(typeof(DungeonKind)).Length];
    public ENEMY[] enemys = new ENEMY[Enum.GetNames(typeof(EnemyKind)).Length];
    public List<InnerEnemy> currentEnemys = new List<InnerEnemy>();
    public BattleComponents heroCmp;                   //Object
    public BattleComponents[] enemysCmps;              //Object
    public BattleComponents[] allysCmps;               //Object
    public SlideSetActive battleScreen;
    float Interval_normalAttack = 3.0f;               //通常攻撃のインターバル
    float currentInterval_normalAttack;               //通常攻撃のインターバルのたまり具合
    //double attack_normalAttack = 1d;                  //通常攻撃の攻撃力
    int targetEnemy_index;
    WaitForNum waitFloor = new WaitForNum();
    public Toggle loopToggle;
    public ToggleGroup targetToggleGroup;


    /* UI */
    public Text dungenNameText;
    public Text floorText;
    public Slider floorSlider;
    public Button fleeButton;
    /******/


    public double DamageCalculate(double skill_atk, double atk, double criticalFactor)
    {
        //double dmg = atk * (1d + skill_atk / 10) * criticalFactor;
        //int randomRange = (int)((1d / 2d) * Math.Sqrt(dmg));
        //double randomFactor = UnityEngine.Random.Range( -randomRange, randomRange + 1);
        //return dmg + randomFactor;
        double dmg = atk * (1d + skill_atk / 30) * criticalFactor; //10 => 30に変更
        return dmg;
    }

    // Use this for initialization
    void Awake () {
        StartBASE();
        /* コンストラクタで配列を初期化しても反映されない */
        skills = new SKILL[main.SD.num_skill];
        dungeons = new DUNGEON[main.SD.num_dungeon];
        enemys = new ENEMY[Enum.GetNames(typeof(EnemyKind)).Length];

        InitializeSlots();
        fleeButton.onClick.AddListener(FleeFromDungeon);
	}

	// Use this for initialization
	void Start () {
        Load();
        TestSkills();

        TestEnemys();
        TestDungeons();
    }
	
	// Update is called once per frame
	void Update () {
        CheckOnlyAction();

        ApplySkills();
        ApplySkillSlots();

        ControllBattle();
        ApplyComponents(); // Componentを更新
    }

    private void FixedUpdate()
    {
        CheckFault();

        PlaySkills();
        PlayNpcSkills();
        NormalAttack();
        SkillLevelControll();

        EnemysAction();
        JudgeCombo();

        CheckFault(); //ここにも追加
    }

    //Called in Fixed Update
    void PlaySkills()
    {
        if (skillActiveToggle.isOn == false) { return; }
        if (skillDungeonToggle.isOn && dunKind == DungeonKind.nothing) { return; }
        for (int i_r = 0; i_r < ROW_SLOT; i_r++)
        {
            if(currentRow != i_r) { continue; }//現在の行の時のみ実行する
            bool goNextRow = true;
            for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
            {
                SkillKind thisKind = slotKinds[i_r, i_c];//何回も使うので変数に代入
                if (thisKind == SkillKind.nothing) { continue; }
                if (skills[(int)thisKind].casted) { continue; }//使用されていたらパス

                goNextRow = false;
                //コストがあれば支払う
                if (skills[(int)thisKind].PayedCost == false)
                {
                    if (skills[(int)thisKind].CanUse())
                    {
                        skills[(int)thisKind].PayCost();
                    }
                    else
                    {
                        continue;
                    }
                }

                
                skills[(int)thisKind].currentValue += 0.1;//0.1秒に0.1ずつ増える
                if (skills[(int)thisKind].currentValue >= skills[(int)thisKind].Duration())
                {
                    //生産系スキル
                    skills[(int)thisKind].Produce();
                    skills[(int)thisKind].CastedAction();

                    if (dunKind != DungeonKind.nothing)
                    {
                        //会心攻撃かどうか
                        double criticalFactor = 1f;
                        string criticalSentense = "";
                        if(UnityEngine.Random.Range(0f,100f) < main.status.CriticalChance)
                        {
                            criticalFactor *= main.status.CriticalFactor;
                            criticalSentense = " (Critical)";
                        }
                        //剣士系の攻撃
                        if (skills[(int)thisKind].warriorAtks.Count > 0)
                        {
                            //AttackToEnemys(thisKind, skills[(int)thisKind].WarriorDamage() * main.status.Attack * criticalFactor, "You", criticalSentense);
                            AttackToEnemys(thisKind,DamageCalculate(skills[(int)thisKind].WarriorDamage(), main.status.Attack, criticalFactor), "You", criticalSentense);
                        }
                        //魔法系の攻撃
                        if (skills[(int)thisKind].sorcererAtks.Count > 0)
                        {
                            //AttackToEnemys(thisKind, skills[(int)thisKind].SorcererDamage() * main.status.MagicAttack * criticalFactor, "You", criticalSentense);
                            AttackToEnemys(thisKind,DamageCalculate(skills[(int)thisKind].SorcererDamage(), main.status.MagicAttack, criticalFactor), "You", criticalSentense);
                        }
                    }

                    skills[(int)thisKind].casted = true;
                    skills[(int)thisKind].PayedCost = false;
                    skills[(int)thisKind].currentValue = 0;
                }
            }//一行終わり

            if (goNextRow)
            {
                currentRow++;
                if(currentRow >= ROW_SLOT)
                {
                    currentRow = 0;
                    //castedを全てfalseにする
                    foreach (var skill in skills)
                    {
                        if(skill == null) { continue; }
                        skill.casted = false;
                    }
                }
            }
        }
    }

    //味方の攻撃
    void PlayNpcSkills()
    {
        for (int i = 0; i < main.SD.num_ally; i++)
        {
            if (i == 0) { continue; }
            if (main.npcSkillCtrl.npcs[i] == null) { continue; }
            if (main.npcSkillCtrl.canFightAllys.Contains((AllyKind)i) == false) { continue; }
            if (main.npcSkillCtrl.npcs[i].isDead) { continue; }
            SKILL thisSkill = main.npcSkillCtrl.npcs[i].Cast();
            if (thisSkill != null && dunKind != DungeonKind.nothing)
            {
                //会心攻撃かどうか
                double criticalFactor = 1f;
                string criticalSentense = "";
                if (UnityEngine.Random.Range(0f, 100f) < main.npcSkillCtrl.npcs[i].CriticalChance)
                {
                    criticalFactor *= main.npcSkillCtrl.npcs[i].CriticalFactor;
                    criticalSentense = " (Critical)";
                }
                if (thisSkill.warriorAtks.Count > 0)
                {
                    AttackToEnemys(thisSkill.kind, DamageCalculate(thisSkill.WarriorDamage(), main.npcSkillCtrl.npcs[i].Attack, criticalFactor)
                        , main.enumCtrl.allys[i].Name(), criticalSentense);
                }
                if (thisSkill.sorcererAtks.Count > 0)
                {
                    AttackToEnemys(thisSkill.kind, DamageCalculate(thisSkill.SorcererDamage(), main.npcSkillCtrl.npcs[i].MagicAttack, criticalFactor)
                        , main.enumCtrl.allys[i].Name(), criticalSentense);
                }
                thisSkill.Produce();
                main.announce_d.Add(main.enumCtrl.allys[i].Name() + " used " + main.enumCtrl.skills[(int)thisSkill.kind].Name());
            }
        }
    }

    //主人公の通常攻撃
    //Called in FixedUpdate
    void NormalAttack()
    {
        if(dunKind == DungeonKind.nothing) { return; }
        currentInterval_normalAttack += 0.1f; //0.1廟に0.1ずつ増える
        if (currentInterval_normalAttack >= Interval_normalAttack)
        {
            currentInterval_normalAttack = 0;
            double criticalFactor = 1f;
            string criticalSentense = "";
            if (UnityEngine.Random.Range(0f, 100f) < main.status.CriticalChance)
            {
                criticalFactor *= main.status.CriticalFactor;
                criticalSentense = " (Critical)";
            }
            //AttackToEnemys(SkillKind.normalAttack, main.status.Attack * criticalFactor, "You", criticalSentense);
            AttackToEnemys(SkillKind.normalAttack, DamageCalculate(skills[(int)SkillKind.normalAttack].WarriorDamage(), main.status.Attack, criticalFactor), "You", criticalSentense);
        }
    }

    //コンボの判定
    void JudgeCombo()
    {
        for (int i_r = 1; i_r < ROW_SLOT; i_r++)
        {
            for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
            {
                //明らかにfalseなものは先に判定してfalseを入れた後continue
                if (slotKinds[i_r, i_c] == SkillKind.nothing)
                {
                    //skills[(int)slotKinds[i_r, i_c]].isCombo = false;
                    continue;
                }
                if(slotKinds[i_r - 1, i_c] == SkillKind.nothing)
                {
                    skills[(int)slotKinds[i_r, i_c]].isCombo = false;
                    continue;
                }
                if (skills[(int)slotKinds[i_r, i_c]].combo == null)
                {
                    skills[(int)slotKinds[i_r, i_c]].isCombo = false;
                    continue;
                }
                if (skills[(int)slotKinds[i_r - 1, i_c]].sources.Count == 0)
                {
                    skills[(int)slotKinds[i_r, i_c]].isCombo = false;
                    continue;
                }

                //判定
                if (skills[(int)slotKinds[i_r - 1, i_c]].sources.Contains(skills[(int)slotKinds[i_r, i_c]].combo.kind))
                {
                    skills[(int)slotKinds[i_r, i_c]].isCombo = true;
                }
                else
                {
                    skills[(int)slotKinds[i_r, i_c]].isCombo = false;
                }
            }
        }

        //装備されていなければ問答無用でfalse
        for (int i = 1; i < skills.Length; i++)
        {
            if(skills[i].equipped == false)
            {
                skills[i].isCombo = false;
            }
        }
    }

    //スキル一覧の更新
    void ApplySkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (i == 0) { continue; }
            skills[i].equipped = false;//直後のApplySkillSlotsでtrueにしている
        }
    }

    //スキルスロットの更新
    void ApplySkillSlots()
    {
        for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
        {
            for (int i_r = 0; i_r < ROW_SLOT; i_r++)
            {
                if (slotKinds[i_r, i_c] == SkillKind.nothing)
                {
                    slots[i_r, i_c].text.text = main.enumCtrl.skills[(int)slotKinds[i_r, i_c]].Name();
                    slots[i_r, i_c].slider.value = 0f;
                }
                else
                {
                    slots[i_r, i_c].text.text = main.enumCtrl.skills[(int)slotKinds[i_r, i_c]].Name();
                    slots[i_r, i_c].slider.value = skills[(int)slotKinds[i_r, i_c]].sliderValue();

                    //スキルが設置してあったら、equipedをtrueにする
                    skills[(int)slotKinds[i_r, i_c]].equipped = true;
                }
                slots[i_r, i_c].kind = slotKinds[i_r, i_c]; //kind設定
            }
        }

        //menu
        for (int i = 0; i < slots_menu.Length; i++)
        {
            slots_menu[i].CopyValue(slots_ins[i]);
        }
    }

    void InitializeSlots()
    {
        slotKinds = new SkillKind[ROW_SLOT, COLUMN_SLOT];
        slots = new SkillSlot[ROW_SLOT, COLUMN_SLOT];
        int row = 0;
        int column = 0;
        foreach (var slot_ins in slots_ins)
        {
            slot_ins.CanClick = true;
            slots[row, column] = slot_ins;
            int row_temp = row;
            int column_temp = column;
            slots[row, column].button.onClick.AddListener(() => SetSkill(row_temp, column_temp));
            row++;
            if(row >= ROW_SLOT)
            {
                row = 0;
                column++;
            }
        }
    }

    void SetSkill(int row, int column)
    {
        slotKinds[row, column] = skillKind;
        if(skillKind != SkillKind.nothing)
        {
            skills[(int)skillKind].currentValue = 0;
            skills[(int)skillKind].casted = false;
            skills[(int)skillKind].PayedCost = false;
        }

        skillKind = SkillKind.nothing;
    }

    void TestSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (i == 0) { continue; }
            if (skills[i] == null) { Debug.Log((SkillKind)i + " がスキル一覧に設置されていません。"); }
        }
    }

    void SkillLevelControll()
    {
        main.SR.level_Skill[(int)SkillKind.normalAttack] = 0;
        main.SR.level_Skill[(int)SkillKind.normalAttack_npc1] = 0;
    }

    void Load()
    {
        for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
        {
            for (int i_r = 0; i_r < ROW_SLOT; i_r++)
            {
                slotKinds[i_r, i_c] = main.SR.slotKinds[ROW_SLOT * i_c + i_r];
            }
        }

        //loop toggle
        loopToggle.isOn = main.SR.isOn_loopToggle;
        skillActiveToggle.isOn = main.SR.isOn_activeSkillToggle;
        skillDungeonToggle.isOn = main.SR.isOn_dungeonOnlyToggle;
    }

    //saveCtrlから呼ぶ
    public void Save()
    {
        for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
        {
            for (int i_r = 0; i_r < ROW_SLOT; i_r++)
            {
                main.SR.slotKinds[ROW_SLOT * i_c + i_r] = slotKinds[i_r, i_c];
            }
        }

        //loop toggle
        main.SR.isOn_loopToggle = loopToggle.isOn;
        main.SR.isOn_activeSkillToggle = skillActiveToggle.isOn;
        main.SR.isOn_dungeonOnlyToggle = skillDungeonToggle.isOn;
    }

    /* Enemys */
    //Called in Update
    void ControllBattle()
    {
        if (dunKind == DungeonKind.nothing)
        {
            battleScreen.Left();
            return;
        }
        else
        {
            battleScreen.ResetPosition();  //戦闘中ならアクティブ
            //UI
            dungenNameText.text = main.enumCtrl.dungeons[(int)dunKind].Name();
            floorText.text = (dungeons[(int)dunKind].currentFloor + 1).ToString() + " / " + dungeons[(int)dunKind].MaxFloor().ToString();
            floorSlider.value = (((float)dungeons[(int)dunKind].currentFloor + 1f) / (float)dungeons[(int)dunKind].MaxFloor());
        }

        // 生死判定 => もしも死んでいたらListから削除
        for (int i = 0; i < currentEnemys.Count; i++)
        {
            currentEnemys[i].ApplyAlive();
            if(currentEnemys[i].alive == false)
            {
                //報酬(敵)
                for (int i_d = 0; i_d < currentEnemys[i].drops.Count; i_d++)
                {
                    //乱数を生成してヒットしたらドロップ
                    if(UnityEngine.Random.Range(0f,100f) < currentEnemys[i].drops[i_d].probability)
                    {
                        if (i_d == 0)
                        {
                            //味方がいるときは経験値を分ける
                            Drop exp_drop = new Drop(currentEnemys[i].drops[0].kind, currentEnemys[i].drops[0].amount, currentEnemys[i].drops[0].probability);//経験値のdropを複製
                            //currentEnemys[i].drops[0].amount /= (main.npcSkillCtrl.allyKinds.Count + 1);
                            exp_drop.amount /= (main.npcSkillCtrl.allyKinds.Count + 1);
                            foreach (var npc_kind in main.npcSkillCtrl.allyKinds)
                            {
                                //経験値を足す
                                main.npcSkillCtrl.npcs[(int)npc_kind].currentExp(main.npcSkillCtrl.npcs[(int)npc_kind].currentExp()
                                    + exp_drop.amount);
                                //Announce
                                main.announce_d.Add("LOOT [" + main.enumCtrl.enemys[(int)currentEnemys[i].kind].Name() + "] : "
                                    + main.enumCtrl.allys[(int)npc_kind].Name() + " gained "
                                    + main.enumCtrl.resources[(int)exp_drop.kind].Name() + " + "
                                    + tDigit(exp_drop.amount));
                            }
                            //自分(普通のドロップの書き方と同じ)
                            GetDrops(currentEnemys[i], exp_drop);

                            continue; //※
                        }
                        else
                        {
                            GetDrops(currentEnemys[i], currentEnemys[i].drops[i_d]);
                        }
                    }
                }
                currentEnemys.Remove(currentEnemys[i]);
                if (currentEnemys.Count > 0)
                {   //次のターゲット
                    enemysCmps[currentEnemys[0].indexInFloor].targetToggle.isOn = true;
                }
}
        }

        //報酬を入手する関数
        void GetDrops(InnerEnemy innerEnemy, Drop drop)
        {
            switch (drop.dropKind)
            {
                case Drop.DropKind.normal://普通の報酬
                    GetResourceDrops(innerEnemy, drop);
                    break;

                case Drop.DropKind.oneShot://一撃の報酬
                    if (innerEnemy.match_oneShot == false) { break; }
                    GetResourceDrops(innerEnemy, drop);

                    break;
                case Drop.DropKind.skill_and://特定のスキルで倒された場合の報酬(AND)
                    if (innerEnemy.match_skill_AND == false) { break; }
                    GetResourceDrops(innerEnemy, drop);
                    break;

                case Drop.DropKind.skill_or://特定のスキルで倒された場合の報酬(OR)
                    if (innerEnemy.match_skill_OR == false) { break; }
                    GetResourceDrops(innerEnemy, drop);
                    break;

                case Drop.DropKind.attribute_and://属性の報酬(AND)
                    if (innerEnemy.match_atr_AND == false) { break; }
                    GetResourceDrops(innerEnemy, drop);
                    break;

                case Drop.DropKind.attribute_or://属性の報酬(OR)
                    if (innerEnemy.match_atr_OR == false) { break; }
                    GetResourceDrops(innerEnemy, drop);
                    break;

                default:
                    break;
            }
        }

        //リソースの報酬を入手する関数
        void GetResourceDrops(InnerEnemy innerEnemy, Drop drop)
        {
            if (drop is Item_Drop)
            {
                bool couldGet = main.itemCtrl.Drop_Inventory(((drop as Item_Drop).itemKind));
                string additive = couldGet ? "" : " (but Inventory is full)";
                main.announce_d.Add("LOOT [" + main.enumCtrl.enemys[(int)innerEnemy.kind].Name() + "] : "
                    + main.enumCtrl.items[(int)(drop as Item_Drop).itemKind].Name()
                    + additive);
            }

            main.announce_d.Add("LOOT [" + main.enumCtrl.enemys[(int)innerEnemy.kind].Name() + "] : " + main.enumCtrl.resources[(int)drop.kind].Name() + " + " +
                                tDigit(drop.amount));
            main.rsc.Value[(int)drop.kind] += drop.amount;
        }

        // 全滅判定
        if (currentEnemys.Count == 0)
        {
            //クリア判定
            if (dungeons[(int)dunKind].currentFloor + 1 >= dungeons[(int)dunKind].MaxFloor())
            {
                main.announce_d.Add("Dungeon Clear!!!");
                main.SR.clearNum_Dungeon[(int)dunKind]++;
                main.analytics.DungeonComplete(dunKind);
                dungeons[(int)dunKind].WinAction();

                //最高到達フロア更新
                dungeons[(int)dunKind].maxFloor = dungeons[(int)dunKind].MaxFloor();

                //報酬(ダンジョン)
                for (int i_d = 0; i_d < dungeons[(int)dunKind].drops.Count; i_d++)
                {
                    //乱数を生成してヒットしたらドロップ
                    if (UnityEngine.Random.Range(0f, 100f) < dungeons[(int)dunKind].drops[i_d].probability)
                    {
                        //もしもアイテムだったら
                        if (dungeons[(int)dunKind].drops[i_d] is Item_Drop)
                        {
                            bool couldGet = main.itemCtrl.Drop_Inventory((dungeons[(int)dunKind].drops[i_d] as Item_Drop).itemKind);
                            string additive = couldGet ? "" : " (but Inventory is full)";
                            main.announce_d.Add("LOOT [" + main.enumCtrl.dungeons[(int)dunKind].Name() + "] : "
                                + main.enumCtrl.items[(int)(dungeons[(int)dunKind].drops[i_d] as Item_Drop).itemKind].Name()
                                + additive, Color.green);
                            
                        }
                        else
                        {
                            main.announce_d.Add("LOOT [" + main.enumCtrl.dungeons[(int)dunKind].Name() + "] : " + main.enumCtrl.resources[(int)dungeons[(int)dunKind].drops[i_d].kind].Name() + " + " +
                                tDigit(dungeons[(int)dunKind].drops[i_d].amount));
                            main.rsc.Value[(int)dungeons[(int)dunKind].drops[i_d].kind] += dungeons[(int)dunKind].drops[i_d].amount;
                        }
                    }
                }

                // First Clear Bonus
                if (main.SR.clearNum_Dungeon[(int)dunKind] == 1)//直前で1増やしているので
                {
                    dungeons[(int)dunKind].FirstClearAction();
                    for (int i_d = 0; i_d < dungeons[(int)dunKind].firstDrops.Count; i_d++)
                    {
                        //もしもアイテムだったら
                        if (dungeons[(int)dunKind].firstDrops[i_d] is Item_Drop)
                        {
                            bool couldGet = main.itemCtrl.Drop_Inventory((dungeons[(int)dunKind].firstDrops[i_d] as Item_Drop).itemKind);
                            string additive = couldGet ? "" : " (but Inventory is full)";
                            main.announce_d.Add("LOOT [" + main.enumCtrl.dungeons[(int)dunKind].Name() + "] : "
                                + main.enumCtrl.items[(int)(dungeons[(int)dunKind].firstDrops[i_d] as Item_Drop).itemKind].Name()
                                + additive, Color.green);

                        }
                        else
                        {
                            main.announce_d.Add("LOOT [" + main.enumCtrl.dungeons[(int)dunKind].Name() + "] : " + main.enumCtrl.resources[(int)dungeons[(int)dunKind].firstDrops[i_d].kind].Name() + " + " +
                                tDigit(dungeons[(int)dunKind].firstDrops[i_d].amount));
                            main.rsc.Value[(int)dungeons[(int)dunKind].firstDrops[i_d].kind] += dungeons[(int)dunKind].firstDrops[i_d].amount;
                        }
                    }
                }
                

                dunKind = DungeonKind.nothing;
                FleeFromDungeon();
                if (loopToggle.isOn)
                {
                    StartCoroutine(NewInvokeCor(() => main.progressCtrl.Rest(), 3.0f)); //ループする
                }
                else
                {
                    main.progressCtrl.SwitchProgress(main.progressCtrl.restFunction); //こう書いておくことで１周以上しなくなる
                }
                return;
            }
            /* クリア判定ここまで */


            // 待つ
            waitFloor.Wait((int)(100f/Time.timeScale));
            if (waitFloor.Waiting)
            {
                return; //待っていたら関数そのものから抜ける ***
            }
            else
            {
                waitFloor.Reset();
            }
            //フロアの更新
            dungeons[(int)dunKind].currentFloor++;
            if (dungeons[(int)dunKind].maxFloor < dungeons[(int)dunKind].currentFloor)
            {
                dungeons[(int)dunKind].maxFloor = dungeons[(int)dunKind].currentFloor;
            }
            Summon();
        }
    }


    // Called in Update
    void ApplyComponents()
    {
        if(dunKind == DungeonKind.nothing) { return; }
        DUNGEON thisD = dungeons[(int)dunKind];
        for (int i = 0; i < enemysCmps.Length; i++)
        {
            if (thisD.currentFloor >= thisD.enemyList.Count)
            {
                Debug.Log("currentFloorが想定よりも大きいです");
                continue;
            }
            //もしも現在のフロアの敵の数が、componentsの数よりも小さければcomponentsをfalse ||
            //今見ている敵が死んでいたらfalse
            if (thisD.enemyList[thisD.currentFloor].Length <= i)
            {
                setFalse(enemysCmps[i].gameObject);
                continue;
            }
            InnerEnemy thisE = currentEnemys.Find(e => e.indexInFloor == i); //enemyとiを揃える
            //if (thisE == null) { setFalse(enemysCmps[i].gameObject); }

            if (thisE == null)
            {
                enemysCmps[i].hp_slider.value = 0f;
                enemysCmps[i].int_slider.value = 0f;
                enemysCmps[i].hp_text.text = "0/" + enemysCmps[i].maxHp;
                continue;
            }
            
            setActive(enemysCmps[i].gameObject);
            enemysCmps[i].ApplyNormalObj(main.enumCtrl.enemys[(int)thisE.kind].Name(),
                tDigit(thisE.currentHp,1), tDigit(thisE.maxHp,1),
                "Atk : " + tDigit(thisE.attack,1) + "/Def : " + tDigit(thisE.defense, 1),
                (float)(thisE.currentHp / thisE.maxHp),
                (thisE.currentInterval / thisE.interval),
                thisE.kind
                );

            //ターゲットインデックスを更新
            if (enemysCmps[i].targetToggle.isOn) { targetEnemy_index = i; }
        }

        // 主人公のComponent更新
        heroCmp.ApplyNormalObj("Hero Lv" + main.SR.level.ToString() + " (" + main.rsc.Value[(int)ResourceKind.exp].ToString("F1") + "/" + main.rsc.Max((int)ResourceKind.exp).ToString("F1") + ")"
            , tDigit(main.rsc.Value[(int)ResourceKind.hp], 1), tDigit(main.rsc.Max((int)ResourceKind.hp), 1),
            "Atk : " + tDigit(main.status.Attack, 1) + "/Def : " + tDigit(main.status.Defense, 1),
            (float)(main.rsc.Value[(int)ResourceKind.hp] / main.rsc.Max((int)ResourceKind.hp)),
            currentInterval_normalAttack / Interval_normalAttack) ;//ここにインターバル

        if (allysCmps.Length < main.SD.num_ally)
        {
            throw new Exception("シーンにより多くの味方のプレハブを配置してください");
        }
        // NpcのComponent更新
        for (int i = 0; i < allysCmps.Length; i++)
        {
            if (main.npcSkillCtrl.allyKinds.IndexOf((AllyKind)i) >= 0)
            {
                setActive(allysCmps[i].gameObject);
                allysCmps[i].ApplyNormalObj(main.enumCtrl.allys[(int)i].Name() + " Lv" + main.npcSkillCtrl.npcs[i].level().ToString(),
                    tDigit(main.npcSkillCtrl.npcs[i].currentHp,1), tDigit(main.npcSkillCtrl.npcs[i].Hp,1),
                    "Atk:" + tDigit(main.npcSkillCtrl.npcs[i].Attack,1) + "/Def:" + tDigit(main.npcSkillCtrl.npcs[i].Defense,1),
                    main.npcSkillCtrl.npcs[i].HpSliderValue(),
                main.npcSkillCtrl.npcs[i].IntervalSliderValue());
            }
            else
            {
                setFalse(allysCmps[i].gameObject);
            }
        }
    }

    public void Summon()
    {
        if (dunKind == DungeonKind.nothing) { return; }
        if (dungeons[(int)dunKind].currentFloor >= dungeons[(int)dunKind].enemyList.Count) { return; }
        if (currentEnemys.Count > 0) { return; }
        //currentEnemys = new List<InnerEnemy>(); //重いため普段は呼ばない
        //↑上に Count > 0 を追加したのでコメントアウト
        DUNGEON thisD = dungeons[(int)dunKind];
        for (int i = 0; i < thisD.enemyList[thisD.currentFloor].Length; i++)
        {
            //NOTE:分かりづらいが、現在のダンジョンの現在のフロアの敵をcurrentEnemysに追加している
            currentEnemys.Add(new InnerEnemy(enemys[(int)thisD.enemyList[thisD.currentFloor][i]], i));
        }
        thisD.summonedEnemy = true;
        waitFloor.Reset(); //フロアの待ち時間のカウントをリセット
    }

    public void EnterDungeon()
    {
        main.npcSkillCtrl.InitFight();
        Summon();
        currentInterval_normalAttack = 0;
        main.analytics.DungeonEnter(dunKind);
    }

    //Called in FixedUpdate
    void EnemysAction()
    {
        if (dunKind == DungeonKind.nothing) { return; }
        for (int i = 0; i < currentEnemys.Count; i++)
        {
            currentEnemys[i].currentInterval += 0.1f;//0.1秒に0.1ずつ増える
            if(currentEnemys[i].currentInterval >= currentEnemys[i].interval)
            {
                int target = UnityEngine.Random.Range(0, main.npcSkillCtrl.CalCanFightNum() + 1);
                if (target == 0)
                {
                    //プレイヤーだったら
                    var cal_dmg = CalDmg(currentEnemys[i].attack, main.status.Defense);
                    //避けるか判定
                    if (UnityEngine.Random.Range(0f, 100f) < main.status.DodgeChance)
                    {
                        //避けた
                        main.announce_d.Add("You dodged " + main.enumCtrl.enemys[i].Name() + "'s attack");
                    }
                    else
                    {
                        //当たった
                        main.rsc.Value[(int)ResourceKind.hp] -= cal_dmg;
                        main.announce_d.Add(main.enumCtrl.enemys[i].Name() + " has attacked you for " + tDigit(cal_dmg, 1) + " damages");
                    }
                }
                else
                {
                    //味方だったら
                    AllyKind target_npc = main.npcSkillCtrl.LivingAlly(target);
                    //避けるか判定
                    if (UnityEngine.Random.Range(0f, 100f) < main.npcSkillCtrl.npcs[(int)target_npc].DodgeChance)
                    {
                        //避けた
                        main.announce_d.Add(main.enumCtrl.allys[(int)target_npc].Name()  + " dodged "
                            + main.enumCtrl.enemys[i].Name() + "'s attack");
                    }
                    else
                    {
                        //当たった
                        var cal_dmg = CalDmg(currentEnemys[i].attack, main.npcSkillCtrl.npcs[(int)target_npc].Defense);
                        main.npcSkillCtrl.npcs[(int)target_npc].currentHp -= cal_dmg;
                        main.announce_d.Add(main.enumCtrl.enemys[i].Name() + " has attacked " + main.enumCtrl.allys[(int)target_npc].Name() + " for " + tDigit(cal_dmg) + " damages");
                        //やられたらcanFightから外す
                        if (main.npcSkillCtrl.npcs[(int)target_npc].currentHp <= 0)
                        {
                            main.npcSkillCtrl.npcs[(int)target_npc].currentHp = 0;
                            main.npcSkillCtrl.npcs[(int)target_npc].isDead = true;
                            main.announce_d.Add(main.enumCtrl.allys[(int)target_npc].Name() + " has been defeated");
                            //main.npcSkillCtrl.LeaveFight(main.npcSkillCtrl.canFightAllys[target -1 ]);
                        }
                    }
                }
                currentEnemys[i].currentInterval = 0;
            }
        }
    }

    //Called in FixedUpdate
    void CheckFault()
    {
        if (dunKind == DungeonKind.nothing) { return; }

        // HP判定
        if (main.rsc.Value[(int)ResourceKind.hp] < 0.1d)
        {
            dungeons[(int)dunKind].LoseAction();
            main.progressCtrl.Rest();
            FleeFromDungeon();
            return;
        }

        // コスト判定
        if (CanPurchase(dungeons[(int)dunKind].progressCost))
        {
            Calculate(dungeons[(int)dunKind].progressCost, true);
        }
        else
        {
            main.progressCtrl.Rest();
            FleeFromDungeon();
            return;
        }
    }


    public void FleeFromDungeon()
    {
        dunKind = DungeonKind.nothing;
        if (currentEnemys.Count > 0)
        {
            currentEnemys = new List<InnerEnemy>();
        }
    }

    //一つしか選択できないというOnlyActionとこのスクリプトを対応させる
    void CheckOnlyAction()
    {
        if(main.progressCtrl.currentFunction is DungeonFunction == false)
        {
            FleeFromDungeon();
        }
    }

    //PlaySkillsで呼ばれる
    //target に targetEnemy_index を代入して使っている
    void AttackToEnemys(SkillKind attackSkillKind, double dmg, string actor = "", string _lastSentense = "")
    {
        if (currentEnemys.Count == 0) { return; }
        int target = targetEnemy_index;
        if (target < 0 || target >= currentEnemys.Count)
        {
            target = UnityEngine.Random.Range(0, currentEnemys.Count); //ランダムな敵にダメージ
        }
        
        var cal_dmg = CalDmg(dmg, currentEnemys[target].defense);
        currentEnemys[target].currentHp -= cal_dmg;
        //特定の倒し方のフラグを変更
        //一撃
        if(currentEnemys[target].hasAttacked == false && currentEnemys[target].currentHp <= 0)
        {
            currentEnemys[target].match_oneShot = true;
        }
        currentEnemys[target].hasAttacked = true;

        //特定のスキル、属性
        foreach (var drop in currentEnemys[target].drops)
        {
            switch (drop.dropKind)
            {
                case Drop.DropKind.skill_and:
                    if (drop.skill_AND != attackSkillKind)
                    {
                        currentEnemys[target].match_skill_AND = false;
                    }
                    break;
                case Drop.DropKind.skill_or:
                    if (drop.skill_OR == attackSkillKind)
                    {
                        currentEnemys[target].match_skill_OR = true;
                    }
                    break;
                case Drop.DropKind.attribute_and:
                    foreach (var attribute in skills[(int)attackSkillKind].sources)
                    {
                        if (attribute != drop.attributes_AND)
                        {
                            currentEnemys[target].match_atr_AND = false;
                        }
                    }
                    break;
                case Drop.DropKind.attribute_or:
                    foreach (var attribute in skills[(int)attackSkillKind].sources)
                    {
                        if (attribute == drop.attributes_OR)
                        {
                            currentEnemys[target].match_atr_OR = true;
                        }
                    }
                    break;
                default:
                    break;
            }
            
        }

        string have = actor == "You" ? "have" : "has";
        string lastSentense = _lastSentense;
        //last sentense にスキルの名前を増やす
        if(attackSkillKind != SkillKind.normalAttack)
        {
            lastSentense = " using " + main.enumCtrl.skills[(int)attackSkillKind].Name() + lastSentense;
        }

        if (actor == "") { return; }
        main.announce_d.Add(actor + " " + have + " attacked " + main.enumCtrl.enemys[target].Name() + " for " + tDigit(cal_dmg, 1) + " damages" + lastSentense);
    }


    void TestEnemys()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (i == 0) { continue; }
            if (enemys[i] == null) { Debug.Log((EnemyKind)i + " がエネミーコンテナにアタッチされていません。"); }
        }
    }


    void TestDungeons()
    {
        for (int i = 0; i < dungeons.Length; i++)
        {
            if (i == 0) { continue; }
            if (dungeons[i] == null) { Debug.Log((DungeonKind)i + " がDungeonCanvasに設置されていません"); }
        }
    }
}
