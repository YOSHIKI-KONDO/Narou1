using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// Narou1でよく使う関数
/// </summary>
public class Narou1Func : BASE {
    public static Color color_pink = new Color(1f, 0.6f, 0.6f, 1f);

	public static ResourceKind ItemPointKind(int rarity)
    {
        if (rarity <= 0 || rarity >= 11) { return ResourceKind.nothing; }
        switch (rarity)
        {
            case 1:
                return ResourceKind.itemPoint1;
            case 2:
                return ResourceKind.itemPoint2;
            case 3:
                return ResourceKind.itemPoint3;
            case 4:
                return ResourceKind.itemPoint4;
            case 5:
                return ResourceKind.itemPoint5;
            case 6:
                return ResourceKind.itemPoint6;
            case 7:
                return ResourceKind.itemPoint7;
            case 8:
                return ResourceKind.itemPoint8;
            case 9:
                return ResourceKind.itemPoint9;
            case 10:
                return ResourceKind.itemPoint10;
            default:
                return ResourceKind.nothing;
        }
    }
}
