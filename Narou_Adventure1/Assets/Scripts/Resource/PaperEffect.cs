using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class PaperEffect : RESOURCE_EFFECT {

	// Use this for initialization
	void Awake () {
		AwakeResource(ResourceKind.paper);
        effects.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 2));
        effects.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.regen, 0.002));
	}
}
