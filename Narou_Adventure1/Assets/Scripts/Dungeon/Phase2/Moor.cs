using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class Moor : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.oak_forest] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(moor, ResourceKind.itemPoint1, 1);
        enemyList.Add(new EnemyKind[] { wolf, wolf, snake });
        enemyList.Add(new EnemyKind[] { rat, rat, snake });
        enemyList.Add(new EnemyKind[] { poison_rat });
        enemyList.Add(new EnemyKind[] { wolf, bird, rat });
        enemyList.Add(new EnemyKind[] { snake, snake, rat, rat });//5
        enemyList.Add(new EnemyKind[] { bird, bird });
        enemyList.Add(new EnemyKind[] { poison_rat, bird, bird });
        enemyList.Add(new EnemyKind[] { wolf, bird, rat, snake });
        enemyList.Add(new EnemyKind[] { wolf, wolf, snake, snake });
        enemyList.Add(new EnemyKind[] { poison_rat, poison_rat, poison_rat });//10

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
