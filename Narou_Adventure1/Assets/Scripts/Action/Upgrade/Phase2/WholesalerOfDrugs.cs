using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class WholesalerOfDrugs : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.entrance_ceremony] >= 1 &&
               (main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.tamer_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1 ||
               main.SR.clearNum_Dungeon[(int)DungeonKind.sewer] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.delivery_of_fur] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.wholesaler_of_drugs, 3, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.medicine, Dealing.R_ParaKind.current, -5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 65));
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
