using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class rest_sample : BASE {

    ProgressFunction rest_progress;
    double currentValue;
    double maxValue = 100;
    double plusValue = 1;

    [ContextMenu("Rest")]
    void Rest()
    {
        main.progressCtrl.Rest();
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
        //rest_progress = gameObject.AddComponent<ProgressFunction>();
        //rest_progress.ProgressDealingList = new List<ProgressFunction.Dealing>
        //{
        //    (x) => rest_progress.DealAction(x, ResourceKind.stamina, ProgressFunction.ParaKind.current, 1),
        //    (x) => rest_progress.DealAction(x, ResourceKind.mana, ProgressFunction.ParaKind.current, 1),
        //};
        //main.progressCtrl.restFunction = rest_progress;
        //rest_progress.StartProgress(gameObject, null, null, null);
    }

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
 //       if (rest_progress.isOn)
 //       {
 //           if (main.rsc.Value[(int)ResourceKind.stamina] >= main.rsc.Max((int)ResourceKind.stamina)
 //               && main.rsc.Value[(int)ResourceKind.mana] >= main.rsc.Max((int)ResourceKind.mana))
 //           {
 //               main.progressCtrl.ActivatePrevious();
 //           }
 //       }
 //   }

 //   private void FixedUpdate()
 //   {
 //       rest_progress.Progress(ref currentValue, plusValue, maxValue);
 //   }
}
