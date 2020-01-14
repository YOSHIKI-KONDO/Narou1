using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Kiln : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.kiln, 1,2,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -150));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 75));
        EffectLists.Add(new Dealing(ResourceKind.firewood, Dealing.R_ParaKind.regen, -0.02));
        EffectLists.Add(new Dealing(ResourceKind.charcoal, Dealing.R_ParaKind.regen, 0.02));
        EffectLists.Add(new Dealing(ResourceKind.charcoal, Dealing.R_ParaKind.max, 10));
        EffectLists.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.max, 10));

        SetSource(NeedKind.goods, NeedKind.kiln);
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
