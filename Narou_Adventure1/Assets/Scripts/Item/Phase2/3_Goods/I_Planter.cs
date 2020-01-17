using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Planter : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.delivery_of_fur] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.planter, 1,5,1,20,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -30));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 15));
        EffectLists.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.regen, 0.01));
        EffectLists.Add(new Dealing(ResourceKind.wheat, Dealing.R_ParaKind.regen, 0.01));
        EffectLists.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.max, 6));
        EffectLists.Add(new Dealing(ResourceKind.wheat, Dealing.R_ParaKind.max, 6));

        SetSource(NeedKind.goods);
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
