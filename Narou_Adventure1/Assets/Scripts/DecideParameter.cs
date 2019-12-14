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
        main.rsc.Regen_Base[(int)ResourceKind.focus] = -0.04;
        main.rsc.Max_Base[(int)ResourceKind.focus] = 2;
        main.SR.released_resource[(int)ResourceKind.focus] = true;

        main.rsc.Max_Base[(int)ResourceKind.inventorySpace] = 0;
        main.rsc.Max_Base[(int)ResourceKind.equipSpace] = 0;
        main.rsc.Max_Base[(int)ResourceKind.stamina] = 1;
        main.rsc.Value[(int)ResourceKind.stamina]=1;
        main.rsc.Max_Base[(int)ResourceKind.hp] = 1;
        main.rsc.Value[(int)ResourceKind.hp] = 1;
        main.rsc.Max_Base[(int)ResourceKind.gold] = 20;
        main.rsc.Max_Base[(int)ResourceKind.research] = 30;
        main.rsc.Max_Base[(int)ResourceKind.paper] = 10;
        main.rsc.Max_Base[(int)ResourceKind.paper] = 5;
        main.rsc.Max_Base[(int)ResourceKind.herb] = 5;
        main.rsc.Max_Base[(int)ResourceKind.bread] = 10;
        main.rsc.Max_Base[(int)ResourceKind.anchovy_sandwich] = 3;
        main.rsc.Max_Base[(int)ResourceKind.filet_o_fish] = 5;
        main.rsc.Max_Base[(int)ResourceKind.ap] = 999;
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
    }
}
