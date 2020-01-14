using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UsefulMethod;

public class OfflineBonus : BASE {
	public DateTime lastTime { get { return DateTime.FromBinary(Convert.ToInt64(main.S.lastTime)); } }
	public Text bonusLabel;
	public TextMeshProUGUI startText;

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
		//Debug.Log("You are left for " + DoubleTimeToDate(DeltaTimeFloat(lastTime)));
		startText.text = Calculate();
        if(startText.text != "")
        {
			bonusLabel.text = "Offline Effect";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string Calculate()
    {
		float elapsedTime = DeltaTimeFloat(lastTime);
		double[] tempCurrency = new double[main.SD.num_resource];
		Array.Copy(main.rsc.Value, tempCurrency, main.rsc.Value.Length);
		double[] plusValue = new double[main.SD.num_resource];
		for (int i = 0; i < main.SD.num_resource; i++)
		{
			if (i == 0) { continue; }
			main.rsc.Value[i] += main.rsc.Regen(i) * elapsedTime;
			main.rsc.Value[i] = Domain(main.rsc.Value[i], main.rsc.Max(i), 0);
			plusValue[i] = main.rsc.Value[i] - tempCurrency[i];
		}

		string sum = "";
		for (int i = 0; i < main.SD.num_resource; i++)
		{
			if (i == 0) { continue; }
			if (Math.Abs(plusValue[i]) < 0.00001)
			{
				continue;
			}
			sum += (sum == "") ? "You are left for " + DoubleTimeToDate(elapsedTime) + "\n" : "\n";
			sum += main.enumCtrl.resources[i].Name() + "\t" + tDigit(plusValue[i], 2);
		}
		return sum;
	}

    string AllDps(bool containZero)
    {
		string sum = "";
        for (int i = 0; i < main.SD.num_resource; i++)
        {
			if (i == 0) { continue; }
            if (Math.Abs(main.rsc.Regen(i)) < 0.00001 && !containZero)
            {
				continue;
            }
			if (sum != "") { sum += "\n"; }
			sum += main.enumCtrl.resources[i].Name() + "\t" + tDigit(main.rsc.Regen(i), 2);
        }
		return sum;
    }
}
