using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Item_Drop : Drop {
    public ItemKind itemKind;
    
    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public Item_Drop(ItemKind itemKind, float probability):base(ResourceKind.nothing,1,probability)
    {
        this.itemKind = itemKind;
    }
}
