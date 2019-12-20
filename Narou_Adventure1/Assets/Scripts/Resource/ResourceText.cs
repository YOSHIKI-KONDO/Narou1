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
    public PopUp popUp;
    string Name_str, Description_str, Regen_str, Effect_str;
    public string Others_str;
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
        numText.text = tDigit(main.rsc.Value[(int)kind], main.resourceTextCtrl.points[(int)kind].current) + "/"
            + tDigit(main.rsc.Max((int)kind), main.resourceTextCtrl.points[(int)kind].max);
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
            Regen_str = "Regeneration : " + tDigit(main.rsc.Regen((int)kind), main.resourceTextCtrl.points[(int)kind].regen) + "/s";
            Effect_str = ProgressDetail(effects);

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Regen_str, popUp.texts[2], popUp.texts[2].gameObject);
            ChangeTextAdaptive(Effect_str, popUp.texts[4], popUp.texts[3].gameObject, popUp.texts[4].gameObject);
            ChangeTextAdaptive(Others_str, popUp.texts[5], popUp.texts[5].gameObject);
        }
    }
}
