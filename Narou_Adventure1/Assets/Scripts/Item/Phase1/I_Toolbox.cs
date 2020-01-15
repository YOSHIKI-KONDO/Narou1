using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Toolbox : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.learn_use_tools] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.buy_bag] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.toolbox, 1,1,1,1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -40));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 20));
        EffectLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.max, 5));
        EffectLists.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 1));

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
