using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Focus : BASE {
    public Button button;
    public GameObject obj;
    void DoFocus()
    {
        main.rsc.Value[(int)ResourceKind.focus] = main.rsc.Max((int)ResourceKind.focus);
    }

    public void Activate()
    {
        setActive(obj);
    }

    public void Deactivate()
    {
        setFalse(obj);
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
        button.onClick.AddListener(DoFocus);
	}
}
