using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class TamerSchool : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.MaxLevel((int)AbilityKind.animal_handling) >= 6;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.warrior_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.tamer_school, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -70));
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -90));
        progress.completeEffectList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.trainRate, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.life_magic, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 200));
        progress.completeEffectList.Add(new Dealing(ResourceKind.inventorySpace, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Item_Dealing(ItemKind.tamer_textbook));
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
