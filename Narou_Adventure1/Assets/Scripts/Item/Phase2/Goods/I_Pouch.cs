using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Pouch : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.pouch, 2,5);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -70));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 35));
        EffectLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.max, 100));

        SetSource(NeedKind.goods);
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
