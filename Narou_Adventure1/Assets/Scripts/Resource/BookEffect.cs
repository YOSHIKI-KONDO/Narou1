using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BookEffect : RESOURCE_EFFECT {

	// Use this for initialization
	void Awake () {
		AwakeResource(ResourceKind.book);
        effects.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 5));
        effects.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.regen, 0.01));
	}
}
