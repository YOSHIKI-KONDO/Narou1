﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Ip6down : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.itemPoint7] >= 1;
    }

    // Use this for initialization
/*    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.ip6down);
        instant.initCostList.Add(new Dealing(ResourceKind.itemPoint7, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.itemPoint6, Dealing.R_ParaKind.current, 6));
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
    }*/
}
