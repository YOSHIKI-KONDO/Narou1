using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CheckDifficulty : BASE {

    [Range(1f, 100f)]
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
    int sum_FloorNum;
    int maxFloorNum;
    public int Sum_FloorNum
    {
        get { return sum_FloorNum; }
        set { if (gameObject.activeSelf) { sum_FloorNum = value; } }
    }
    public int MaxFloorNum
    {
        get { return maxFloorNum; }
        set { if (gameObject.activeSelf) { maxFloorNum = value; } }
    }
    public int duration = 5;

    CalculateKind calKind;
    enum CalculateKind
    {
        nothing,
        clear,
        floor,
    }

    class DataSet
    {
        public float ClearRate { get; set; }
        public float FloorRate { get; set; }

        public DataSet(float clearRate, float floorRate)
        {
            this.ClearRate = clearRate;
            this.FloorRate = floorRate;
        }
    }
    string NewLineClear(List<DataSet> list)
    {
        string sum = "";
        foreach (var set in list)
        {
            if (sum != "") { sum += "\n"; }
            sum += set.ClearRate.ToString("F3");
        }
        return sum;
    }
    string NewLineFloor(List<DataSet> list)
    {
        string sum = "";
        foreach (var set in list)
        {
            if (sum != "") { sum += "\n"; }
            sum += set.FloorRate.ToString("F3");
        }
        return sum;
    }
    List<DataSet> dataList = new List<DataSet>();
    bool allFlg;


    // Use this for initialization
    void Awake () {
		StartBASE();
    }

	// Use this for initialization
	void Start () {
        Debug.Log("NOTE : CheckDifficultyがonになっています。");
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
            End();
        }
    }

    void End()
    {
        main.progressCtrl.DontDoAnything();
        Debug.Log("計測終了");
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の平均クリア率\n" + NewLineClear(dataList));
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の平均攻略率\n" + NewLineFloor(dataList));

        if (allFlg)
        {
            if((int)dunKind < main.SD.num_dungeon - 1)
            {
                dunKind = (DungeonKind)((int)dunKind + 1);
                CalculateFloor();
                return;
            }
            Debug.Log("全体調査終了.");
        }
    }

    void CheckNextLevel()
    {
        if (WinNum + loseNum >= duration)
        {
            /*
            Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() +
                "の計測終了(Lv" + currentLevel + ")" + winNum + "勝" + LoseNum + "負. " + "勝率は" + (100f * (float)winNum / (float)duration).ToString("F1") + "%でした。 " +
                "平均クリア率は" + (100f * sum_FloorNum / (duration * maxFloorNum)).ToString("F0") + "%."
                );*/
            dataList.Add(new DataSet(((float)winNum / (float)duration), ((float)sum_FloorNum / (float)(duration * maxFloorNum))));

            if (winNum == duration) { End(); }//計測終了

            currentLevel++;
            main.rsc.Max_Base[(int)ResourceKind.hp] += 2.2; //hpをレベルアップするごとに増加
            totalNum = 0;
            WinNum = 0;
            loseNum = 0;
            sum_FloorNum = 0;
            maxFloorNum = 0;
        }
    }

    void ControllLevel()
    {
        main.rsc.Value[(int)ResourceKind.exp] = 0;
        main.SR.level = currentLevel;
    }

    [ContextMenu("Check_thisDungeon")]
    void CalculateThisDungeon()
    {
        allFlg = false;
        CalculateFloor();
    }

    void CalculateFloor()
    {
        if (dunKind == DungeonKind.nothing) { return; }
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の計測開始. floor.");
        calKind = CalculateKind.floor;
        currentLevel = minLevel;
        main.rsc.Max_Base[(int)ResourceKind.hp] = currentLevel + 8;
        dataList = new List<DataSet>();//listの初期化
        totalNum = 0;
        WinNum = 0;
        loseNum = 0;
        sum_FloorNum = 0;
        maxFloorNum = 0;
        main.progressCtrl.ActivateProgress(main.battleCtrl.dungeons[(int)dunKind].progress);
        main.progressCtrl.previousFunction = main.battleCtrl.dungeons[(int)dunKind].progress;
    }

    [ContextMenu("Check_AllDungeon")]
    void CalculateWholeDungeon()
    {
        allFlg = true;
        dunKind = (DungeonKind)1;
        CalculateFloor();
    }
}
