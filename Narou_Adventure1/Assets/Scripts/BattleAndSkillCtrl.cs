using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

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
    [SerializeField] GameObject enemysContainer;
    public ENEMY[] ememys = new ENEMY[Enum.GetNames(typeof(EnemyKind)).Length];
    int currentRow;

    public DungeonKind dunKind;
    public SkillKind skillKind;

	// Use this for initialization
	void Awake () {
		StartBASE();
        InitializeSlots();
	}

	// Use this for initialization
	void Start () {
        TestSkills();
	}
	
	// Update is called once per frame
	void Update () {
        ApplySkills();
        ApplySkillSlots();
	}

    private void FixedUpdate()
    {
        PlaySkills();
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
                    Debug.Log("敵に" + tDigit(skills[(int)thisKind].WarriorDamage()) + " damage");
                    Debug.Log("敵に" + tDigit(skills[(int)thisKind].SorcererDamage()) + " damage");
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
}
