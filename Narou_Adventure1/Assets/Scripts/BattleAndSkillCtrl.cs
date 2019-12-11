using System;
using System.Collections.Generic;
using UnityEngine;
using static UsefulMethod;
using UnityEngine.UI;

public class BattleAndSkillCtrl : BASE {
    /// <summary>
    /// [0,0], [1,0], [2,0], [3,0],
    /// [0,1], [1,1], [2,1], [3,1],
    /// [0,2], [1,2], [2,2], [3,2],
    /// </summary>
    public readonly int ROW_SLOT = 4;
    public readonly int COLUMN_SLOT = 3;
    public SkillKind[,] slotKinds;
    public SKILL[] skills = new SKILL[Enum.GetNames(typeof(SkillKind)).Length];
    [SerializeField]SkillSlot[] slots_ins;//InspectorからAddする
    SkillSlot[,] slots;
    int currentRow;

    public DungeonKind dunKind;
    public SkillKind skillKind;


    /* battle */
    public DUNGEON[] dungeons = new DUNGEON[Enum.GetNames(typeof(DungeonKind)).Length];
    public ENEMY[] enemys = new ENEMY[Enum.GetNames(typeof(EnemyKind)).Length];
    public List<InnerEnemy> currentEnemys = new List<InnerEnemy>();
    public BattleComponents heroCmp;                   //Object
    public BattleComponents[] enemysCmps;              //Object
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
                        //剣士系の攻撃
                        AttackToEnemys(skills[(int)thisKind].WarriorDamage() * (main.rsc.Max((int)ResourceKind.strength) + main.rsc.Max((int)ResourceKind.attack)));
                        //魔法系の攻撃
                        AttackToEnemys(skills[(int)thisKind].SorcererDamage() * (main.rsc.Max((int)ResourceKind.mentalStrength) + main.rsc.Max((int)ResourceKind.magic_attack)));
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
                slotKinds[i_r, i_c] = main.SR.slotKinds[COLUMN_SLOT * i_c + i_r];
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
                main.SR.slotKinds[COLUMN_SLOT * i_c + i_r] = slotKinds[i_r, i_c];
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
            battleScreen.ResetPosition();//戦闘中ならアクティブ
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
                //報酬
                for (int i_d = 0; i_d < currentEnemys[i].drops.Count; i_d++)
                {
                    //乱数を生成してヒットしたらドロップ
                    if(UnityEngine.Random.Range(0f,100f) < currentEnemys[i].drops[i_d].probability)
                    {
                        main.announce.Add("LOOT(" + main.enumCtrl.enemys[(int)currentEnemys[i].kind].Name() + ") : " + main.enumCtrl.resources[(int)currentEnemys[i].drops[i_d].kind].Name() + " + " +
                            tDigit(currentEnemys[i].drops[i_d].amount));
                        main.rsc.Value[(int)currentEnemys[i].drops[i_d].kind] += currentEnemys[i].drops[i_d].amount;
                    }
                }
                currentEnemys.Remove(currentEnemys[i]);
            }
        }


        // 全滅判定
        if (currentEnemys.Count == 0)
        {
            dungeons[(int)dunKind].currentFloor++;
            if (dungeons[(int)dunKind].currentFloor >= dungeons[(int)dunKind].MaxFloor())
            {
                main.announce.Add("Dungeon Clear!!!");
                main.SR.clearNum_Dungeon[(int)dunKind]++;

                //報酬
                for (int i_d = 0; i_d < dungeons[(int)dunKind].drops.Count; i_d++)
                {
                    //乱数を生成してヒットしたらドロップ
                    if (UnityEngine.Random.Range(0f, 100f) < dungeons[(int)dunKind].drops[i_d].probability)
                    {
                        main.announce.Add("LOOT(" + main.enumCtrl.dungeons[(int)dunKind].Name() + ") : " + main.enumCtrl.resources[(int)dungeons[(int)dunKind].drops[i_d].kind].Name() + " + " +
                            tDigit(dungeons[(int)dunKind].drops[i_d].amount));
                        main.rsc.Value[(int)dungeons[(int)dunKind].drops[i_d].kind] += dungeons[(int)dunKind].drops[i_d].amount;
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

        // 味方のComponent更新
        heroCmp.ApplyNormalObj("Hero", tDigit(main.rsc.Value[(int)ResourceKind.hp]) + "/" + tDigit(main.rsc.Max((int)ResourceKind.hp)),
            "",
            (float)(main.rsc.Value[(int)ResourceKind.hp] / main.rsc.Max((int)ResourceKind.hp)),
            1f);
    }

    public void Summon()
    {
        if (dunKind == DungeonKind.nothing) { return; }
        if (dungeons[(int)dunKind].currentFloor >= dungeons[(int)dunKind].enemyList.Count) { return; }
        currentEnemys = new List<InnerEnemy>(); //重いため普段は呼ばない
        DUNGEON thisD = dungeons[(int)dunKind];
        for (int i = 0; i < thisD.enemyList[thisD.currentFloor].Length; i++)
        {
            //NOTE:分かりづらいが、現在のダンジョンの現在のフロアの敵をcurrentEnemysに追加している
            currentEnemys.Add(new InnerEnemy(enemys[(int)thisD.enemyList[thisD.currentFloor][i]]));
        }
        thisD.summonedEnemy = true;
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
                main.rsc.Value[(int)ResourceKind.hp] -= currentEnemys[i].attack;
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
    void AttackToEnemys(double dmg)
    {
        if (currentEnemys.Count == 0) { return; }
        currentEnemys[0].currentHp -= dmg;
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
