using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ReadEarthSpellbook : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.mothers_den] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }
    public override void CompleteAction()
    {
        main.SR.released_element[(int)ElementKind.main] = true;
        main.SR.released_element[(int)ElementKind.ability] = true;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.read_earth_spellbook, 1,10);
        progress.progressCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.9));
        progress.completeEffectList.Add(new Dealing(AbilityKind.primary_earth_magic, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.primary_earth_magic, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 5));
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
