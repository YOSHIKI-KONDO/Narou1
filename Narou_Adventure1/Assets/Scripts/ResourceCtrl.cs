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
        return Max_Base[index];
    }
    public double Regen(int index)
    {
        return Regen_Base[index];
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
		
	}
	
	// Update is called once per frame
    private void LateUpdate()
    {
        //max以下切り捨て
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] = Domain(Value[i], Max(i), 0d);
        }
    }

    //rateは0.1/s
    private void FixedUpdate()
    {
        //regeneration
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] += Regen(i) * Time.fixedDeltaTime;
        }
    }
}
