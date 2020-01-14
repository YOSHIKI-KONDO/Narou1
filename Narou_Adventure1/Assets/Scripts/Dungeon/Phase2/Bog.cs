using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class Bog : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(bog, ResourceKind.itemPoint1, 1);
        enemyList.Add(new EnemyKind[] { goblin, goblin, goblin });
        enemyList.Add(new EnemyKind[] { snake, snake, snake });
        enemyList.Add(new EnemyKind[] { goblin, snake, snake });
        enemyList.Add(new EnemyKind[] { goblin, goblin });
        enemyList.Add(new EnemyKind[] { lizard_man });//5
        enemyList.Add(new EnemyKind[] { snake, snake });
        enemyList.Add(new EnemyKind[] { snake, goblin });
        enemyList.Add(new EnemyKind[] { goblin, orc, goblin });
        enemyList.Add(new EnemyKind[] { goblin, goblin, snake });
        enemyList.Add(new EnemyKind[] { lizard_man, lizard_man });//10

        //progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

        drops.Add(new Drop(ResourceKind.gold, 5, 100));
        drops.Add(new Drop(ResourceKind.filet_o_fish, 1, 100));
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
