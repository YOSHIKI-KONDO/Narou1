using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_DarknessSword : ITEM
{
    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.darkness_sword, 1,1,3,50,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -600));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 300));
        EffectLists.Add(new Dealing(ResourceKind.dark, Dealing.R_ParaKind.regen, 0.1));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 18));

        SetSource(NeedKind.weapon, NeedKind.sword, NeedKind.dark);
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
