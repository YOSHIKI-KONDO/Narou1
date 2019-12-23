using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class dll_sample : BASE {

	// Use this for initialization
	void Awake () {
		StartBASE();

        var obj = new Dll_Test.First_Dll_Happy();
        obj.HelloWorld();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
