﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_Hoodlum : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.hoodlum] >= 5 ||
               ((main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.punish_the_bad_kids] >= 1 ||
               main.rsc.Value[(int)ResourceKind.research] <= 5) &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.entrance_ceremony] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.hoodlum, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -50));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.max, 0.1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, -2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.stone, Dealing.R_ParaKind.max, 2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 2));

        unlocks.Add(new InstantUnlock(1, MainAction.ActionEnum.Instant.mugged));
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