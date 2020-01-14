using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Ip3up : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.itemPoint2] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.ip3up);
        instant.initCostList.Add(new Dealing(ResourceKind.itemPoint2, Dealing.R_ParaKind.current, -10));
        instant.completeEffectList.Add(new Dealing(ResourceKind.itemPoint3, Dealing.R_ParaKind.current, 1));
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
