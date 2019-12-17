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
    public List<Drop> drops = new List<Drop>();

    public InnerEnemy(ENEMY enemy)
    {
        kind = enemy.kind;
        maxHp = enemy.maxHp;
        interval = enemy.interval;
        defense = enemy.defense;
        attack = enemy.attack;
        drops = enemy.drops;

        currentHp = maxHp;
        currentInterval = 0;
    }

    public void ApplyAlive()
    {
        alive = currentHp > 0;
    }
}
