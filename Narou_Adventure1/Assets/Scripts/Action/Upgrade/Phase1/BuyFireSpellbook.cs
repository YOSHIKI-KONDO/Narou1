using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BuyFireSpellbook : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.rune_augmentation] >= 1 &&
               main.a_rsc.MaxLevel( (int)AbilityKind.primary_water_magic) >= 6 &&
               main.rsc.Value[ (int)ResourceKind.firewood] >= 10;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.buy_fire_spellbook, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -40));
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
