using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Python : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.python, 2, 2);
        BuyLists.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -10));
        SellLists.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, 5));
        EffectLists.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 10));

        SetSource(NeedKind.fire, NeedKind.water);

        need.AddItemNeed(ItemKind.c);
        need.AddSourceNeed(NeedKind.sword);
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
