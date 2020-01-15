using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class TalkFatherD : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.talk_fatherC] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.talk_fatherD, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -16));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 5));
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
