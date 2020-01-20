using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class HoardingHouse : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.house_clean_up] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(hoarding_house, ResourceKind.itemPoint1, 12);
        enemyList.Add(new EnemyKind[] { rat });//1
        enemyList.Add(new EnemyKind[] { rat, bat });
        enemyList.Add(new EnemyKind[] { rat, rat });
        enemyList.Add(new EnemyKind[] { red_slime });//4
        enemyList.Add(new EnemyKind[] { bat, rat, rat });
        enemyList.Add(new EnemyKind[] { snake, bat, rat });
        enemyList.Add(new EnemyKind[] { red_slime, rat, rat });//7
        enemyList.Add(new EnemyKind[] { bat, bat, snake });
        enemyList.Add(new EnemyKind[] { snake, snake, slime });
        enemyList.Add(new EnemyKind[] { red_slime, red_slime });//10

        //progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

        drops.Add(new Drop(ResourceKind.gold, 10, 100));
        drops.Add(new Drop(ResourceKind.dough, 1, 60));
        firstDrops.Add(new Item_Drop(ItemKind.leather_vest, 100));
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
