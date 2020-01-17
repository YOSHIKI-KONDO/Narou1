using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static MainAction.ActionEnum;

public class HighLightCtrl : BASE {
	ResourceText[] resources { get => main.resourceTextCtrl.resourceTexts; }

	public ResourceHighLight[] resourceHighLights;

	// Use this for initialization
	void Awake () {
		StartBASE();
		resourceHighLights = new ResourceHighLight[Enum.GetNames(typeof(ResourceKind)).Length];
        for (int i = 0; i < resourceHighLights.Length; i++)
        {
            resourceHighLights[i] = new ResourceHighLight();
        }
	}
    
	// Use this for initialization
	void Start () {

	}

    public class ResourceHighLight
    {
        public List<HighLightFunction> functions = new List<HighLightFunction>();
	}

    public void ApplyResource(ResourceKind resourceKind, bool onHighLight)
    {
        if(main.SR.isOn_ResourceHighLight == false) { return; }
        if(resources[(int)resourceKind] == null) { return; }
		if (resources[(int)resourceKind].highLight == null) { return; }
		if (onHighLight)
		{
			setActive(resources[(int)resourceKind].highLight);
        }
        else
        {
            setFalse(resources[(int)resourceKind].highLight);
		}
    }

    public void ApplyContents(ResourceKind resourceKind, bool onHighLight)
    {
        if (main.SR.isOn_ButtonHighLight == false) { return; }
        if (resourceKind == ResourceKind.nothing) { return; }
        foreach (var hl in resourceHighLights[(int)resourceKind].functions)
        {
            if (onHighLight)
            {
				setActive(hl.highLightObj);
            }
            else
            {
				setFalse(hl.highLightObj);
            }
        }
    }
}
