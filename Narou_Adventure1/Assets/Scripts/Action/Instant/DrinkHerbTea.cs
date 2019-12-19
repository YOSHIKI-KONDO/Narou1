using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class DrinkHerbTea : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Max((int)ResourceKind.research) >= 50 &&
               main.rsc.Value[(int)ResourceKind.herb] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.drink_herb_tea);
        instant.initCostList.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 2));
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
