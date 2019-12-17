using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SellFirewood : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.wood] >=1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.sell_firewood);
        instant.initCostList.Add(new Dealing(ResourceKind.firewood, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 2));
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
