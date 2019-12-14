using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// allyKinds      ...現在の同行者の種類。
/// </summary>
public class NpcSkillCtrl : BASE {
    public class Npc
    {
        public AllyKind kind;
        public List<SkillKind> skills = new List<SkillKind>();
        Main main;
        public delegate int IntSync(int? x = null);
        public IntSync level;
        public double Hp { get => initHp + plusHp * level(); }
        public double Strength { get => initStrength + plusStrength * level(); }
        public double MentalStrength { get => initMStrength + plusMStrength * level(); }
        public double Attack { get => Strength; }
        public double MagicAttack { get => MentalStrength; }
        public double Defense { get => initDefense + plusDefense * level(); }
        public double DodgeChance { get => initDodge + plusDodge * level(); }
        public double CriticalChance { get => initCriticalC + plusCriticalC * level(); }
        public double CriticalFactor { get => initCriticalD + plusCriticalD * level(); }

        int initHp, plusHp;
        int initStrength, plusStrength;
        int initMStrength, plusMStrength;
        int initDefense, plusDefense;
        double initDodge, plusDodge;
        double initCriticalC, plusCriticalC;
        double initCriticalD, plusCriticalD;

        public double currentHp;
        double current_time;
        bool casted;
        SKILL thisSkill;

        public Npc(AllyKind kind, IntSync level, Main main, int initHp, int plusHp, int initStrength, int plusStrength,
            int initMStrength, int plusMStrength, int initDefense, int plusDefense, int initDodge, int plusDodge,
            int initCriticalC, int plusCriticalC, int initCriticalD, int plusCriticalD)
        {
            this.kind = kind;
            this.level = level;
            this.main = main;
            this.initHp         = initHp;
            this.plusHp         = plusHp;
            this.initStrength   = initStrength;
            this.plusStrength   = plusStrength;
            this.initMStrength  = initMStrength;
            this.plusMStrength  = plusMStrength;
            this.initDefense    = initDefense;
            this.plusDefense    = plusDefense;
            this.initDodge      = initDodge;
            this.plusDodge      = plusDodge;
            this.initCriticalC  = initCriticalC;
            this.plusCriticalC  = plusCriticalC;
            this.initCriticalD  = initCriticalD;
            this.plusCriticalD  = plusCriticalD;

            Initialize();
        }
        

        //Called in FixedUpdate
        //呼ぶたびに詠唱が溜まり、最大まで行ったらその時ためていたSKILLを返す
        public SKILL Cast()
        {
            if (casted)
            {
                current_time += 0.1d; //0.1秒ごとに0.1ずつたまる
                if(current_time >= thisSkill.Duration())
                {
                    current_time = 0;
                    casted = false;
                    return thisSkill;
                }

            }
            else
            {
                int cast_index = UnityEngine.Random.Range(0, skills.Count);
                thisSkill = main.battleCtrl.skills[(int)skills[cast_index]];
                casted = true;
            }
            return null;
        }

        public float HpSliderValue()
        {
            return (float)(currentHp / Hp);
        }

        public float IntervalSliderValue()
        {
            if(thisSkill == null) { return 1f; }
            return (float)(current_time / thisSkill.Duration());
        }

        public void Initialize()
        {
            currentHp = Hp;
            current_time = 0;
            casted = false;
        }
    }

    public List<AllyKind> allyKinds { get => main.SR.allyKinds; set => main.SR.allyKinds = value; } //現在の同行者
    public List<AllyKind> canFightAllys;
    public Npc[] npcs;

    public void JoinAlly(AllyKind kind)
    {
        //現在の仕様だと一人まで
        if(allyKinds.IndexOf(kind) < 0)
        {
            allyKinds.Add(kind);
        }
    }

    public void LeaveAlly(AllyKind kind)
    {
        //現在の仕様だと一人まで
        if (allyKinds.IndexOf(kind) >= 0)
        {
            allyKinds.Remove(kind);
        }
    }

    public void InitFight()
    {
        canFightAllys = new List<AllyKind>();
        canFightAllys.AddRange(allyKinds);
    }

    public void LeaveFight(AllyKind kind)
    {
        //現在の仕様だと一人まで
        if (canFightAllys.IndexOf(kind) >= 0)
        {
            canFightAllys.Remove(kind);
        }
    }

	// Use this for initialization
	void Awake () {
		StartBASE();

        npcs = new Npc[main.SD.num_ally];
        npcs[(int)AllyKind.npcA] =
            new Npc(AllyKind.npcA, x => Sync(ref main.SR.levels_Ally[(int)AllyKind.npcA], x), main,
            10, 5,
            1, 2,
            1, 2,
            0, 1,
            0, 0,
            0, 0,
            2, 0);
        npcs[(int)AllyKind.npcA].skills.AddRange(new List<SkillKind> { SkillKind.meteor, SkillKind.thuderBolt });
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
