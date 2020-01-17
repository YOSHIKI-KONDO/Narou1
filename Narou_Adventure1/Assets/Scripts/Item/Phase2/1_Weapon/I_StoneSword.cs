﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_StoneSword : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.stone_sword, 1,1,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -100));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 50));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 10));

        SetSource(NeedKind.weapon, NeedKind.sword);
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
