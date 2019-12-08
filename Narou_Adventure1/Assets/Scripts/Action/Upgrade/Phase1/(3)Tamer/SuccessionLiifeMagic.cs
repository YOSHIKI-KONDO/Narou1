using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SuccessionLiifeMagic : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Max((int)ResourceKind.mp) >= 3 &&
               main.rsc.Value[(int)ResourceKind.research] >= 15;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.succession_life_magic, 1,30);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3));
        progress.progressCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.4));
        progress.completeEffectList.Add(new Dealing(AbilityKind.life_magic, Dealing.A_ParaKind.maxLevel, 1));
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
