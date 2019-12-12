using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static ResourceKind;

public class ResourceTextControll : BASE {
    public ResourceText normalPre;
    public ResourceText energyPre;
    public Transform parent;

    void Initialize()
    {
        for (int i = 0; i < Enum.GetNames(typeof(ResourceKind)).Length; i++)
        {
            // 表示しないリソース
            ResourceKind k = (ResourceKind)i;
            if (k == focus || k==equipSpace || k == inventorySpace || k == strength || k==mentalStrength||k==defense||k==dodge||k==criticalChance||
                k == exp || k == attack || k == magic_attack || k == ap)
            {
                continue;
            }

            // エナジー
            if (k == stamina || k == hp || k == mp || k == fire || k == water || k == wind || k == earth || k == thunder || k == ice ||
                k == ResourceKind.light || k == dark)
            {
                var energy = Instantiate(energyPre, parent);
                energy.kind = k;
                continue;
            }

            // 普通のリソース
            var resource = Instantiate(normalPre, parent);
            resource.kind = k;
        }
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
