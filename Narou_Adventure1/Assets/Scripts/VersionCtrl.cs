using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class VersionCtrl : BASE {

	// Use this for initialization
	void Awake () {
		StartBASE();

		/*** 製品版は忘れずに消す  ***/
		main.SR.isBeta = true;
        /***                    ***/
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
