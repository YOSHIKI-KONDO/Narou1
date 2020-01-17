using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_WindRod : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.wind_rod, 1,1,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -130));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 65));
        EffectLists.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.max, 1));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 6));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 6));

        SetSource(NeedKind.weapon, NeedKind.rod, NeedKind.wind);
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
