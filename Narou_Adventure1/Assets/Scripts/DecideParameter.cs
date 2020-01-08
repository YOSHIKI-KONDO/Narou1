using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class DecideParameter : BASE {
    /// <summary>
    /// アセンド後に一度だけ呼ぶ関数
    ///
    /// リソースの現在値 ... main.rsc.Value[(int)ResouceKind.(リソースのEnum)];
    /// リソースの最大値 ... main.rsc.Max_Base[(int)ResouceKind.(リソースのEnum)];
    /// リソースの回復値 ... main.rsc.Regen_Base[(int)ResouceKind.(リソースのEnum)];
    /// 
    /// アビリティの最大レベル ... main.a_rsc.MaxLevels_Base[(int)AbilityKind.(アビリティのEnum)];
    /// アビリティの増加値 ... main.a_rsc.TrainRate_Base[(int)AbilityKind.(アビリティのEnum)];
    /// アビリティの経験値 ... main.a_rsc.CurrentExp[(int)AbilityKind.(アビリティのEnum)];
    /// 
    /// </summary>
    public void Initialize()
    {
        /* アビリティのレベルの最大値 */
        for (int i = 0; i < main.SD.num_ability; i++)
        {
            main.a_rsc.MaxLevels_Base[i] = 5;//最大レベル5でスタート
        }
        main.SR.released_element[(int)ElementKind.main] = true;     //要素、タブの解放
        main.SR.released_element[(int)ElementKind.status] = true;   //要素、タブの解放

        main.rsc.Max_Base[(int)ResourceKind.inventorySpace] = 0;
        main.rsc.Max_Base[(int)ResourceKind.equipSpace] = 0;
        //ステータスリソース
        main.rsc.Max_Base[(int)ResourceKind.stamina] = 1;
        main.rsc.Value[(int)ResourceKind.stamina]=1;
        main.rsc.Max_Base[(int)ResourceKind.hp] = 1;
        main.rsc.Value[(int)ResourceKind.hp] = 1;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint1] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint2] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint3] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint4] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint5] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint6] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint7] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint8] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint9] = 20;
        main.rsc.Max_Base[(int)ResourceKind.itemPoint10] = 20;
        //MP回復
        main.rsc.Regen_Base[(int)ResourceKind.mp] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.fire] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.water] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.wind] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.earth] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.thunder] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.ice] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.light] = 0.1;
        main.rsc.Regen_Base[(int)ResourceKind.dark] = 0.1;
        //リソース
        main.rsc.Max_Base[(int)ResourceKind.gold] = 20;
        main.rsc.Max_Base[(int)ResourceKind.research] = 10;
        main.rsc.Max_Base[(int)ResourceKind.paper] = 5;
        main.rsc.Max_Base[(int)ResourceKind.book] = 1;
        main.rsc.Max_Base[(int)ResourceKind.herb] = 5;
        main.rsc.Max_Base[(int)ResourceKind.flower] = 1;
        main.rsc.Max_Base[(int)ResourceKind.bread] = 10;
        main.rsc.Max_Base[(int)ResourceKind.anchovy_sandwich] = 3;
        main.rsc.Max_Base[(int)ResourceKind.filet_o_fish] = 3;
        main.rsc.Max_Base[(int)ResourceKind.stone] = 10;
        main.rsc.Max_Base[(int)ResourceKind.magi_stone] = 5;
        main.rsc.Max_Base[(int)ResourceKind.ap] = 9999;
        main.rsc.Max_Base[(int)ResourceKind.fur] = 3;
        main.rsc.Max_Base[(int)ResourceKind.premium_fur] = 3;
        //装備アイテム：種類毎の上限(0ならば上限なし、無限)
        main.SR.needLimits[(int)NeedKind.weapon] = 1;
        main.SR.needLimits[(int)NeedKind.armor] = 1;
        main.SR.needLimits[(int)NeedKind.shield] = 1;
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
    }
}
