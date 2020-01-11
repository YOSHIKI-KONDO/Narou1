using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class EnemyParameter : BASE {
    public List<parameter> parameters = new List<parameter>();

	// Use this for initialization
	void Awake () {
		StartBASE();

        parameters.Add(null);                                 //Int,  HP, ATK, DEF, GOLD, EXP, RANK, Level
        parameters.Add(new parameter(EnemyKind.slime,             5,   5,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.goblin,            5,   6,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.rat,               2,   5,   1,  0.5,    1,   1,    1,   3));
        parameters.Add(new parameter(EnemyKind.bird,              3,   5,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.bat,               4,   4,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.wolf,              3,   5,   1,  0.5,    1,   1,    1,   4));
        parameters.Add(new parameter(EnemyKind.snake,            10,   4,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.demonic,           5,   5,   1,  0.5,    1,   1,    1,  40));
        parameters.Add(new parameter(EnemyKind.sigurd,            4,   5,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.askr,              5, 4.5,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.embla,             3, 3.5,   1,  0.5,    1,   1,    1,   2));
        parameters.Add(new parameter(EnemyKind.red_slime,         5,   5,   1,  0.5,    1,   1,    1,   8));
        parameters.Add(new parameter(EnemyKind.orc,               6,   5,   1,  0.5,    1,   1,    1,   8));
        parameters.Add(new parameter(EnemyKind.poison_rat,        2,   5,   1,  0.5,    1,   1,    1,   5));
        parameters.Add(new parameter(EnemyKind.harpy,             3,   5,   1,  0.5,    1,   1,    1,  10));
        parameters.Add(new parameter(EnemyKind.ghoul,             4,   5,   1,  0.5,    1,   1,    1,  12));
        parameters.Add(new parameter(EnemyKind.werewolf,          4,  11,   1,  0.5,    1,   1,    1,  11));
        parameters.Add(new parameter(EnemyKind.lizard_man,        5,   5,   1,  0.5,    1,   1,    1,   5));
        parameters.Add(new parameter(EnemyKind.demonic_warrior,   5,   5,   1,  0.5,    1,   1,    1,  20));
    }

	// Use this for initialization
	void Start () {
        CheckValid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public class parameter
    {
        public EnemyKind kind;
        public double MaxHp { get => maxHp * level; }      //levelの関数
        public float Interval { get => interval; }                 
        public double Attack { get => attack * level; }    //levelの関数
        public double Defense { get => defense * level; }  //levelの関数
        public int Gold { get => gold * level; }           //levelの関数
        public int Exp { get => exp * level; }             //levelの関数
        double maxHp;
        float interval;
        double attack;
        double defense;
        int gold;
        int exp;
        public int rank;
        public int level;

        public parameter(EnemyKind kind, float interval, double base_maxHp, double base_attack, double base_defense, int base_gold, int base_exp, int rank, int level)
        {
            this.level = level;
            this.kind = kind;
            this.maxHp = base_maxHp;       
            this.interval = interval;      
            this.attack = base_attack;     
            this.defense = base_defense;   
            this.gold = base_gold;         
            this.exp = base_exp;           
            this.rank = rank;
        }
        public parameter()
        {
            kind = EnemyKind.nothing;
            maxHp = 1;
            interval = 1;
            attack = 1;
            defense = 1;
            gold = 1;
            exp = 1;
            rank = 1;
        }
    }

    void CheckValid()
    {
        if(parameters.Count != Enum.GetNames(typeof(EnemyKind)).Length)
        {
            throw new Exception("EnemyParameterのparametersの要素が少ないです。");
        }
        for (int i = 0; i < Enum.GetNames(typeof(EnemyKind)).Length; i++)
        {
            if (i == 0) { continue; }
            if(i != (int)parameters[i].kind)
            {
                throw new Exception("EnemyParameterのparametersの順番が違います。(" + main.enumCtrl.enemys[i].Name() + ")");
            }
        }
    }
}
