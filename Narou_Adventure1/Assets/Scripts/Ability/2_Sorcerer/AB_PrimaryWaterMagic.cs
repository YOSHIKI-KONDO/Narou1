﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_PrimaryWaterMagic : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.primary_water_magic) >= 6 ||
        main.itemCtrl.exitSourceNums[(int)NeedKind.water] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.primary_water_magic, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -100));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 4));

        need.AddSourceNeed(NeedKind.water);

        SetSource(NeedKind.water);

        unlocks.Add(new SkillUnlock(1, SkillKind.waterball));
        unlocks.Add(new SkillUnlock(4, SkillKind.catch_fish));
        unlocks.Add(new SkillUnlock(7, SkillKind.cure_water));
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