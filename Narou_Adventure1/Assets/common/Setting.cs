using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UsefulMethod;

public class Setting : BASE {
    //UI
    public Slider SESlider, BGMSlider;
    public Toggle buttonHighLightToggle;
    public Toggle resourceHighLightToggle;
    public Toggle announce_resourceMaxLack;
    public Toggle darkModeToggle;
    public Button restartButton;

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
        main.SR.colorMode = darkModeToggle.isOn == true ? 1 : 0;
    }

    void Load()
    {
        //SESlider.value = SEVolume;
        //BGMSlider.value = BGMVolume;
        buttonHighLightToggle.isOn = main.SR.isOn_ButtonHighLight;
        resourceHighLightToggle.isOn = main.SR.isOn_ResourceHighLight;
        announce_resourceMaxLack.isOn = main.SR.doAnnounce_resourceMaxOrLack;
        darkModeToggle.isOn = main.SR.colorMode == 1 ? true : false;
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
    }

    private void Start()
    {
        Load();
        StartCoroutine(FixedUpdateCor());
        restartButton.onClick.AddListener(() =>LoadNewScene("main"));

        //UIを変更するたびにsaveされるようにする
        //NOTE:FixedUpdateCorがなくてもいいかも
        buttonHighLightToggle.onValueChanged.AddListener((x) => Save());
        resourceHighLightToggle.onValueChanged.AddListener((x) => Save());
        announce_resourceMaxLack.onValueChanged.AddListener((x) => Save());
        darkModeToggle.onValueChanged.AddListener((x) => Save());
        //SESlider.onValueChanged.AddListener((x) => Save());
        //BGMSlider.onValueChanged.AddListener((x) => Save());
    }


    IEnumerator FixedUpdateCor()
    {
        while (true)
        {
            Save();
            yield return new WaitForSeconds(1.0f);
        }
    }



    //new scene
    public void LoadNewScene(string sceneName)
    {
        Save();
        StartCoroutine(LoadNewSceneCor(sceneName));
    }

    IEnumerator LoadNewSceneCor(string sceneName)
    {
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
