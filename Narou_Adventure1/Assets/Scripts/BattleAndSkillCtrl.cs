using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BattleAndSkillCtrl : BASE {
    /// <summary>
    /// [0,0], [1,0], [2,0], [3,0],
    /// [0,1], [1,1], [2,1], [3,1],
    /// [0,2], [1,2], [2,2], [3,2],
    /// </summary>
    public SkillKind[,] skillSlots = new SkillKind[4,3];
    public SKILL[] skills = new SKILL[Enum.GetNames(typeof(SkillKind)).Length];

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
