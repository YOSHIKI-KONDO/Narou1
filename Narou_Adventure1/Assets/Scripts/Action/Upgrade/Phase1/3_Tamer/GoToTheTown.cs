using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class GoToTheTown : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.talk_fatherA] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }
    public override void CompleteAction()
    {
        main.analytics.TutorialComplete();
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.go_to_the_town, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -15));
        progress.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.max, 10));
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
