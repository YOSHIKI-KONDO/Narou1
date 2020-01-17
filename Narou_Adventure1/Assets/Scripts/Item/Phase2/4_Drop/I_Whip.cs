using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Whip : ITEM
{
    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.whip, 1,1,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -160));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 80));
        EffectLists.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.trainRate, 1));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 4));

        SetSource(NeedKind.goods, NeedKind.animal);
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
