using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Cutlass : ITEM
{
    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.cutlass, 1,1,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -180));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 90));
        EffectLists.Add(new Dealing(AbilityKind.beginner_swordmanship, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 14));

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
