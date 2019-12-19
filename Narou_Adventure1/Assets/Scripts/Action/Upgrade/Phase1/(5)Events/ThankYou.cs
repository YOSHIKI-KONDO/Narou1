using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ThankYou : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.small_hill] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.punish_the_bad_kids] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.thank_you, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.flower, Dealing.R_ParaKind.current, -1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 30));
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
