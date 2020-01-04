using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class EdgeOfTown : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.punish_the_bad_kids] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(edge_of_town, ResourceKind.itemPoint1,1);
        enemyList.Add(new EnemyKind[] { sigurd });
        enemyList.Add(new EnemyKind[] { askr, embla });

        drops.Add(new Drop(ResourceKind.stone, 1, 100));
        firstDrops.Add(new Drop(ResourceKind.gold, 15, 100));
        firstDrops.Add(new Drop(ResourceKind.filet_o_fish, 1, 100));
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
