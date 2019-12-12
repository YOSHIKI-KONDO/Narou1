using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ResourceCtrl : BASE {

    //読み取り用
    public double Max(int index)
    {
        return Max_Base[index] + main.itemCtrl.R_max[index];
    }
    public double Regen(int index)
    {
        return Regen_Base[index] + main.itemCtrl.R_regen[index] + main.tempEffectsCtrl.Regens[index];
    }
    //読み取り・書き換え可能
    public double[] Value { get => main.SR.current_resource; set => main.SR.current_resource = value; }

    public double[] Max_Base { get => main.SR.max_resource; set => main.SR.max_resource = value; }
    public double[] Regen_Base { get => main.SR.regen_resource; set => main.SR.regen_resource = value; }


    

    public int ResourceNum { get => main.SD.num_resource;}

    // Use this for initialization
    void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
        index_exp = (int)ResourceKind.exp;
	}

    // Update is called once per frame
    private void LateUpdate()
    {
        ApplyLevel();
        Truncate();
    }

    //rateは0.1/s
    private void FixedUpdate()
    {
        //regeneration
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] += Regen(i) * Time.fixedDeltaTime;
            Truncate();
        }
    }

    void Truncate()
    {
        //max以下切り捨て
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] = Domain(Value[i], Max(i), 0d);
        }
    }

    int index_exp;
    //levelと経験値の設定
    void ApplyLevel()
    {
        Max_Base[index_exp] = 10 * Math.Pow(1.2, main.SR.level);  //最大値を設定
        while (Value[index_exp] >= Max(index_exp))
        {
            Max_Base[index_exp] = 10 * Math.Pow(1.2, main.SR.level);  //最大値を設定
            Value[index_exp] -= Max(index_exp);
            main.SR.level++;
            main.announce.Add("Level UP! (" + (main.SR.level - 1).ToString() + "→" + main.SR.level.ToString() + ")");
        }
    }
}
