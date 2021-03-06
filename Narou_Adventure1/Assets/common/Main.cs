﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UsefulMethod;

public class Main : MonoBehaviour
{
    public double allTime { get => S.allTime; set => S.allTime = value; }
    public DateTime birthTime
    {
        get { return DateTime.FromBinary(Convert.ToInt64(S.birthDate)); }
        set { S.birthDate = value.ToBinary().ToString(); }
    }
    [NonSerialized]
    public DateTime ReleaseTime = DateTime.Parse("8/21/2019 7:00:00 AM");
    public DateTime lastTime//最後にプレイした時間。
    {
        get { return DateTime.FromBinary(Convert.ToInt64(S.lastTime)); }
        set { S.lastTime = value.ToBinary().ToString(); }
    }
    [Range(0.05f,10.0f)]
    public float tick=1.0f;
    public IdleActionCtrl idleActionCtrl;

    [SerializeField]
    public SaveR SR;
    [SerializeField]
    public Save S;
    public SaveDeclare SD;
    public Transform windowShowCanvas;
    
    

    public AudioSource SoundEffectSource;
    public Sound sound;
    public GameObject plainPopText;


    /* Libraryここまで */
    public static bool isJapanese = true;
    public ResourceCtrl rsc;//resourceCtrlの略
    public AbilityResourceCtrl a_rsc;//AbilityResourceCtrlの略
    public ProgressCtrl progressCtrl;
    public ReleaseCtrl releaseCtrl;
    public ItemCtrl itemCtrl;
    public Announce announce;
    public Focus focus;
    public DecideParameter decideParameter;

    /* プレハブ */
    public PopUp ActionPopUpPre;
    public PopUp AbilityPopUpPre;
    public PopUp itemPopUpPre;

    public EnumCtrl enumCtrl;

    private void Awake()
    {
        SoundEffectSource = gameObject.GetOrAddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //初めてのプレイだったら現在の値を代入
        if (!S.isContinuePlay)
        {
            birthTime = DateTime.Now;
            lastTime = DateTime.Now;
            S.isContinuePlay = true;
        }
        //不正な時間が入っていたら現在の値を代入
        if(lastTime < ReleaseTime || lastTime > DateTime.Now)
        {
            lastTime = DateTime.Now;
        }
        StartCoroutine(plusTime());

        if (SR.firstPlay)
        {
            SR.firstPlay = false;
            decideParameter.Initialize();
        }


    }

    IEnumerator plusTime()
    {
        while (true)
        {
            allTime++;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
