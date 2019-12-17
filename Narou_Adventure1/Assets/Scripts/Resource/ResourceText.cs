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
    PopUp popUp;
    string Name_str, Description_str, Regen_str, Effect_str;
    public List<Dealing> effects = new List<Dealing>();

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
        popUp = main.resourcePopUp.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
    }

    private void FixedUpdate()
    {
        nameText.text = main.enumCtrl.resources[(int)kind].Name();
        numText.text = tDigit(main.rsc.Value[(int)kind], 1) + "/" + tDigit(main.rsc.Max((int)kind), 1);
        if (slider != null)
        {
            slider.value = (float)(main.rsc.Value[(int)kind] / main.rsc.Max((int)kind));
        }

        ApplyPopUp();
    }

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.resources[(int)kind].Name();
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            Description_str = main.enumCtrl.resources[(int)kind].Description();
            Regen_str = "Regeneration : " + tDigit(main.rsc.Regen((int)kind),2) + "/s";
            Effect_str = ProgressDetail(effects);

            if (Name_str == "" || Name_str == null)
            {
                setFalse(popUp.texts[0].gameObject);
            }
            else
            {
                popUp.texts[0].text = Name_str;
            }

            if (Description_str == "" || Description_str == null)
            {
                setFalse(popUp.texts[1].gameObject);
            }
            else
            {
                popUp.texts[1].text = Description_str;
            }

            if (Regen_str == "" || Regen_str == null)
            {
                setFalse(popUp.texts[2].gameObject);
            }
            else
            {
                popUp.texts[2].text = Regen_str;
            }

            if (Effect_str == "" || Effect_str == null)
            {
                setFalse(popUp.texts[3].gameObject);
                setFalse(popUp.texts[4].gameObject);
            }
            else
            {
                popUp.texts[4].text = Effect_str;
            }

        }
    }
}
