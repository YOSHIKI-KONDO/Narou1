using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class HouseCleanUp : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.entrance_ceremony] >= 1 &&
               (main.SR.clearNum_Dungeon[(int)DungeonKind.sewer] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.wholesaler_of_drugs] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.delivery_of_fur] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.house_clean_up, 1, 0, null, false, false);
        progress.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 20));
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
