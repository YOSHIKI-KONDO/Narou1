using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using UnityEngine.Analytics;

public class AnalyticsCtrl : BASE {

    //一度だけ呼ばれるようにした
    public void DungeonEnter(DungeonKind kind)
    {
        if (kind == DungeonKind.nothing) { return; }
        int index = (int)kind - 1;
        int startDungeonNum = 1;
        int endDungeonNum = 10;
        int EventPageNum = index / 10; //0~9:0page, 10~19:1page, 20~29:2page
        string customEventName = (startDungeonNum + EventPageNum).ToString() + "_" +
            (endDungeonNum + EventPageNum).ToString() + "_Dungeon_Enter";
        Dictionary<string, object> dic = new Dictionary<string, object> { { "Name", kind.ToString()} };
        if(main.SR.sent_enterAnalytics_Dungeon[(int)kind] == false)
        {
            AnalyticsEvent.Custom(customEventName, dic);
            main.SR.sent_enterAnalytics_Dungeon[(int)kind] = true;
        }
    }

    //一度だけ呼ばれるようにした
    public void DungeonComplete(DungeonKind kind)
    {
        if (kind == DungeonKind.nothing) { return; }
        int index = (int)kind - 1;
        int startDungeonNum = 1;
        int endDungeonNum = 10;
        int EventPageNum = index / 10; //0~9:0page, 10~19:1page, 20~29:2page
        string customEventName = (startDungeonNum + EventPageNum).ToString() + "_" +
            (endDungeonNum + EventPageNum).ToString() + "_Dungeon_Complete";
        Dictionary<string, object> dic = new Dictionary<string, object> { { "Name", kind.ToString() } };
        
        if (main.SR.sent_completeAnalytics_Dungeon[(int)kind] == false)
        {
            AnalyticsEvent.Custom(customEventName, dic);
            main.SR.sent_completeAnalytics_Dungeon[(int)kind] = true;
        }
    }

    public void StartGame()
    {
        AnalyticsEvent.TutorialStart("Game Start");
    }

    public void TutorialComplete()
    {
        AnalyticsEvent.TutorialComplete("Tutorial Complete");
    }

    private void Awake()
    {
        StartBASE();
    }
}
