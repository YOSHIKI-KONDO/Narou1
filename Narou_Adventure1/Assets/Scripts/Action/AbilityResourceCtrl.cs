using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AbilityResourceCtrl : BASE {
    public int[] MaxLevels { get => main.SR.maxLevels_ability; set => main.SR.maxLevels_ability = value; }
    public double[] TrainRate { get => main.SR.trainRate_ability; set => main.SR.trainRate_ability = value; }
    public double[] CurrentExp { get => main.SR.currentValue_ability; set => main.SR.currentValue_ability = value; }


    public int[] CurrentLevels { get => main.SR.levels_ability; set => main.SR.levels_ability = value; }

    // Use this for initialization
    void Awake () {
		StartBASE();
	}
}
