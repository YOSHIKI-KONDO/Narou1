﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_EarthOrb : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.earth_orb, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -50));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 25));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 2));

        SetSource(NeedKind.goods, NeedKind.earth);
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