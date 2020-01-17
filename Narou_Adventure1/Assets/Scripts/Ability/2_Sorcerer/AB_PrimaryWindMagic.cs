using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_PrimaryWindMagic : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.primary_wind_magic) >= 6 ||
        main.itemCtrl.exitSourceNums[(int)NeedKind.wind] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.primary_wind_magic, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -100));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 4));

        need.AddSourceNeed(NeedKind.wind);

        SetSource(NeedKind.wind);

        unlocks.Add(new SkillUnlock(1, SkillKind.air_cutter));
        unlocks.Add(new SkillUnlock(4, SkillKind.create_firewood));
        unlocks.Add(new SkillUnlock(7, SkillKind.air_bazooka));
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
