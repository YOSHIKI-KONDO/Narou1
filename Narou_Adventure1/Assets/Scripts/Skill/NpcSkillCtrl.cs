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
        public delegate double DoubleSync(double? x = null);
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
        public double MaxExp { get => CashMaxExp ?? CalculateMaxExp(); }
        public DoubleSync currentExp;

        int initHp, plusHp;
        int initStrength, plusStrength;
        int initMStrength, plusMStrength;
        int initDefense, plusDefense;
        double initDodge, plusDodge;
        double initCriticalC, plusCriticalC;
        double initCriticalD, plusCriticalD;
        double initExp, powerExp;
        double? CashMaxExp = null; //levelupした時にnullにする

        public bool isDead;
        public double currentHp;
        
        double current_time;
        bool casted;
        SKILL thisSkill;

        public Npc(AllyKind kind, IntSync level, Main main, int initHp, int plusHp, int initStrength, int plusStrength,
            int initMStrength, int plusMStrength, int initDefense, int plusDefense, int initDodge, int plusDodge,
            int initCriticalC, int plusCriticalC, int initCriticalD, int plusCriticalD, double initExp, double powerExp,
            DoubleSync currentExp)
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
            this.initExp = initExp;
            this.powerExp = powerExp;
            this.currentExp = currentExp;

            Initialize();
        }

        //最大値の計算
        double CalculateMaxExp()
        {
            CashMaxExp = initExp * Math.Pow(level(), powerExp);
            return (double)CashMaxExp;
        }

        public void CheckLevelUp()
        {
            while(currentExp() >= MaxExp)
            {
                currentExp(currentExp() - MaxExp);
                level(level() + 1);
                CashMaxExp = null;
                CalculateMaxExp();
                main.announce.Add(main.enumCtrl.allys[(int)kind].Name() + " Level UP! (" + (level() - 1).ToString() + "→" + level() + ")");
            }
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
            isDead = false;
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

        foreach (var npc in npcs)
        {
            if(npc == null) { continue; }
            npc.Initialize();
        }
    }

    public void LeaveFight(AllyKind kind)
    {
        //現在の仕様だと一人まで
        if (canFightAllys.IndexOf(kind) >= 0)
        {
            canFightAllys.Remove(kind);
        }
    }

    //CanFightAllysの中で、生きているnpcの数を返す
    public int CalCanFightNum()
    {
        int sum = 0;
        foreach (var kind in canFightAllys)
        {
            if (npcs[(int)kind].isDead)
                continue;

            sum++;
        }
        return sum;
    }

    //canFightAllysで「何番目のNPCか」を渡された時に、そのEnumを返す関数
    public AllyKind LivingAlly(int index)
    {
        int sum = 0;
        for (int i = 0; i < canFightAllys.Count; i++)
        {
            if (npcs[(int)canFightAllys[i]] == null) { continue; }
            if (npcs[(int)canFightAllys[i]].isDead == false)
            {
                sum++;
            }
            if(sum == index)
            {
                return canFightAllys[i];
            }
        }
        return AllyKind.nothing;
    }

    //レベルアップの確認
    void CheckLevelUp()
    {
        foreach (var npc in npcs)
        {
            if(npc == null) { continue; }
            npc.CheckLevelUp();
        }
    }

    // Use this for initialization
    void Awake () {
		StartBASE();

        npcs = new Npc[main.SD.num_ally];
        npcs[(int)AllyKind.npcA] =
            new Npc(AllyKind.npcA, x => Sync(ref main.SR.levels_Ally[(int)AllyKind.npcA], x), main,
            4, 1,           //hp
            0, 1,           //str
            0, 1,           //m str
            0, 0,           //def
            0, 0,           //dodge
            0, 0,           //cri chance
            2, 0,           //cri factor
            10, 2.5,        //exp
            x=>Sync(ref main.SR.exps_Ally[(int)AllyKind.npcA],x));
        npcs[(int)AllyKind.npcA].skills.AddRange(new List<SkillKind> { SkillKind.normalAttack_npc1 });
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        CheckLevelUp();
    }
}
