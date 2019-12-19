﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Umbrella : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.umbrella, 1,12);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -30));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 15));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 1));
        EffectLists.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 1));

        SetSource(NeedKind.weapon, NeedKind.sword, NeedKind.spear, NeedKind.rod, NeedKind.shield, NeedKind.water);
    }

    // Use this for initialization
    void Start()
    {
        StartItem();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItem();
    }

    void FixedUpdate()
    {
        FixedUpdateItem();
    }
}