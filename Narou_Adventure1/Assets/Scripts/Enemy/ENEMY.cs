using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ENEMY : BASE {
    public EnemyKind kind;

	// Use this for initialization
	void Awake () {
		StartBASE();
        main.battleCtrl.ememys[(int)kind] = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
