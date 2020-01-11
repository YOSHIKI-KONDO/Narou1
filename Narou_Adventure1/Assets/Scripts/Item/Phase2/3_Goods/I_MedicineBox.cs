using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_MedicineBox : ITEM
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeItem(ItemKind.medicine_box, 2,5,2,30,2);
        BuyLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -120));
        SellLists.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 60));
        EffectLists.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.max, 10));
        EffectLists.Add(new Dealing(ResourceKind.medicine, Dealing.R_ParaKind.max, 5));
        EffectLists.Add(new Dealing(ResourceKind.potion, Dealing.R_ParaKind.max, 2));

        SetSource(NeedKind.goods, NeedKind.medic);
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
