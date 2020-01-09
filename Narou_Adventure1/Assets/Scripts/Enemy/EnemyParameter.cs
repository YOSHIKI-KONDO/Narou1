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

        parameters.Add(null);                                  //HP, ATK, INT, DEF, GOLD, EXP,RANK
        parameters.Add(new parameter(EnemyKind.slime,            10,   5,   1,   0,    1,   2,  1));
        parameters.Add(new parameter(EnemyKind.goblin,           12,   5,   3,   0,    3,   3,  1));
        parameters.Add(new parameter(EnemyKind.rat,              25,   2,   9,   0,    1,   5,  1));
        parameters.Add(new parameter(EnemyKind.bird,             10,   3,   2,   0,    1,   2,  1));
        parameters.Add(new parameter(EnemyKind.bat,               8,   4,   2,   0,    1,   3,  1));
        parameters.Add(new parameter(EnemyKind.wolf,             20,   3, 2.5,   0,    1,   5,  1));
        parameters.Add(new parameter(EnemyKind.snake,             8,  10,   8,   0,    1,   3,  1));
        parameters.Add(new parameter(EnemyKind.demonic,         200,   5,  25,   0,   10,   2,  1));
        parameters.Add(new parameter(EnemyKind.sigurd,           10,   4,   4,   0,    5,   5,  1));
        parameters.Add(new parameter(EnemyKind.askr,              9,   5,   3,   0,    4,   3,  1));
        parameters.Add(new parameter(EnemyKind.embla,             7,   3,   2,   0,    4,   3,  1));
        parameters.Add(new parameter(EnemyKind.red_slime,        40,   5,  14,   0,   10,   8,  1));
        parameters.Add(new parameter(EnemyKind.orc,              40,   6,   7,   0,   30,   8,  1));
        parameters.Add(new parameter(EnemyKind.poison_rat,       25,   2,  12,   0,   10,   8,  1));
        parameters.Add(new parameter(EnemyKind.harpy,            50,   3,  10,   0,   10,   9,  1));
        parameters.Add(new parameter(EnemyKind.ghoul,            60,   4,  12,   0,   10,   8,  1));
        parameters.Add(new parameter(EnemyKind.werewolf,         55,   4,  12,   0,   10,  10,  1));
        parameters.Add(new parameter(EnemyKind.lizard_man,       25,  5f,  13,   0,   10,  12,  1));
        parameters.Add(new parameter(EnemyKind.demonic_warrior, 100,   5,  30,   0,  100,  20,  1));
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
        public double maxHp;
        public float interval;
        public double attack;
        public double defense;
        public int gold;
        public int exp;
        public int rank;

        public parameter(EnemyKind kind, double maxHp, float interval, double attack, double defense, int gold, int exp, int rank)
        {
            this.kind = kind;
            this.maxHp = maxHp;
            this.interval = interval;
            this.attack = attack;
            this.defense = defense;
            this.gold = gold;
            this.exp = exp;
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
