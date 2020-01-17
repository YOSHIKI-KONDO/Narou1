using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_LifeMagic : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.life_magic) >= 6;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.life_magic, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -50));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.4));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 0.1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.regen, 0.02));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 5));

        unlocks.Add(new LoopUnlock(1, MainAction.ActionEnum.Loop.grow_herb));
        unlocks.Add(new InstantUnlock(4, MainAction.ActionEnum.Instant.rune_generation));
        unlocks.Add(new InstantUnlock(7, MainAction.ActionEnum.Instant.runic_carving));
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
