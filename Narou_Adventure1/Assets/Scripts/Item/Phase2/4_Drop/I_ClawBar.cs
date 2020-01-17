using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_ClawBar : ITEM
{
    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.claw_bar, 1,1,2,1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -160));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 80));
        EffectLists.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(AbilityKind.life_magic, Dealing.A_ParaKind.maxLevel, 1));
        EffectLists.Add(new Dealing(ResourceKind.attack, Dealing.R_ParaKind.status, 6));

        SetSource(NeedKind.weapon, NeedKind.rod);
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
