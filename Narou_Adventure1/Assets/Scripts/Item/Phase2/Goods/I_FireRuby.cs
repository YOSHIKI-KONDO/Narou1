using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_FireRuby : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.fire_ruby, 4,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -500));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 250));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 5));
        EffectLists.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.max, 3));
        EffectLists.Add(new Dealing(AbilityKind.primary_fire_magic, Dealing.A_ParaKind.maxLevel, 2));

        SetSource(NeedKind.goods, NeedKind.fire);
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
