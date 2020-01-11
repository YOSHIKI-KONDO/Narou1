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
        parameters.Add(new parameter(EnemyKind.slime,             5,   4,   5,   4,    1,   2,  1));
        parameters.Add(new parameter(EnemyKind.goblin,            8,   3,   3,   7,    3,   3,  1));
        parameters.Add(new parameter(EnemyKind.rat,               4,   2,   2,   4,    1,   5,  1));
        parameters.Add(new parameter(EnemyKind.bird,              5,   5,   4,   5,    1,   2,  1));
        parameters.Add(new parameter(EnemyKind.bat,               5,   7,   5,   4,    1,   3,  1));
        parameters.Add(new parameter(EnemyKind.wolf,              7,   4,   3,   6,    1,   5,  1));
        parameters.Add(new parameter(EnemyKind.snake,             6,   9,   7,   5,    1,   3,  1));
        parameters.Add(new parameter(EnemyKind.demonic,           9,   5,   3,   4,   10,   2,  1));
        parameters.Add(new parameter(EnemyKind.sigurd,            9,   4,   4,   5,    5,   5,  1));
        parameters.Add(new parameter(EnemyKind.askr,              8,   5,   3,   5,    4,   3,  1));
        parameters.Add(new parameter(EnemyKind.embla,             7,   3,   2,   5,    4,   3,  1));
        parameters.Add(new parameter(EnemyKind.red_slime,         4,   4,   2,   4,   10,   8,  2));
        parameters.Add(new parameter(EnemyKind.orc,               9,   6,   5,   6,   30,   8,  2));
        parameters.Add(new parameter(EnemyKind.poison_rat,        6,   2,   2,   5,   10,   8,  2));
        parameters.Add(new parameter(EnemyKind.harpy,             6,   4,   4,   6,   10,   9,  2));
        parameters.Add(new parameter(EnemyKind.ghoul,             4,   7,   3,   3,   10,   8,  2));
        parameters.Add(new parameter(EnemyKind.werewolf,          8,   4,   3,   7,   10,  10,  2));
        parameters.Add(new parameter(EnemyKind.lizard_man,        6,   8,   6,   8,   10,  12,  2));
        parameters.Add(new parameter(EnemyKind.demonic_warrior,   9,   4,   2,   5,  100,  20,  2));
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
