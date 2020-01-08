using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class DeskWork : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.sorcerer_school] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.desk_work, 20,1);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.3));
        progress.progressCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.3));
        progress.progressEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 0.6));
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
