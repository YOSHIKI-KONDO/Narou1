using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// アビリティの値を増やしたり減らしたりするクラス
/// </summary>
public class AbilityResourceCtrl : BASE {
    public int MaxLevel(int index)
    {
        return Add_MaxLevel(index) * Mul_MaxLevel(index);
    }
    public double TrainRate(int index)
    {
        return Add_TrainRate(index) * Mul_TrainRate(index);
    }

    int Add_MaxLevel(int index)
    {
        return MaxLevels_Base[index] + main.itemCtrl.A_maxLevel[index] + main.rsc.A_maxLevel[index];
    }
    int Mul_MaxLevel(int index) { return 1; }

    double Add_TrainRate(int index)
    {
        return TrainRate_Base[index] + main.itemCtrl.A_trainRate[index] + main.tempEffectsCtrl.TRates[index] + main.rsc.A_trainRate[index];
    }
    double Mul_TrainRate(int index) { return main.focus.FocusFactor(); }


    public double[] CurrentExp { get => main.SR.currentValue_ability; set => main.SR.currentValue_ability = value; }

    public int[] MaxLevels_Base { get => main.SR.maxLevels_ability; set => main.SR.maxLevels_ability = value; }
    public double[] TrainRate_Base { get => main.SR.trainRate_ability; set => main.SR.trainRate_ability = value; }
    


    public int[] CurrentLevels { get => main.SR.levels_ability; set => main.SR.levels_ability = value; }

    // Use this for initialization
    void Awake () {
		StartBASE();
	}
}
