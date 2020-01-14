using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_SandwichStall : ITEM
{
    public override bool Requires()
    {
        return main.itemCtrl.exitSourceNums[(int)NeedKind.kiln] > 0;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.sandwich_stall, 5,1,3,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -400));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 200));
        EffectLists.Add(new Dealing(ResourceKind.dough, Dealing.R_ParaKind.max, 20));
        EffectLists.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.max, 10));
        EffectLists.Add(new Dealing(ResourceKind.anchovy_sandwich, Dealing.R_ParaKind.max, 10));

        SetSource(NeedKind.goods, NeedKind.stall); 
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
