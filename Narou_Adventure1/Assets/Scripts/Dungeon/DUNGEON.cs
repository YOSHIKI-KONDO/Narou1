using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class DUNGEON : BASE {
    public DungeonKind kind;
    public PopUp popUp;
    public DungeonFunction progress; //AddComponentだけして、行動選択とする
    public ReleaseFunction release;
    public NeedFunciton need;
    [NonSerialized]public Button button;
    Text text, sliderText;
    Slider slider;

    public List<EnemyKind[]> enemyList = new List<EnemyKind[]>(); //Enemyの配列のList
    public List<Drop> drops = new List<Drop>(); //ドロップ品
    public List<Dealing> progressCost = new List<Dealing>();
    public int MaxFloor() { return enemyList.Count; }
    public int currentFloor { get => main.SR.currentFloor_Dungeon[(int)kind]; set => main.SR.currentFloor_Dungeon[(int)kind] = value; }//save
    public bool summonedEnemy;

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    void Enter()
    {
        if(currentFloor >= MaxFloor())
        {
            currentFloor = 0;
        }
        main.announce.Add("You entered " + main.enumCtrl.dungeons[(int)kind].Name());
        main.battleCtrl.dunKind = kind;
        main.battleCtrl.Summon();
    }

    public string Name_str, Description_str, Need_str, Floor_str, ProgressCost_str, Drops_str;

    // Use this for initialization
    public void AwakeDungeon (DungeonKind kind) {
		StartBASE();
        this.kind = kind;
        main.battleCtrl.dungeons[(int)kind] = this;

        button = GetComponentInChildren<Button>();            //UI関連
        text = GetComponentsInChildren<Text>()[0];            //UI関連
        sliderText = GetComponentsInChildren<Text>()[1];      //UI関連
        slider = GetComponentInChildren<Slider>();            //UI関連
        button.onClick.AddListener(Enter);

        progress = gameObject.AddComponent<DungeonFunction>();
        progress.AwakeDungeon(button, main.enumCtrl.dungeons[(int)kind].Name());
        progress.SelectedAction = Enter;
        popUp = main.dungeonPopUp.StartPopUp(gameObject, main.windowShowCanvas);
        need = gameObject.AddComponent<NeedFunciton>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Dungeon[(int)kind], x), x => Sync(ref main.SR.completed_Dungeon[(int)kind], x), x => Requires());
    }

    // Use this for initialization
    public void StartDungeon () {
        ApplySlider();   
	}

    // Update is called once per frame
    public void UpdateDungeon () {
		
	}

    public void FixedUpdateDungeon()
    {
        text.text = Name_str;
        ApplyPopUp();

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }

        if (progress.isOn)
        {
            ApplySlider();
        }
    }

    void ApplySlider()
    {
        progress.sliderValue = (float)currentFloor / (float)MaxFloor();
        if(slider != null)
        {
            slider.value = progress.sliderValue;
            sliderText.text = currentFloor.ToString() + " / " + MaxFloor().ToString();
        }
    }


    void ApplyPopUp()
    {
        Name_str = main.enumCtrl.dungeons[(int)kind].Name();
        if(main.SR.clearNum_Dungeon[(int)kind] > 0)
        {
            Name_str += " (Cleared)";
        }
        if (popUp.gameObject.activeSelf)
        {
            ApplySlider();//本来はここじゃない
            //自動でコストの文章を生成
            Floor_str = currentFloor.ToString() + "/" + MaxFloor().ToString();
            Description_str = main.enumCtrl.dungeons[(int)kind].Description();
            ProgressCost_str = progress.ProgressDetail(progressCost);
            Drops_str = DropsDetail(drops);

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }


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

            if (Need_str == "" || Need_str == null)
            {
                setFalse(popUp.texts[2].gameObject);
                setFalse(popUp.texts[3].gameObject);
            }
            else
            {
                popUp.texts[3].text = Need_str;
            }

            if (Floor_str == "" || Floor_str == null)
            {
                setFalse(popUp.texts[4].gameObject);
                setFalse(popUp.texts[5].gameObject);
            }
            else
            {
                popUp.texts[5].text = Floor_str;
            }

            if (ProgressCost_str == "" || ProgressCost_str == null)
            {
                setFalse(popUp.texts[6].gameObject);
                setFalse(popUp.texts[7].gameObject);
            }
            else
            {
                popUp.texts[7].text = ProgressCost_str;
            }

            if (Drops_str == "" || Drops_str == null)
            {
                setFalse(popUp.texts[8].gameObject);
                setFalse(popUp.texts[9].gameObject);
            }
            else
            {
                popUp.texts[9].text = Drops_str;
            }
        }
    }
}
