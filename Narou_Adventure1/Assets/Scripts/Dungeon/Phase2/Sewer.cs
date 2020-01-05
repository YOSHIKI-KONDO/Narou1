using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static DungeonKind;
using static EnemyKind;

public class Sewer : DUNGEON
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.get_rid_of_rat] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeDungeon(sewer, ResourceKind.itemPoint1, 1);
        enemyList.Add(new EnemyKind[] { rat, rat, rat, rat });
        enemyList.Add(new EnemyKind[] { red_slime });//2
        enemyList.Add(new EnemyKind[] { bat, bat });
        enemyList.Add(new EnemyKind[] { poison_rat });//4
        enemyList.Add(new EnemyKind[] { rat, rat, rat });
        enemyList.Add(new EnemyKind[] { red_slime, bat });//6
        enemyList.Add(new EnemyKind[] { rat, rat, rat });
        enemyList.Add(new EnemyKind[] { poison_rat, rat, rat });//8
        enemyList.Add(new EnemyKind[] { bat, bat, bat });
        enemyList.Add(new EnemyKind[] { ghoul, ghoul });//10

        progressCost.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3f));

        drops.Add(new Drop(ResourceKind.gold, 5, 100));
        drops.Add(new Drop(ResourceKind.filet_o_fish, 1, 100));
        firstDrops.Add(new Drop(ResourceKind.gold, 60, 100));
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
