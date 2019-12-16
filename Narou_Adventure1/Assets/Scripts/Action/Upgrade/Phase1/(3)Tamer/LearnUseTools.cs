using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class LearnUseTools : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.research] >= 3;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.learn_use_tools, 1,20);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.4));
        progress.completeEffectList.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 1));
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
