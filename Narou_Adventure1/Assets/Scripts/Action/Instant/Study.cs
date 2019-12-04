using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Study : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.codices] >= 10;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.study);
        instant.initCostList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.max, 1));
    }

    // Use this for initialization
    void Start()
    {
        StartInstantAction();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInstantAction();
    }

    void FixedUpdate()
    {
        FixedUpdateInstantAction();
    }
}
