using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ENEMY : BASE {
    [NonSerialized]public EnemyKind kind;
    [NonSerialized]public double maxHp;
    [NonSerialized]public float interval;
    [NonSerialized]public double defense;
    [NonSerialized]public double attack;
    public List<Drop> drops = new List<Drop>();

    public void AwakeEnemy(EnemyKind kind, double maxHp, float interval, double attack, double defense, int gold, int exp)
    {
        StartBASE();
        this.kind = kind;
        main.battleCtrl.enemys[(int)kind] = this;

        this.maxHp = maxHp;
        this.interval = interval;
        this.attack = attack;
        this.defense = defense;
        drops.Add(new Drop(ResourceKind.gold, gold, 100));
        drops.Add(new Drop(ResourceKind.exp, exp, 100));
    }

    public void StartEnemy()
    {

    }

    public void UpdateEnemy()
    {

    }

    public void FixedUpdateEnemy()
    {

    }
}
