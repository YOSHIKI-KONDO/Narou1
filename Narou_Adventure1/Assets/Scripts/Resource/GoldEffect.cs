using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class GoldEffect : RESOURCE_EFFECT {

	// Use this for initialization
	void Awake () {
		AwakeResource(ResourceKind.gold);
        effects.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.regen, 0.01));
        effects.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 1));
	}
}
