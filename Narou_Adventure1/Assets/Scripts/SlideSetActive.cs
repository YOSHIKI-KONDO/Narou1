using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlideSetActive : MonoBehaviour
{
    public bool LimitedFlg;
    [SerializeField]
    private int leftLimit;
    public int LeftLimit { get { return leftLimit; } set { LimitedFlg = true; leftLimit = value; } }

    //インスタンス化直後から移動している設定。インスペクターからのみ設定できる。
    [SerializeField]
    private bool InitSlideFlg;
    [SerializeField]
    private int InitLeftSlide;

    public (int x, int y) currentPage;
    RectTransform rectTransform;
    Vector2 tempVec2 = new Vector2();
    Vector2 initPos;

    // Use this for initialization
    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        initPos = rectTransform.anchoredPosition;

        if (InitSlideFlg)
        {
            Left(InitLeftSlide);
        }
    }

    public void LeftForToggle(bool isOn)
    {
        if (isOn)
        {
            ResetPosition();
        }
        else
        {
            Left(1);
        }
    }

    public void Left(int Page = 1)
    {
        if (LimitedFlg)
        {
            if (currentPage.x <= LeftLimit * (-1)) { return; }
            currentPage.x -= Page;
        }

        tempVec2 = rectTransform.anchoredPosition;
        tempVec2.x -= Screen.width * Page;
        rectTransform.anchoredPosition = tempVec2;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initPos;
        if (LimitedFlg) { currentPage = (0, 0); }
    }
}
