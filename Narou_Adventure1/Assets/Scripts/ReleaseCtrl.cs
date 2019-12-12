using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ReleaseFunctionのListを持っており、これは自動で追加される。
/// このクラスで一括でFixedUpdateを回すことで、各クラスに書く必要が無くなる。
/// </summary>
public class ReleaseCtrl : BASE {
    public List<ReleaseFunction> list = new List<ReleaseFunction>();

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

    private void FixedUpdate()
    {
        foreach (var function in list)
        {
            function.UpdateFunction();
        }
    }
}
