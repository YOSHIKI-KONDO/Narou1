using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_WaterRod : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.water_rod, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -200));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 100));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 5));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 5));

        SetSource(NeedKind.goods, NeedKind.water);
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
