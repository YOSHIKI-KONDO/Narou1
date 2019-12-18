using System;
using System.Collections.Generic;
using UnityEngine;
using static UsefulMethod;
using UnityEngine.UI;
using UnityEngine.Analytics;

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


    /* UI */
    public Text dungenNameText;
    public Text floorText;
    public Slider floorSlider;
    public Button fleeButton;
    /******/


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
	}

    private void FixedUpdate()
    {
        CheckFault();

        PlaySkills();
        PlayNpcSkills();

        EnemysAction();
    }

    //Called in Fixed Update
    void PlaySkills()
    {
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
                    skills[(int)thisKind].Produce();

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
                        AttackToEnemys(thisKind, skills[(int)thisKind].WarriorDamage() * main.status.Attack * criticalFactor, "You", criticalSentense);
                        //魔法系の攻撃
                        AttackToEnemys(thisKind, skills[(int)thisKind].SorcererDamage() * main.status.MagicAttack * criticalFactor, "You", criticalSentense);
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
                AttackToEnemys(thisSkill.kind, thisSkill.WarriorDamage() * main.npcSkillCtrl.npcs[i].Attack * criticalFactor
                    , main.enumCtrl.allys[i].Name(), criticalSentense);
                AttackToEnemys(thisSkill.kind, thisSkill.SorcererDamage() * main.npcSkillCtrl.npcs[i].MagicAttack * criticalFactor
                    , main.enumCtrl.allys[i].Name(), criticalSentense);
                thisSkill.Produce();
                main.announce_d.Add(main.enumCtrl.allys[i].Name() + " casted " + main.enumCtrl.skills[(int)thisSkill.kind].Name());
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

    void Load()
    {
        for (int i_c = 0; i_c < COLUMN_SLOT; i_c++)
        {
            for (int i_r = 0; i_r < ROW_SLOT; i_r++)
            {
                slotKinds[i_r, i_c] = main.SR.slotKinds[ROW_SLOT * i_c + i_r];
            }
        }
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
            floorText.text = dungeons[(int)dunKind].currentFloor.ToString() + " / " + dungeons[(int)dunKind].MaxFloor().ToString();
            floorSlider.value = ((float)dungeons[(int)dunKind].currentFloor / (float)dungeons[(int)dunKind].MaxFloor());
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
                            currentEnemys[i].drops[0].amount /= (main.npcSkillCtrl.allyKinds.Count + 1);
                            foreach (var npc_kind in main.npcSkillCtrl.allyKinds)
                            {
                                //経験値を足す
                                main.npcSkillCtrl.npcs[(int)npc_kind].currentExp(main.npcSkillCtrl.npcs[(int)npc_kind].currentExp()
                                    + currentEnemys[i].drops[i_d].amount);
                                //Announce
                                main.announce_d.Add("LOOT [" + main.enumCtrl.enemys[(int)currentEnemys[i].kind].Name() + "] : "
                                    + main.enumCtrl.allys[(int)npc_kind].Name() + " gained "
                                    + main.enumCtrl.resources[(int)currentEnemys[i].drops[i_d].kind].Name() + " + "
                                    + tDigit(currentEnemys[i].drops[i_d].amount));
                            }
                            //自分(普通のドロップの書き方と同じ)
                            GetDrops(currentEnemys[i], currentEnemys[i].drops[i_d]);

                            continue; //※
                        }
                        else
                        {
                            GetDrops(currentEnemys[i], currentEnemys[i].drops[i_d]);
                        }
                    }
                }
                currentEnemys.Remove(currentEnemys[i]);
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
            dungeons[(int)dunKind].currentFloor++;
            if (dungeons[(int)dunKind].currentFloor >= dungeons[(int)dunKind].MaxFloor())
            {
                main.announce_d.Add("Dungeon Clear!!!");
                main.SR.clearNum_Dungeon[(int)dunKind]++;
                AnalyticsEvent.LevelComplete(dunKind.ToString());

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
                                + additive);
                            
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
                                + additive);

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
                main.progressCtrl.SwitchProgress(main.progressCtrl.restFunction); //こう書いておくことで１周以上しなくなる
                return;
            }
            Summon();
        }

        // Componentを更新
        DUNGEON thisD = dungeons[(int)dunKind];
        for (int i = 0; i < enemysCmps.Length; i++)
        {
            if(thisD.currentFloor >= thisD.enemyList.Count)
            {
                Debug.Log("currentFloorが想定よりも大きいです");
                continue;
            }
            //もしも現在のフロアの敵の数が、componentsの数よりも小さければcomponentsをfalse ||
            //今見ている敵が死んでいたらfalse
            if(thisD.enemyList[thisD.currentFloor].Length <= i ||
                currentEnemys.Count <= i)
            {
                setFalse(enemysCmps[i].gameObject);
                continue;
            }
            setActive(enemysCmps[i].gameObject);
            enemysCmps[i].ApplyNormalObj(main.enumCtrl.enemys[(int)thisD.enemyList[thisD.currentFloor][i]].Name(),
                tDigit(currentEnemys[i].currentHp) + "/" + tDigit(currentEnemys[i].maxHp),
                "Atk : " + tDigit(currentEnemys[i].attack),
                (float)(currentEnemys[i].currentHp / currentEnemys[i].maxHp),
                (float)(currentEnemys[i].currentInterval / currentEnemys[i].interval)
                );
        }

        // 主人公のComponent更新
        heroCmp.ApplyNormalObj("Hero", tDigit(main.rsc.Value[(int)ResourceKind.hp]) + "/" + tDigit(main.rsc.Max((int)ResourceKind.hp)),
            "",
            (float)(main.rsc.Value[(int)ResourceKind.hp] / main.rsc.Max((int)ResourceKind.hp)),
            1f);

        if (allysCmps.Length < main.SD.num_ally)
        {
            throw new Exception("シーンにより多くの味方のプレハブを配置してください");
        }
        // NpcのComponent更新
        for (int i = 0; i < allysCmps.Length; i++)
        {
            if(main.npcSkillCtrl.allyKinds.IndexOf((AllyKind)i) >= 0)
            {
                setActive(allysCmps[i].gameObject);
                allysCmps[i].ApplyNormalObj(main.enumCtrl.allys[(int)i].Name(), tDigit(main.npcSkillCtrl.npcs[i].currentHp) + "/" + tDigit(main.npcSkillCtrl.npcs[i].Hp),
                    "Atk:" + tDigit(main.npcSkillCtrl.npcs[i].Attack) + "/MAtk:" + tDigit(main.npcSkillCtrl.npcs[i].MagicAttack),
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
            currentEnemys.Add(new InnerEnemy(enemys[(int)thisD.enemyList[thisD.currentFloor][i]]));
        }
        thisD.summonedEnemy = true;
    }

    public void EnterDungeon()
    {
        main.npcSkillCtrl.InitFight();
        Summon();
        AnalyticsEvent.LevelStart(dunKind.ToString());
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
                        main.announce_d.Add(main.enumCtrl.enemys[i].Name() + " has attacked you for " + tDigit(cal_dmg) + " damages");
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
        if (main.rsc.Value[(int)ResourceKind.hp] <= 0)
        {
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
    void AttackToEnemys(SkillKind attackSkillKind, double dmg, string actor = "", string lastSentense = "")
    {
        if (currentEnemys.Count == 0) { return; }
        int target = UnityEngine.Random.Range(0, currentEnemys.Count); //ランダムな敵にダメージ
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
                    foreach (var attribute in skills[(int)attackSkillKind].attributes)
                    {
                        if (attribute != drop.attributes_AND)
                        {
                            currentEnemys[target].match_atr_AND = false;
                        }
                    }
                    break;
                case Drop.DropKind.attribute_or:
                    foreach (var attribute in skills[(int)attackSkillKind].attributes)
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

        if(actor != "")
        {
            main.announce_d.Add(actor + " has attacked " + main.enumCtrl.enemys[target].Name() + " for " + tDigit(cal_dmg) + " damages" + lastSentense);
        }
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
