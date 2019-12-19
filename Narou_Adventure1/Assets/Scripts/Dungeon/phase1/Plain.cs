using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class Plain : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(plain);
        enemyList.Add(new EnemyKind[] { slime });
        enemyList.Add(new EnemyKind[] { bird, slime });
        enemyList.Add(new EnemyKind[] { goblin });
        enemyList.Add(new EnemyKind[] { slime, slime });
        enemyList.Add(new EnemyKind[] { goblin, goblin });//5
        enemyList.Add(new EnemyKind[] { slime, slime, slime });
        enemyList.Add(new EnemyKind[] { bird });
        enemyList.Add(new EnemyKind[] { snake });
        enemyList.Add(new EnemyKind[] { goblin, bird });
        enemyList.Add(new EnemyKind[] { bird, goblin, slime });//10
        enemyList.Add(new EnemyKind[] { slime, slime });
        enemyList.Add(new EnemyKind[] { goblin, slime });
        enemyList.Add(new EnemyKind[] { slime, snake });
        enemyList.Add(new EnemyKind[] { bird, bird });
        enemyList.Add(new EnemyKind[] { snake, snake });//15
        enemyList.Add(new EnemyKind[] { slime });
        enemyList.Add(new EnemyKind[] { bird });
        enemyList.Add(new EnemyKind[] { slime, bird, slime });
        enemyList.Add(new EnemyKind[] { snake, bird });
        enemyList.Add(new EnemyKind[] { orc });//20

        drops.Add(new Drop(ResourceKind.herb, 3, 100));
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
