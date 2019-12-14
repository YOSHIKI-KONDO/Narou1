using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class NeedLimits : BASE {
    public int? LimitNum(NeedKind kind)
    {
        if(main.SR.needLimits[(int)kind] == 0)
        {
            return null;
        }
        else
        {
            return main.SR.needLimits[(int)kind];
        }
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
