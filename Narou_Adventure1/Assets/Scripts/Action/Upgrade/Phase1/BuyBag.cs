﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BuyBag : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.use_tools] >= 1 || main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.buy_bag, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -50));
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
