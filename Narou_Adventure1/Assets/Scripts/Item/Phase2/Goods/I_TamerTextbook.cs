using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_TamerTextbook : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.tamer_textbook, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -600));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 300));
        EffectLists.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 10));
        EffectLists.Add(new Dealing(AbilityKind.animal_handling, Dealing.A_ParaKind.maxLevel, 2));
        EffectLists.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 2));
        EffectLists.Add(new Dealing(AbilityKind.life_magic, Dealing.A_ParaKind.maxLevel, 2));

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
