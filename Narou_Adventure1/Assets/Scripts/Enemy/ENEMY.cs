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
    [NonSerialized]public int rank;
    public List<Drop> drops = new List<Drop>();
    //public List<Drop> drops_oneShot = new List<Drop>();  //一撃で倒した際の報酬
    //public List<Drop> drops_atr_AND = new List<Drop>();  //必要な属性全てを使って倒した場合の報酬(i)
    //public List<Drop> drops_atr_OR = new List<Drop>();   //必要な属性どれか一つを使って倒した場合の報酬(ii)
    //public List<Drop> drops_skill = new List<Drop>();    //特定のスキルを使って倒した場合の報酬(iii)

    public void AwakeEnemy(EnemyKind kind)
    {
        StartBASE();
        this.kind = kind;
        main.battleCtrl.enemys[(int)kind] = this;

        this.maxHp = main.enemyParameter.parameters[(int)kind].maxHp;
        this.interval = main.enemyParameter.parameters[(int)kind].interval;
        this.attack = main.enemyParameter.parameters[(int)kind].attack;
        this.defense = main.enemyParameter.parameters[(int)kind].defense;
        this.rank = main.enemyParameter.parameters[(int)kind].rank;
        drops.Add(new Drop(ResourceKind.exp, main.enemyParameter.parameters[(int)kind].exp, 100));
        drops.Add(new Drop(ResourceKind.gold, main.enemyParameter.parameters[(int)kind].gold, 100));
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
