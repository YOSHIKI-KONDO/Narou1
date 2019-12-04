using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class sample_progress : BASE {

    ProgressFunction pf;
    double currentValue;
    double maxValue = 100;
    double plusValue = 1;
    public Slider slider;

    void Complete()
    {
        main.announce.Add("Max!!!!");
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
        //pf = gameObject.AddComponent<ProgressFunction>();//addをしないといけない
        //pf.InitDealingList = new List<ProgressFunction.Dealing>
        //{
        //    (x)=>pf.DealAction(x, ResourceKind.hp, ProgressFunction.ParaKind.current, -10, true),
        //};
        //pf.ProgressDealingList = new List<ProgressFunction.Dealing>
        //{
        //    (x)=>pf.DealAction(x, ResourceKind.fire,ProgressFunction.ParaKind.current, -1),
        //    (x)=>pf.DealAction(x, ResourceKind.water,ProgressFunction.ParaKind.current,-1),
        //};
        //pf.StartProgress(gameObject, ()=>Complete(), () => { return main.rsc.Value[(int)ResourceKind.gems] >= 1; }, slider);
	}

    private void FixedUpdate()
    {
        //pf.Progress(ref currentValue, plusValue,  maxValue);
    }
}
