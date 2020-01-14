using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class LeaveTheTown : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return (main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.warrior_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.tamer_school] >= 1) &&
               (main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.thank_you] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.ill_get_you_for_this] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.leave_the_town, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.inventorySpace, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Item_Dealing(ItemKind.charm));
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
