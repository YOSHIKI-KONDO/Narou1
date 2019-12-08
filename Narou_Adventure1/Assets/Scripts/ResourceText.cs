using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

//releaseのcompletedをtrueにする処理は書いていない
public class ResourceText : BASE {
    public ResourceKind kind;
    public Slider slider;
    Text nameText, numText;
    ReleaseFunction release;//added

    bool Requires()
    {
        return main.rsc.Value[(int)kind] >= 1;//現在地が整数で1以上の時true
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
        nameText = GetComponentsInChildren<Text>()[0];
        numText = GetComponentsInChildren<Text>()[1];

        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_resource[(int)kind], x), x => Sync(ref main.SR.completed_resource[(int)kind], x), x => Requires());
	}

    private void FixedUpdate()
    {
        nameText.text = main.enumCtrl.resources[(int)kind].Name();
        numText.text = tDigit(main.rsc.Value[(int)kind], 1) + "/" + tDigit(main.rsc.Max((int)kind), 1);
        if (slider != null)
        {
            slider.value = (float)(main.rsc.Value[(int)kind] / main.rsc.Max((int)kind));
        }
    }
}
