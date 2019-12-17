﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_BeginnerSpearmanship : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.beginner_spearmanship) >= 6;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.beginner_spearmanship, 50, 1);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -1));
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 0.05));

        need.AddSourceNeed(NeedKind.spear);
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
