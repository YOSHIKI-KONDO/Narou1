using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class RuneAugmentation : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.talk_fatherD] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.rune_augmentation, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.current, -5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.max, 5));
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
