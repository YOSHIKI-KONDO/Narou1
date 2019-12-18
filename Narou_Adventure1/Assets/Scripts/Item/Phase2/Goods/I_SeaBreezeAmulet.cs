﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_SeaBreezeAmulet : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.sea_breeze_amulet, 3,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -500));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 250));
        EffectLists.Add(new Dealing(ResourceKind.defense, Dealing.R_ParaKind.status, 2));
        EffectLists.Add(new Dealing(AbilityKind.primary_water_magic, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.regen, 0.1));
        EffectLists.Add(new Dealing(AbilityKind.primary_wind_magic, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.regen, 0.1));

        SetSource(NeedKind.goods, NeedKind.water, NeedKind.wind);
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
