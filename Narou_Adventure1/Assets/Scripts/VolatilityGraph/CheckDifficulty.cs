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
    public EnemyKind eneKind;
    public int minLevel = 1;
    public int maxlevel = 5;
    int currentLevel;
    int totalNum;
    int winNum;
    int loseNum;
    public int TotalNum
    {
        get { return totalNum; }
        set { if (this.enabled) { totalNum = value; } }
    }
    public int WinNum
    {
        get { return winNum; }
        set { if (this.enabled) { winNum = value; } }
    }
    public int LoseNum
    {
        get { return loseNum; }
        set { if (this.enabled) { loseNum = value; } }
    }
    int sum_FloorNum;
    int maxFloorNum;
    public int Sum_FloorNum
    {
        get { return sum_FloorNum; }
        set { if (this.enabled) { sum_FloorNum = value; } }
    }
    public int MaxFloorNum
    {
        get { return maxFloorNum; }
        set { if (this.enabled) { maxFloorNum = value; } }
    }
    public int duration = 5;

    CalculateKind calKind;
    enum CalculateKind
    {
        nothing,
        oneDungeon,
        allDungeon,
        oneEnemy,
        allEnemy
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
    string NewLineClear(IEnumerable<DataSet> list)
    {
        string sum = "";
        foreach (var set in list)
        {
            if (sum != "") { sum += "\n"; }
            if(set == null)
            {
                sum += "0.000";
            }
            else
            {
                sum += set.ClearRate.ToString("F3");
            }
        }
        return sum;
    }
    string NewLineFloor(IEnumerable<DataSet> list)
    {
        string sum = "";
        foreach (var set in list)
        {
            if (sum != "") { sum += "\n"; }
            if (set == null)
            {
                sum += "0.000";
            }
            else
            {
                sum += set.FloorRate.ToString("F3");
            }
        }
        return sum;
    }

    string NewLineClear_wholeD(DataSet[,] list)
    {
        string sum = "";
        for (int i_l = 0; i_l < list.GetLength(1); i_l++)
        {
            for (int i_d = 0; i_d < list.GetLength(0); i_d++)
            {
                if(list[i_d, i_l] == null)
                {
                    sum += "0.000" + "\t";
                }
                else
                {
                    sum += list[i_d, i_l].ClearRate.ToString("F3") + "\t";
                }
            }
            sum += "\n";
        }
        return sum;
    }
    string NewLineFloor_wholeD(DataSet[,] list)
    {
        string sum = "";
        for (int i_l = 0; i_l < list.GetLength(1); i_l++)
        {
            for (int i_d = 0; i_d < list.GetLength(0); i_d++)
            {
                if (list[i_d, i_l] == null)
                {
                    sum += "0.000" + "\t";
                }
                else
                {
                    sum += list[i_d, i_l].FloorRate.ToString("F3") + "\t";
                }
            }
            sum += "\n";
        }
        return sum;
    }

    DataSet[] dataList = new DataSet[100]; //lv100まで初期化
    DataSet[,] dataList_wholeD;



    struct EnemyLevelRange
    {
        public int minLevel;
        public int maxLevel;
        public EnemyKind kind;
        public EnemyLevelRange(EnemyKind kind, int minLevel, int maxLevel)
        {
            this.kind = kind;
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
        }
    }
    List<EnemyLevelRange> enemyLevels = new List<EnemyLevelRange>();
    void InitEnemyLevels(List<EnemyLevelRange> list)
    {
        //list = new List<EnemyLevelRange>();
        list.Add(new EnemyLevelRange(EnemyKind.nothing, 2, 3));
        list.Add(new EnemyLevelRange(EnemyKind.slime, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.goblin, 3, 10));
        list.Add(new EnemyLevelRange(EnemyKind.rat, 1, 3));
        list.Add(new EnemyLevelRange(EnemyKind.bird, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.bat, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.wolf, 1, 5));
        list.Add(new EnemyLevelRange(EnemyKind.snake, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.demonic, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.sigurd, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.askr, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.embla, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.red_slime, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.orc, 6, 8));
        list.Add(new EnemyLevelRange(EnemyKind.poison_rat, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.harpy, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.ghoul, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.werewolf, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.lizard_man, 1, 2));
        list.Add(new EnemyLevelRange(EnemyKind.demonic_warrior, 1, 2));
    }

    //入力したダンジョンに入力した敵が含まれているかどうか
    bool ContainEnemy(DungeonKind dKind, EnemyKind eKind)
    {
        foreach (var ary in main.battleCtrl.dungeons[(int)dKind].enemyList)
        {
            if(Array.IndexOf(ary, eKind) >= 0)
            {
                return true;
            }
        }
        return false;
    }


    // Use this for initialization
    void Awake () {
		StartBASE();
        InitEnemyLevels(enemyLevels);
    }

	// Use this for initialization
	void Start () {
        Debug.Log("NOTE : CheckDifficultyがonになっています。");
        main.SR.level_loop[(int)MainAction.ActionEnum.Loop.rest] = 10000;
        //CalculateEnemyLevel(EnemyKind.slime);
    }
	
	// Update is called once per frame
	void Update () {
        ControlLevel();
	}

    private void FixedUpdate()
    {
        Time.timeScale = TimeScale;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,800,50), winNum + "勝" + LoseNum + "負. ");
    }

    public void EnterAction(int maxFloor)
    {
        TotalNum++;
        MaxFloorNum = maxFloor;
    }

    public void WinAction(int maxFloor)
    {
        WinNum++;
        Sum_FloorNum += maxFloor;

        CheckSituation();
    }

    public void LoseAction(int currentFloor)
    {
        LoseNum++;
        Sum_FloorNum += currentFloor;

        CheckSituation();
    }

    void CheckSituation()
    {
        switch (calKind)
        {
            case CalculateKind.oneDungeon:
                CheckNextLevel_dungeon();
                CheckEnding_dungeon();
                break;
            case CalculateKind.allDungeon:
                CheckNextLevel_dungeon();
                CheckEnding_dungeon();
                break;
            case CalculateKind.oneEnemy:
                CheckNextLevel_enemy();
                CheckEnd_enemy();
                break;
            case CalculateKind.allEnemy:
                CheckNextLevel_enemy();
                CheckEnd_enemy();
                break;
            default:
                break;
        }
    }

    void CheckEnding_dungeon()
    {
        if(currentLevel > maxlevel)
        {
            End_dungeon();
        }
    }


    void End_dungeon()
    {
        main.progressCtrl.DontDoAnything();
        //Debug.Log("計測終了");
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の平均クリア率\n" + NewLineClear(dataList));
        Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の平均攻略率\n" + NewLineFloor(dataList));
        //全体のリストに追加
        for (int i = 0; i < dataList.Length; i++)
        {
            dataList_wholeD[(int)dunKind, i] = dataList[i]; 
        }

        if (calKind == CalculateKind.allDungeon)
        {
            if ((int)dunKind < main.SD.num_dungeon - 1)
            {
                dunKind = (DungeonKind)((int)dunKind + 1);
                CalculateFloor();
                return;
            }
            Debug.Log("全体調査完了.");
            Debug.Log("全体クリア率\n" + NewLineClear_wholeD(dataList_wholeD));
            Debug.Log("全体攻略率\n" + NewLineFloor_wholeD(dataList_wholeD));
        }
    }

    void CheckNextLevel_dungeon()
    {
        if (WinNum + loseNum >= duration)
        {
            /*
            Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() +
                "の計測終了(Lv" + currentLevel + ")" + winNum + "勝" + LoseNum + "負. " + "勝率は" + (100f * (float)winNum / (float)duration).ToString("F1") + "%でした。 " +
                "平均クリア率は" + (100f * sum_FloorNum / (duration * maxFloorNum)).ToString("F0") + "%."
                );*/
            dataList[currentLevel] = new DataSet(((float)winNum / (float)duration), ((float)sum_FloorNum / (float)(duration * maxFloorNum)));

            //続行判定
            if (winNum == duration)
            {
                End_dungeon();
            }
            else
            {
                currentLevel++;
                main.status.LevelUp_Debug();//スタータスの更新
                totalNum = 0;
                WinNum = 0;
                loseNum = 0;
                sum_FloorNum = 0;
                maxFloorNum = 0;
            }
        }
    }

    void CheckEnd_enemy()
    {
        if(currentEnemyLevel > maxLevel_Enemy)
        {
            End_enemy();
        }
    }


    void End_enemy()
    {
        //今だけ
        Debug.Log(main.enumCtrl.enemys[(int)eneKind].Name() + "Lv:" + currentEnemyLevel.ToString() + ", " + 
            main.enumCtrl.dungeons[(int)dunKind].Name() + "の平均攻略率\n" + NewLineFloor(dataList));
        main.progressCtrl.DontDoAnything();
        //まだダンジョンがあるかどうかの判定
        if((int)dunKind < main.SD.num_dungeon - 1)
        {
            //損失関数の計算
            ZeroToOneAfterOne(ref result_oneLevel);
            loss[currentEnemyLevel] = Mean_squared_error(result_oneLevel, completeRate_teacher);

            dunKind = (DungeonKind)((int)dunKind + 1);
            //dungeonに指定のenemyが含まれていなければskip
            while(ContainEnemy(dunKind, eneKind) == false)
            {
                if((int)dunKind == main.SD.num_dungeon - 1)
                {
                    break ;//
                }
                dunKind = (DungeonKind)((int)dunKind + 1);
            }
            if(ContainEnemy(dunKind, eneKind))
            {
                CalculateFloor();
                return;
            }
        }

        if (currentEnemyLevel < maxLevel_Enemy)
        {
            currentEnemyLevel++;
            while (currentEnemyLevel < enemyLevels[(int)eneKind].minLevel)
            {
                currentEnemyLevel++;
            }
            //敵のステータスの更新
            main.enemyParameter.parameters[(int)eneKind].level = currentEnemyLevel;
            main.battleCtrl.enemys[(int)eneKind].UpdateParameter();    
            CalculateEnemyOneLevelAllDungeon();
        }
        else
        {
            (double MinValue, int bestLevel) = MinLoss(loss);
            Debug.Log(main.enumCtrl.enemys[(int)eneKind].Name() + "の損失関数が最小となったのはLv." + bestLevel.ToString() + "で" + MinValue.ToString("F1") + "でした");
            Debug.Log("損失関数一覧:" + string.Join(",", loss));
            main.progressCtrl.DontDoAnything();
            dunKind = DungeonKind.nothing;
            eneKind = EnemyKind.nothing;
        }
    }

    (double value, int index)MinLoss(double[] _loss)
    {
        (double value, int index) min = (10000000000, 0);
        for (int i = 0; i < _loss.Length; i++)
        {
            if(i < enemyLevels[(int)eneKind].minLevel || i > enemyLevels[(int)eneKind].maxLevel)
            {
                continue;
            }
            if (_loss[i] < min.value)
            {
                min = (_loss[i], i);
            }
        }
        return min;
    }

    //攻略率100%が出たら、以降すべての数字を100%にする関数
    void ZeroToOneAfterOne(ref double[,] list)
    {
        for (int i_d = 0; i_d < list.GetLength(0); i_d++)
        {
            bool cleared = false;
            for (int i_l = 0; i_l < list.GetLength(1); i_l++)
            {
                if (list[i_d, i_l] >= 0.99d)
                {
                    cleared = true;
                }
                if (cleared)
                {
                    list[i_d, i_l] = 1;
                }
            }
        }
    }



    void CheckNextLevel_enemy()
    {
        if (WinNum + loseNum >= duration)
        {
            result_oneLevel[(int)dunKind, currentLevel] = (float)sum_FloorNum / (float)(duration * maxFloorNum);
            result_oneLevel[(int)dunKind, currentLevel] = Domain(result_oneLevel[(int)dunKind, currentLevel], 1, 0);
            dataList[currentLevel] = new DataSet(((float)winNum / (float)duration), ((float)sum_FloorNum / (float)(duration * maxFloorNum)));//本当はダンジョン用と分けた方がいいかも
            if(completeRate_teacher[(int)dunKind, currentLevel - 1] < 0.01d)
            {
                if (dataList[currentEnemyLevel] != null)
                {
                    dataList[currentEnemyLevel].FloorRate = 0;
                }
            }

            if (winNum == duration)
            {
                End_enemy();
            }
            else
            {
                currentLevel++;
                main.status.LevelUp_Debug();//スタータスの更新
                totalNum = 0;
                WinNum = 0;
                loseNum = 0;
                sum_FloorNum = 0;
                maxFloorNum = 0;
            }
        }

        //if (enemyFlg)
        //{
        //    if(currentLevel > maxlevel)
        //    {
        //        End_enemy();
        //    }
        //    if(completeRate_teacher[currentLevel, (int)dunKind] < 0.001)
        //    {
        //        currentLevel++;
        //        return;
        //    }
        //    if(!(dunKind == DungeonKind.small_hill || dunKind == DungeonKind.plain || dunKind == DungeonKind.oak_forest))
        //    {
        //        EndEnemy();
        //        return; 
        //    }
        //}
    }

    //void SkipDungeon_enemy()
    //{
    //    if (completeRate_teacher[currentLevel, (int)dunKind] < 0.001)
    //    {
    //        currentLevel++;
    //        return;
    //    }
    //}

    void ControlLevel()
    {
        main.rsc.Value[(int)ResourceKind.exp] = 0;
        main.SR.level = currentLevel;
    }

    [ContextMenu("Check_thisDungeon")]
    void CalculateThisDungeon()
    {
        CalculateFloor();
        calKind = CalculateKind.oneDungeon;
    }

    void CalculateFloor()
    {
        if (dunKind == DungeonKind.nothing) { return; }
        //Debug.Log(main.enumCtrl.dungeons[(int)dunKind].Name() + "の計測開始. floor.");
        
        currentLevel = minLevel;
        main.status.InitStatus_Debug();
        for (int i = 0; i < currentLevel - 1; i++)
        {
            main.status.LevelUp_Debug();
        }
        dataList = new DataSet[100];//listの初期化
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
        calKind = CalculateKind.allDungeon;
        dunKind = (DungeonKind)1;
        dataList_wholeD = new DataSet[main.SD.num_dungeon, 100];
        CalculateFloor();
    }






    double[] loss;
    double[,] result_oneLevel;
    double[,] completeRate_teacher;
    int minLevel_Enemy, maxLevel_Enemy;
    int currentEnemyLevel;
    public int? CurrentEnemyLevel(EnemyKind kind)
    {
        if (kind == eneKind)
        { return currentEnemyLevel; }
        else { return null; } 
    }

    [ContextMenu("Check_OneEnemy")]
    void CalculateEnemyLevel()
    {
        if (eneKind == EnemyKind.nothing) { return; }
        CalculateEnemyLevel(eneKind);
    }

    //現状一番高等な関数
    void CalculateEnemyLevel(EnemyKind eKind)
    {
        eneKind = eKind;
        minLevel_Enemy = enemyLevels[(int)eKind].minLevel;
        maxLevel_Enemy = enemyLevels[(int)eKind].maxLevel;
        currentEnemyLevel = minLevel_Enemy;
        loss = new double[100];
        completeRate_teacher = Read_Data_T();
        calKind = CalculateKind.oneEnemy;

        CalculateEnemyOneLevelAllDungeon();
    }

    //レベルリセットのたびに呼ばれる関数
    void CalculateEnemyOneLevelAllDungeon()
    {
        //Debug.Log("現在のレベルは" + currentEnemyLevel);
        //敵のステータスの更新
        main.enemyParameter.parameters[(int)eneKind].level = currentEnemyLevel;
        main.battleCtrl.enemys[(int)eneKind].UpdateParameter();
        dunKind = (DungeonKind)1;
        CalculateFloor();
        result_oneLevel = new double[main.SD.num_dungeon, enemy_t_sheet.sheets[0].list.Count];
    }


    public Entity_enemy_t enemy_t_sheet;
    double[,] Read_Data_T()
    {
        double[,] sum = new double[main.SD.num_dungeon, enemy_t_sheet.sheets[0].list.Count];
        for (int i = 0; i < enemy_t_sheet.sheets[0].list.Count; i++)
        {
            for (int i_d = 0; i_d < main.SD.num_dungeon; i_d++)
            {
                switch ((DungeonKind)i_d)
                {
                    case DungeonKind.edge_of_town:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].EdgeOfTown;
                        break;
                    case DungeonKind.small_hill:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].SmallHill;
                        break;
                    case DungeonKind.plain:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].Plain;
                        break;
                    case DungeonKind.lost_forest:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].LostForest;
                        break;
                    case DungeonKind.oak_forest:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].OakForest;
                        break;
                    case DungeonKind.moor:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].moor;
                        break;
                    case DungeonKind.hoarding_house:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].hoardingHouse;
                        break;
                    case DungeonKind.sewer:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].sewer;
                        break;
                    case DungeonKind.bog:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].bog;
                        break;
                    case DungeonKind.demonic_cellar:
                        sum[i_d, i] = enemy_t_sheet.sheets[0].list[i].demonic_cellar;
                        break;
                    default:
                        break;
                }
            }
        }
        return sum;
    }
}
