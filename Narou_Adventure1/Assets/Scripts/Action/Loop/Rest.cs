using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Rest : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Max((int)ResourceKind.stamina) >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.rest, 10,0);
        progress.progressEffectList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, 1));
        progress.progressEffectList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.current, 1));
        progress.progressEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.earth, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.thunder, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.ice, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.light, Dealing.R_ParaKind.current, 0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.dark, Dealing.R_ParaKind.current, 0.5));



        main.progressCtrl.restFunction = progress;
    }

	// Use this for initialization
	void Start () {
        StartLoopAction();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLoopAction();

        if (progress.isOn)
        {
            if (main.rsc.Value[(int)ResourceKind.stamina] >= main.rsc.Max((int)ResourceKind.stamina)
                && main.rsc.Value[(int)ResourceKind.hp] >= main.rsc.Max((int)ResourceKind.hp)
                && main.rsc.Value[(int)ResourceKind.mp] >= main.rsc.Max((int)ResourceKind.mp))
            {
                main.progressCtrl.ActivatePrevious();
            }
        }
    }

    private void FixedUpdate()
    {
        FixedUpdateLoopAction();
    }
}
