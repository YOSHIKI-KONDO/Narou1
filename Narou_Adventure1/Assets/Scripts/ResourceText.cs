using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ResourceText : BASE {
    public ResourceKind kind;
    public Slider slider;
    Text nameText, numText;

	// Use this for initialization
	void Awake () {
		StartBASE();
        nameText = GetComponentsInChildren<Text>()[0];
        numText = GetComponentsInChildren<Text>()[1];
	}

    private void FixedUpdate()
    {
        nameText.text = main.enumCtrl.resources[(int)kind].Name();
        numText.text = tDigit(main.rsc.Value[(int)kind], 1) + "/" + tDigit(main.rsc.Max((int)kind), 1);
        if (slider != null)
        {
            slider.value = (float)(main.rsc.Value[(int)kind] / main.rsc.Max((int)kind));
        }
    }
}
