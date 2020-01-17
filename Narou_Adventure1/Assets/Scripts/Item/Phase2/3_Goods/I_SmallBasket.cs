using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_SmallBasket : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.delivery_of_fur] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.small_basket, 1,null,1,20,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -40));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 20));
        EffectLists.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.max, 4));
        EffectLists.Add(new Dealing(ResourceKind.anchovy_sandwich, Dealing.R_ParaKind.max, 2));
        EffectLists.Add(new Dealing(ResourceKind.filet_o_fish, Dealing.R_ParaKind.max, 2));

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
