﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class DemonicCellar : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.in_cellar] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(demonic_cellar, ResourceKind.itemPoint1, 17);
        enemyList.Add(new EnemyKind[] { lizard_man, rat, rat });//1
        enemyList.Add(new EnemyKind[] { bat, bat });
        enemyList.Add(new EnemyKind[] { rat, rat, rat });
        enemyList.Add(new EnemyKind[] { werewolf, werewolf });//4
        enemyList.Add(new EnemyKind[] { bat, bat, rat, rat });
        enemyList.Add(new EnemyKind[] { rat, bat, rat });
        enemyList.Add(new EnemyKind[] { lizard_man, werewolf, rat, rat });//7
        enemyList.Add(new EnemyKind[] { bat, bat, bat });
        enemyList.Add(new EnemyKind[] { lizard_man, bat, rat });
        enemyList.Add(new EnemyKind[] { demonic_warrior });//10

        //progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

        drops.Add(new Drop(ResourceKind.gold, 20, 100));
        drops.Add(new Item_Drop(ItemKind.fire_ruby, 1));
        firstDrops.Add(new Drop(ResourceKind.book, 1, 100));
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
