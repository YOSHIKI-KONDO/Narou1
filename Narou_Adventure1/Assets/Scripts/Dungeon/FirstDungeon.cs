﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class FirstDungeon : DUNGEON {

    public override void FirstClearAction()
    {
        main.announce.Add("This is first time to clear this dungeon!");
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(firstDungeon);
        enemyList.Add(new EnemyKind[] { slime });
        enemyList.Add(new EnemyKind[] { slime, slime });
        enemyList.Add(new EnemyKind[] { goblin });
        enemyList.Add(new EnemyKind[] { slime, slime, slime });
        enemyList.Add(new EnemyKind[] { slime, goblin, slime });

        progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.5f));

        drops.Add(new Drop(ResourceKind.gold, 3, 100));
        drops.Add(new Drop(ResourceKind.herb, 3, 50));
        drops.Add(new Item_Drop(ItemKind.woodspear, 100));
        drops.Add(new Item_Drop(ItemKind.earth_orb, 10));

        firstDrops.Add(new Drop(ResourceKind.bread, 1, 100));
        firstDrops.Add(new Item_Drop(ItemKind.toolbox, 100));
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
