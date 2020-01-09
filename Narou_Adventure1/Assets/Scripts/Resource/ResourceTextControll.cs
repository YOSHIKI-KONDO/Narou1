using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static ResourceKind;

public class ResourceTextControll : BASE {
    public ResourceText normalPre;
    public ResourceText energyPre;
    public Transform parent_energy;
    //public Transform parent_normal;
    public RESOURCE_EFFECT[] effectAry;
    public DecimalPoint_Resource[] points;

    //小数点の設定
    //規定値 現在地:0, 最大値0, 回復:3, 現在地(Dealing):1, 最大値(Dealing):1, 回復(Dealing):2
    void SetDecimalPoint()
    {
        //ここに個別で小数点の位置を変える処理を書く。
        //エナジー
        foreach (var e in energies)
        {
            points[(int)e].ChangeAllPoint(1, 1, 2, 1, 1, 2);
        }

        /* ここから設定してね */
        points[(int)focus].ChangeAllPoint(1, 1, 2, 1, 1, 2); //一括で変える時
        points[(int)anchovy_sandwich].current_deal = 0;      //一つだけ変えるとき
    }

    List<ResourceKind> energies = new List<ResourceKind>
    { action,hp,mp,stamina,fire,water,wind,earth,thunder,ice,ResourceKind.light,dark,animal};

    public class DecimalPoint_Resource
    {
        public int current;
        public int max;
        public int regen;
        public int current_deal;
        public int max_deal;
        public int regen_deal;
        public DecimalPoint_Resource()
        {
            current = 0;
            max = 0;
            regen = 3;
            current_deal = 1;
            max_deal = 1;
            regen_deal = 3;
        }
        /// <summary>
        /// 小数点の位置を一括で変える
        /// </summary>
        /// <param name="current">普段一覧に出る現在地。規定値は0</param>
        /// <param name="max">普段一覧に出る最大値。規定値は0</param>
        /// <param name="regen">普段一覧のポップアップに出るリジェネ。規定値は3</param>
        /// <param name="current_deal">アクションなどで使う現在値。規定値は1</param>
        /// <param name="max_deal">アクションなどで使う最大値。規定値は1</param>
        /// <param name="regen_deal">アクションなどで使うリジェネ。規定値は2</param>
        public void ChangeAllPoint(int current, int max, int regen, int current_deal, int max_deal, int regen_deal)
        {
            this.current = current;
            this.max = max;
            this.regen = regen;
            this.current_deal = current_deal;
            this.max_deal = max_deal;
            this.regen_deal = regen_deal;
        }
    }
    
    //リソーステキストの表示/非表示、sliderあり/なしなど設定
    void Initialize()
    {
        for (int i = 0; i < Enum.GetNames(typeof(ResourceKind)).Length; i++)
        {
            // 表示しないリソース
            ResourceKind k = (ResourceKind)i;
            if (k == focus || k==equipSpace || k == inventorySpace || k == strength || k==mentalStrength||k==defense||k==dodge||k==criticalChance||
                k == exp || k == attack || k == magic_attack || k == ap || k == itemPoint1 || k == itemPoint2 || k == itemPoint3 || k == itemPoint4
                 || k == itemPoint5 || k == itemPoint6 || k == itemPoint7 || k == itemPoint8 || k == itemPoint9 || k == itemPoint10)
            {
                continue;
            }

            // エナジー
            if (energies.Contains(k))
            {
                var energy = Instantiate(energyPre, parent_energy);
                energy.kind = k;
                if (effectAry[i] != null)
                {
                    energy.effects = effectAry[i].effects;
                }
                continue;
            }

            // 普通のリソース
            var resource = Instantiate(normalPre, parent_energy);
            resource.kind = k;
            if (effectAry[i] != null)
            {
                resource.effects = effectAry[i].effects;
            }
        }
    }

    public void InitializeArray()
    {
        //if (effectAry != null) { return; }
        if (effectAry.Length != 0) { return; }
        effectAry = new RESOURCE_EFFECT[Enum.GetNames(typeof(ResourceKind)).Length];
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
        InitializeArray();
        points = new DecimalPoint_Resource[Enum.GetNames(typeof(ResourceKind)).Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new DecimalPoint_Resource();
        }
        SetDecimalPoint();
    }

	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
	}
}
