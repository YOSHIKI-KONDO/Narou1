using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class IntoADormitory : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.lost_forest] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.into_a_dormitory, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -10));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 20));
        progress.completeEffectList.Add(new Dealing(ResourceKind.equipSpace, Dealing.R_ParaKind.max, 5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.inventorySpace, Dealing.R_ParaKind.max, 5));
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
