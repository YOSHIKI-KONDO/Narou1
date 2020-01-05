using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CheckDifficulty : BASE {

    [Range(1f, 10f)]
    public float TimeScale = 1f;
    public DungeonKind dunKind;
    public int minLevel = 1;
    public int maxlevel = 5;
    int currentLevel;
    int totalNum;
    int winNum;
    int loseNum;
    public int TotalNum
    {
        get { return totalNum; }
        set { if (gameObject.activeSelf) { totalNum = value; } }
    }
    public int WinNum
    {
        get { return winNum; }
        set { if (gameObject.activeSelf) { winNum = value; } }
    }
    public int LoseNum
    {
        get { return loseNum; }
        set { if (gameObject.activeSelf) { loseNum = value; } }
    }
    int duration = 5;

    // Use this for initialization
    void Awake () {
		StartBASE();
        Debug.Log("NOTE : CheckDifficultyがonになっています。");
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        ControllLevel();
        CheckNextLevel();
        CheckEnding();
	}

    private void FixedUpdate()
    {
        Time.timeScale = TimeScale;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,800,50), winNum + "勝" + LoseNum + "負. ");
    }

    void CheckEnding()
    {
        if(currentLevel > maxlevel)
        {
            main.progressCtrl.DontDoAnything();
            Debug.Log("計測終了");
        }
    }

    void CheckNextLevel()
    {
        if (WinNum + loseNum >= duration)
        {
            Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() +
                "の計測終了(Lv" + currentLevel + ")" + winNum + "勝" + LoseNum + "負. " + "勝率は" + (100f * (float)winNum / (float)duration).ToString("F1") + "%でした。");

            currentLevel++;
            totalNum = 0;
            WinNum = 0;
            loseNum = 0;
        }
    }

    void ControllLevel()
    {
        main.rsc.Value[(int)ResourceKind.exp] = 0;
        main.SR.level = currentLevel;
    }

    [ContextMenu("StartCheck")]
    void SelectDungeon()
    {
        if(dunKind == DungeonKind.nothing) { return; }
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の計測開始");
        currentLevel = minLevel;
        totalNum = 0;
        WinNum = 0;
        loseNum = 0;
        main.progressCtrl.ActivateProgress(main.battleCtrl.dungeons[(int)dunKind].progress);
    }
}
