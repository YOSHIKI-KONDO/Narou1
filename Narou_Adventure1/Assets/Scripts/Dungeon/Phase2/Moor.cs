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
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.pick_flowers] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(moor);
        enemyList.Add(new EnemyKind[] { bird, bird, slime });
        enemyList.Add(new EnemyKind[] { wolf, wolf, slime });
        enemyList.Add(new EnemyKind[] { rat, rat, rat, slime });
        enemyList.Add(new EnemyKind[] { bird, rat, slime });
        enemyList.Add(new EnemyKind[] { poison_rat, rat, rat });//5
        enemyList.Add(new EnemyKind[] { wolf, bird, rat });
        enemyList.Add(new EnemyKind[] { slime, slime, rat, rat });
        enemyList.Add(new EnemyKind[] { bird, bird });
        enemyList.Add(new EnemyKind[] { wolf, wolf, rat });
        enemyList.Add(new EnemyKind[] { harpy, bird, bird });//10
        enemyList.Add(new EnemyKind[] { wolf, bird, rat, slime });
        enemyList.Add(new EnemyKind[] { wolf, rat, rat });
        enemyList.Add(new EnemyKind[] { bird, wolf, slime });
        enemyList.Add(new EnemyKind[] { wolf, wolf, slime, slime });
        enemyList.Add(new EnemyKind[] { poison_rat, harpy });//15

        progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

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
