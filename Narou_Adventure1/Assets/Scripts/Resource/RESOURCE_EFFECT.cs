using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class RESOURCE_EFFECT : BASE {
    public ResourceKind kind;
    public List<Dealing> effects = new List<Dealing>();

	// Use this for initialization
	public void AwakeResource (ResourceKind kind) {
		StartBASE();
        this.kind = kind;
        main.resourceTextCtrl.InitializeArray();
        main.resourceTextCtrl.effectAry[(int)kind] = this;
	}
}
