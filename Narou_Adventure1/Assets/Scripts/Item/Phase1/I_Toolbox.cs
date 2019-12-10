using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_Toolbox : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.toolbox, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -40));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 20));
        EffectLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.max, 5));
        EffectLists.Add(new Dealing(AbilityKind.use_tools, Dealing.A_ParaKind.maxLevel, 1));
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
