using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class OakForest : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(oak_forest, ResourceKind.itemPoint1, 22);
        enemyList.Add(new EnemyKind[] { bird, bird, bird });
        enemyList.Add(new EnemyKind[] { wolf, wolf });
        enemyList.Add(new EnemyKind[] { wolf, bird, rat });
        enemyList.Add(new EnemyKind[] { bird, rat, rat });
        enemyList.Add(new EnemyKind[] { harpy, bird });//5
        enemyList.Add(new EnemyKind[] { rat, rat, rat });
        enemyList.Add(new EnemyKind[] { bird, bird, wolf, rat });
        enemyList.Add(new EnemyKind[] { wolf, wolf, rat, rat });
        enemyList.Add(new EnemyKind[] { wolf, werewolf, wolf });
        enemyList.Add(new EnemyKind[] { harpy, harpy,  });//10

        //progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

        drops.Add(new Drop(ResourceKind.wood, 2, 100));
        drops.Add(new Drop(ResourceKind.herb, 6, 60));
        firstDrops.Add(new Drop(ResourceKind.wood, 4, 100));
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
