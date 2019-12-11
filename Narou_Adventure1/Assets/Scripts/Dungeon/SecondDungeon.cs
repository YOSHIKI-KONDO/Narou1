using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class SecondDungeon : DUNGEON
{

    // Use this for initialization
    void Awake()
    {
        AwakeDungeon(second);

        enemyList.Add(new EnemyKind[] { slime });
        enemyList.Add(new EnemyKind[] { slime, slime });
        enemyList.Add(new EnemyKind[] { goblin });
        enemyList.Add(new EnemyKind[] { slime, slime, slime });
        enemyList.Add(new EnemyKind[] { slime, goblin, slime });
    }

    // Use this for initialization
    void Start()
    {
        StartDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDungeon();
    }

    void FixedUpdate()
    {
        FixedUpdateDungeon();
    }
}