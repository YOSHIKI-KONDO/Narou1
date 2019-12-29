using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Focus : BASE {
    public Button button;
    public GameObject obj;
    double Remain { get => main.rsc.Value[(int)ResourceKind.focus]; set => main.rsc.Value[(int)ResourceKind.focus] = value; }
    double Max { get => main.rsc.Max((int)ResourceKind.focus); }
    ResourceText resourceText;

    public double FocusFactor()
    {
        if (Remain <= 1) { return 1; }
        return 0.01d * (25 * Remain + 75);
    }

    void DoFocus()
    {
        double sub = Max - Remain;
        if(sub > main.rsc.Value[(int)ResourceKind.mp])
        {
            Remain += main.rsc.Value[(int)ResourceKind.mp] - 0.1d; //若干残さないとrestに入ってしまう
            main.rsc.Value[(int)ResourceKind.mp] = 0.1d;
        }
        else
        {
            Remain = Max;
            main.rsc.Value[(int)ResourceKind.mp] -= sub;
        }
    }

    public void Activate()
    {
        setActive(obj);
    }

    public void Deactivate()
    {
        setFalse(obj);
    }

    //Called in Fixed Update
    void Decline()
    {
        if(Remain > 1)
        {
            Remain -= 0.01 + Remain * 0.0025;
        }
        else
        {
            Remain = 1;
        }
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
        button.onClick.AddListener(DoFocus);

        main.rsc.Regen_Base[(int)ResourceKind.focus] = 0;
	}

    private void Start()
    {
        //if (GetComponent<ReleaseFunction>())
        //{
        //    GetComponent<ReleaseFunction>().RemoveRelease();
        //    Destroy(GetComponent<ReleaseFunction>());
        //}
        resourceText = GetComponent<ResourceText>();
    }

    private void FixedUpdate()
    {
        Decline();
        resourceText.Others_str = "Current Bonus : " + (FocusFactor() * 100).ToString("F0") + "%";
        main.rsc.Max_Base[(int)ResourceKind.focus] = main.rsc.Max_Base[(int)ResourceKind.mp];
    }
}
