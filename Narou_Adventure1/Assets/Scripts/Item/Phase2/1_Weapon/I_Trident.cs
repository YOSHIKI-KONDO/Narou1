using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Trident : ITEM
{
    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.trident, 1,1,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -190));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 95));
        EffectLists.Add(new Dealing(AbilityKind.beginner_spearmanship, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(AbilityKind.beginner_spearmanship, Dealing.A_ParaKind.trainRate, 0.5));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 12));

        SetSource(NeedKind.weapon, NeedKind.spear);
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
