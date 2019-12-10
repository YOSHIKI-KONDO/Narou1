using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_WindOrb : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.wind_orb, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -50));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 25));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.max, 2));

        SetSource(NeedKind.wind);
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
