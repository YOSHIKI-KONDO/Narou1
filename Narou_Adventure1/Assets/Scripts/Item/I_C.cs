using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class I_C : ITEM {

	// Use this for initialization
	void Awake () {
        AwakeItem(ItemKind.c, 1);
	}

	// Use this for initialization
	void Start () {
        StartItem();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateItem();
	}

    void FixedUpdate()
    {
        FixedUpdateItem();
    }
}
