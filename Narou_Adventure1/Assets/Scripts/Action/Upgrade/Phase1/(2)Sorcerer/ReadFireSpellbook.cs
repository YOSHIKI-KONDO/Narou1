using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ReadFireSpellbook : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.MaxLevel( (int)AbilityKind.primary_water_magic) >= 6 &&
               main.a_rsc.MaxLevel( (int)AbilityKind.primary_earth_magic) >= 6 &&
               main.rsc.Value[ (int)ResourceKind.research] >= 40;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.read_fire_spellbook, 1,10);
        progress.progressCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.9));
        progress.completeEffectList.Add(new Dealing(AbilityKind.primary_fire_magic, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.primary_fire_magic, Dealing.A_ParaKind.trainRate, 0.5));
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
