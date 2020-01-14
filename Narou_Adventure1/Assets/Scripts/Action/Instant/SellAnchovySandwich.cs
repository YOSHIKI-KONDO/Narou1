using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SellAnchovySandwich : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.itemCtrl.exitSourceNums[(int)NeedKind.stall] > 0;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.sell_anchovy_sandwich);
        instant.initCostList.Add(new Dealing(ResourceKind.anchovy_sandwich, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 25));
        instant.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));

        need.AddSourceNeed(NeedKind.stall);
    }

	// Use this for initialization
	void Start ()
    {
        StartInstantAction();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateInstantAction();
	}

    void FixedUpdate()
    {
        FixedUpdateInstantAction();
    }
}
