using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_SeaBreezeAmulet : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.hoarding_house] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.wholesaler_of_drugs] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.sea_breeze_amulet, 2,1,3,1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -480));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 240));
        EffectLists.Add(new Dealing(ResourceKind.defense, Dealing.R_ParaKind.status, 2));
        EffectLists.Add(new Dealing(AbilityKind.primary_water_magic, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.regen, 0.1));
        EffectLists.Add(new Dealing(AbilityKind.primary_wind_magic, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.regen, 0.1));

        SetSource(NeedKind.goods, NeedKind.water, NeedKind.wind);
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
