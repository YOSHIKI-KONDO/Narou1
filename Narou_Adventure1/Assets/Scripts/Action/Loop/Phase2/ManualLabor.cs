using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ManualLabor : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.warrior_school] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.manual_labor, 10,1);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.6));
        progress.progressEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.current, 1));
    }

	// Use this for initialization
	void Start () {
        StartLoopAction();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLoopAction();
	}

    void FixedUpdate()
    {
        FixedUpdateLoopAction();
    }
}
