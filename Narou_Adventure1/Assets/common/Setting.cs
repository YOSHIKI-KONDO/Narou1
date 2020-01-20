using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Setting : BASE {
    //UI
    public Slider SESlider, BGMSlider;
    public Toggle buttonHighLightToggle;
    public Toggle resourceHighLightToggle;
    public Toggle announce_resourceMaxLack;

    public float SEVolume
    {
        get { return main.S.SEVolume; }
        set
        {
            main.S.SEVolume = value;
            main.sound.ChangeSEVolume(value);
        }
    }
    public float BGMVolume
    {
        get { return main.S.BGMVolume; }
        set
        {
            main.S.BGMVolume = value;
            main.sound.ChangeBGMVolume(value);
        }
    }

    

    void Save()
    {
        //SEVolume = SESlider.value;
        //BGMVolume = BGMSlider.value;
        main.SR.isOn_ButtonHighLight = buttonHighLightToggle.isOn;
        main.SR.isOn_ResourceHighLight = resourceHighLightToggle.isOn;
        main.SR.doAnnounce_resourceMaxOrLack = announce_resourceMaxLack.isOn;
    }

    void Load()
    {
        //SESlider.value = SEVolume;
        //BGMSlider.value = BGMVolume;
        buttonHighLightToggle.isOn = main.SR.isOn_ButtonHighLight;
        resourceHighLightToggle.isOn = main.SR.isOn_ResourceHighLight;
        announce_resourceMaxLack.isOn = main.SR.doAnnounce_resourceMaxOrLack;
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
    }

    private void Start()
    {
        Load();
        StartCoroutine(FixedUpdateCor());
    }

    IEnumerator FixedUpdateCor()
    {
        while (true)
        {
            Save();
            yield return new WaitForSeconds(main.tick);
        }
    }
}
