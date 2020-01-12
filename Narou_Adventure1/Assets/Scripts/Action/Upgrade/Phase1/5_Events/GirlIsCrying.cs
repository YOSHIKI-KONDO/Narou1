using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class GirlIsCrying : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return (main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_swordmanship] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_spearmanship] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_bojutsu] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_wind_magic] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_earth_magic] >= 1 ||
               main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 1) ||
               (main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.warrior_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.tamer_school] >= 1);
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.girl_is_crying, 1, 0, null, false, false);
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 10));
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
