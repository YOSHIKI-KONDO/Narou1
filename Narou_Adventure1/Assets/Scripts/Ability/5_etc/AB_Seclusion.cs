using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_Seclusion : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.seclusion] >= 5 ||
               ((main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.pick_flowers] >= 1 ||
               main.rsc.Value[(int)ResourceKind.research] <= 5) &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.entrance_ceremony] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.seclusion, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -50));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.dodge, Dealing.R_ParaKind.status, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 2));

        unlocks.Add(new LoopUnlock(1, MainAction.ActionEnum.Loop.pickpocket));
    }

	// Use this for initialization
	void Start () {
        StartAbility();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAbility();
	}

    private void FixedUpdate()
    {
        FixedUpdateAbility();
    }
}
