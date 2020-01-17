using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// NOTE:のちのちテストを作りたい
/// </summary>
[DefaultExecutionOrder(-10)]
public class AbilityContainer : BASE {
	public ABILITY[] abilitys;

	// Use this for initialization
	void Awake () {
		StartBASE();
		abilitys = new ABILITY[main.SD.num_ability];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
