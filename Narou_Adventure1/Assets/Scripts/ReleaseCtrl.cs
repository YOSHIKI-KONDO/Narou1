using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ReleaseCtrl : BASE {
    public List<ReleaseFunction> list = new List<ReleaseFunction>();

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

    private void FixedUpdate()
    {
        foreach (var function in list)
        {
            function.UpdateFunction();
        }
    }
}
