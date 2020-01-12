using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class InCellar : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.hoarding_house] >= 1 &
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.desolate_room] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.in_cellar, 1, 0, null, false, false);
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 20));
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
