using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class UICtrl : BASE {
    public Text apText;
    public Text inventoryText;

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

    private void FixedUpdate()
    {
        apText.text = "AP : " + tDigit(main.rsc.Value[(int)ResourceKind.ap], 2);
        inventoryText.text = "Equipment:" + main.rsc.Value[(int)ResourceKind.equipSpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.equipSpace).ToString() +
            ",  Inventory:" + main.rsc.Value[(int)ResourceKind.inventorySpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.inventorySpace).ToString();
    }
}
