using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Item_Dealing : Dealing
{
    public ItemKind itemKind;
    public int amount;          //まだ使用していない
    public float probability;   //まだ使用していない
    public Item_Dealing(ItemKind kind) : base(null, null, 1)
    {
        this.itemKind = kind;
    }
}
