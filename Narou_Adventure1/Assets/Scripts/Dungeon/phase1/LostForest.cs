using System.Collections;
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
        AwakeDungeon(lost_forest);
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
