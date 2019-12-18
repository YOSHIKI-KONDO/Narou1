using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_StoneSpear : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.stone_spear, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -150));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 75));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 8));

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
