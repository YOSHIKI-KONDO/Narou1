using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ApprenticeTamer : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.tamer_school] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.entrance_ceremony] >= 1 &&
               main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 8;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.warrior_school] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.apprentice_tamer, 1,0,0);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -300));
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -300));
        progress.completeEffectList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.regen, 0.1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.life_magic, Dealing.A_ParaKind.maxLevel, 1));
        progress.completeEffectList.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.beast_tamer, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.insect_handling, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.bird_handling, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.elementalor, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.summon_fairy, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(AbilityKind.summon_familiar, Dealing.A_ParaKind.trainRate, 0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.ap, Dealing.R_ParaKind.current, 100));
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
