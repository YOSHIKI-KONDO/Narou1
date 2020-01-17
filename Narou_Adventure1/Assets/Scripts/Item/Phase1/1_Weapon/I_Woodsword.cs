using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Woodsword : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.fathers_gym] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.buy_bag] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.woodsword, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -30));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 15));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 4));

        SetSource(NeedKind.weapon, NeedKind.sword);
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
