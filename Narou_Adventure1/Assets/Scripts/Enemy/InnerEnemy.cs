using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerEnemy
{
    public double currentHp;
    public float currentInterval;
    public bool alive;

    public EnemyKind kind;
    public double maxHp;
    public float interval;
    public double defense;
    public double attack;
    public int indexInFloor; //そのフロアで何番目の敵か
    public List<Drop> drops = new List<Drop>();
    //public List<Drop> drops_oneShot = new List<Drop>();  //一撃で倒した際の報酬
    //public List<Drop> drops_atr_AND = new List<Drop>();  //必要な属性全てを使って倒した場合の報酬(i)
    //public List<Drop> drops_atr_OR = new List<Drop>();   //必要な属性どれか一つを使って倒した場合の報酬(ii)
    //public List<Drop> drops_skill = new List<Drop>();    //特定のスキルを使って倒した場合の報酬(iii)

    public bool match_oneShot = false;//一撃で倒されたかどうか
    public bool match_atr_AND = true; //(i)を満たしているかどうか
    public bool match_atr_OR = false; //(ii)を満たしているかどうか
    public bool match_skill_AND = true;   //(iii)を満たしているかどうか
    public bool match_skill_OR = false;

    public bool hasAttacked;//攻撃されたかどうか

    public InnerEnemy(ENEMY enemy, int indexInFloor)
    {
        kind = enemy.kind;
        maxHp = enemy.maxHp;
        interval = enemy.interval;
        defense = enemy.defense;
        attack = enemy.attack;
        drops = enemy.drops;
        this.indexInFloor = indexInFloor;

        currentHp = maxHp;
        currentInterval = 0;
    }

    public void ApplyAlive()
    {
        alive = currentHp > 0;
    }
}
