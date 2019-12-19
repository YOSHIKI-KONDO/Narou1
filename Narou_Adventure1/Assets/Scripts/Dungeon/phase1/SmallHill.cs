using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class SmallHill : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.pick_flowers] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(small_hill);
        enemyList.Add(new EnemyKind[] { slime });
        enemyList.Add(new EnemyKind[] { slime, slime });
        enemyList.Add(new EnemyKind[] { bat });
        enemyList.Add(new EnemyKind[] { slime, bat });
        enemyList.Add(new EnemyKind[] { wolf, wolf });

        drops.Add(new Drop(ResourceKind.research, 20, 100));
        firstDrops.Add(new Drop(ResourceKind.flower, 1, 100));
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
