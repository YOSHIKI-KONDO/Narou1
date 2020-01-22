using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class PopUp_Default : BASE {
	PopUp popUp;
	[TextAreaAttribute(10, 100)]//height:10,width:100
	public string text;

	// Use this for initialization
	void Awake () {
		StartBASE();
		popUp = main.DefaultPopUp.StartPopUp(gameObject, main.windowShowCanvas);
		popUp.textPros[0].text = text;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
