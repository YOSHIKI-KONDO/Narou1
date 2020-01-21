using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ClearTimeSubmit : BASE {

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
        Submit();
	}

    //あとで統合する
    public void Submit()
    {
        int clearTime = (int)(main.S.allTime / 60f);
        Application.ExternalCall("kongregate.stats.submit", "Clear Time", clearTime); //ハイスコアを送信
    }
}
