using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_AnimalHandling : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.animal_handling) >= 6 ||
        main.itemCtrl.exitSourceNums[(int)NeedKind.animal] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.animal_handling, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -100));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.animal, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 6));

        need.AddSourceNeed(NeedKind.animal);

        SetSource(NeedKind.animal);

        unlocks.Add(new SkillUnlock(1, SkillKind.animal_attack));
        unlocks.Add(new SkillUnlock(4, SkillKind.picking_up_coins));
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
