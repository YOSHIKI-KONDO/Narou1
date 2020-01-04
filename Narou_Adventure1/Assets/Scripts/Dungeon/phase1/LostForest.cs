﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class LostForest : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.plain] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(lost_forest, ResourceKind.itemPoint1, 1);
        enemyList.Add(new EnemyKind[] { goblin });
        enemyList.Add(new EnemyKind[] { bird, bird });
        enemyList.Add(new EnemyKind[] { goblin, wolf });
        enemyList.Add(new EnemyKind[] { wolf, wolf });
        enemyList.Add(new EnemyKind[] { goblin, goblin });//5
        enemyList.Add(new EnemyKind[] { snake, snake });
        enemyList.Add(new EnemyKind[] { wolf, bird });
        enemyList.Add(new EnemyKind[] { goblin, snake });
        enemyList.Add(new EnemyKind[] { wolf, wolf, bird });
        enemyList.Add(new EnemyKind[] { goblin, goblin, snake });//10
        enemyList.Add(new EnemyKind[] { wolf, wolf });
        enemyList.Add(new EnemyKind[] { goblin, wolf });
        enemyList.Add(new EnemyKind[] { wolf, snake });
        enemyList.Add(new EnemyKind[] { bird, bird, bird });
        enemyList.Add(new EnemyKind[] { harpy });//15
        enemyList.Add(new EnemyKind[] { snake, bird });
        enemyList.Add(new EnemyKind[] { bird });
        enemyList.Add(new EnemyKind[] { goblin, bird });
        enemyList.Add(new EnemyKind[] { wolf, wolf, wolf });
        enemyList.Add(new EnemyKind[] { werewolf });//20

        drops.Add(new Drop(ResourceKind.wood, 3, 100));
        firstDrops.Add(new Drop(ResourceKind.research, 20, 100));
    }

	// Use this for initialization
	void Start () {
        StartDungeon();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateDungeon();
	}

    void FixedUpdate()
    {
        FixedUpdateDungeon();   
    }
}
