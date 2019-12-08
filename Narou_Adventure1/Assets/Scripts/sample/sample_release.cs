using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class sample_release : BASE {
    public int index;
    ReleaseFunction r_func;

    bool condition()
    {
        return main.rsc.Value[index] >= 10;
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
        //r_func = gameObject.AddComponent<ReleaseFunction>();
        //r_func.StartFunction(gameObject, x => Sync(ref main.SR.released[index], x), x => Sync(ref main.SR.completed[index], x), x => condition());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
