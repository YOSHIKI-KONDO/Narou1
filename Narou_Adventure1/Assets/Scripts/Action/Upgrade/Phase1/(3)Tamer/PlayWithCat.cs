﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class PlayWithCat : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Max((int)ResourceKind.stamina) >= 5;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.play_with_cat, 1,10);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.maxLevel, 1));
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
