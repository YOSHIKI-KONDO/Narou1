using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class SaveR
{
    public string ascendTime;
    public bool firstPlay = true;//アセンド直後にStartでfalseにする
    /* ここからアセンドでリセットする変数をpublicで宣言していく */
    /* NOTE : インスペクターに表示させたくない変数は[NonSerialized]をつける */
    /* NOTE : サイズの大きい配列は[NonSeriarized]をつける */

    public double[] max_resource;
    public double[] current_resource;
    public double[] regen_resource;

    public bool[] released_resource;
    public bool[] completed_resource;

    public bool[] released_instant;
    public bool[] completed_instant;

    public bool[] released_loop;
    public bool[] completed_loop;
    public double[] currentValue_loop;
    public bool[] paid_loop;

    public bool[] released_upgrade;
    public bool[] completed_upgrade;
    public double[] currentValue_upgrade;
    public bool[] paid_upgrade;
    public int[] clearNum_upgrade;

    /* Ability */
    public bool[] released_ability;
    public bool[] completed_ability;
    public double[] currentValue_ability;
    public bool[] paid_ability;
    public int[] levels_ability;
    public int[] maxLevels_ability;
    public bool[] unlocked_ability;
    public double[] trainRate_ability;

    /* Item */
    public int[] equipNum_Item;
    public int[] inventoryNum_Item;
    public bool[] released_Item;
    public bool[] completed_Item;
}
