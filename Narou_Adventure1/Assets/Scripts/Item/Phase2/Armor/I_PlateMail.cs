using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_PlateMail : ITEM
{

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.plate_mail, 1,1);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -600));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 300));
        EffectLists.Add(new Dealing(ResourceKind.defense, Dealing.R_ParaKind.status, 30));

        SetSource(NeedKind.armor);
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
