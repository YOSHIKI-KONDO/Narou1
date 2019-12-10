using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Woodstick : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.woodstick, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -20));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 10));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.max, 1));

        SetSource(NeedKind.rod);
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
