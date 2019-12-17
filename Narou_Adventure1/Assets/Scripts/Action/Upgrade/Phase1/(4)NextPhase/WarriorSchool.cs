using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class WarriorSchool : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.beginner_swordmanship) >= 6 ||
               main.a_rsc.MaxLevel((int)AbilityKind.beginner_spearmanship) >= 6;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.warrior_school, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -90));
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -30));
        progress.completeEffectList.Add(new Dealing(AbilityKind.beginner_swordmanship, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.beginner_spearmanship, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.beginner_bojutsu, Dealing.A_ParaKind.maxLevel, 1));
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
