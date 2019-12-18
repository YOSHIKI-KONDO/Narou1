using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Rosary : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.rosary, 5,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -500));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 250));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 2));
        EffectLists.Add(new Dealing(ResourceKind.defense, Dealing.R_ParaKind.status, 3));
        EffectLists.Add(new Dealing(AbilityKind.believer, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(AbilityKind.believer, Dealing.A_ParaKind.trainRate, 0.5));
        EffectLists.Add(new Dealing(ResourceKind.light, Dealing.R_ParaKind.regen, 0.1));

        SetSource(NeedKind.goods, NeedKind.light);
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
