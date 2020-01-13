using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_Pharmacist : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.pharmacist) >= 6 ||
               main.itemCtrl.exitSourceNums[(int)NeedKind.medic] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.doctor, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -100));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.4));
        progress.completeEffectList.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.medicine, Dealing.R_ParaKind.max, 2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.potion, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 5));

        need.AddSourceNeed(NeedKind.medic);

        unlocks.Add(new LoopUnlock(1, MainAction.ActionEnum.Loop.dispense_medicines));
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
