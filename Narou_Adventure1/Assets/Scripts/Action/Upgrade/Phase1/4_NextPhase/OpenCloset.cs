using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class OpenCloset : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.shed] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.lost_forest] >= 1;
    }

    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.open_closet, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        progress.completeEffectList.Add(new Item_Dealing(ItemKind.old_robe));
    }

	// Use this for initialization
	void Start () {
        StartUpgradeAction();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUpgradeAction();
	}

    void FixedUpdate()
    {
        FixedUpdateUpgradeAction();
    }
}
