using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_WindOrb : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.mothers_den] >= 1 &&
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.buy_bag] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.wind_orb, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -35));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 17));
        EffectLists.Add(new Dealing(ResourceKind.magic_attack, Dealing.R_ParaKind.status, 2));

        SetSource(NeedKind.goods, NeedKind.wind);
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
