using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_BeginnerBojutsu : ABILITY
{

    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.beginner_bojutsu) >= 6 ||
        main.itemCtrl.exitSourceNums[(int)NeedKind.rod] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeAbility(AbilityKind.beginner_bojutsu, 30, 1.2);
        progress.unlockCostList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, -100));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.regen, 0.02));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 4));

        need.AddSourceNeed(NeedKind.rod);

        SetSource(NeedKind.rod);

        unlocks.Add(new SkillUnlock(1, SkillKind.left_small_swing));
        unlocks.Add(new SkillUnlock(4, SkillKind.knead_dough));
        unlocks.Add(new SkillUnlock(7, SkillKind.parry));
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
