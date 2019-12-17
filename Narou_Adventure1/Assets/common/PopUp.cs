using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// PopUPを表示させるためのスクリプト。
/// 使い方
/// ①このスクリプトをプレハブにアタッチする。
/// ②そのプレハブをスクリプトから読み込む。
/// ③スクリプトから「Popup obj = プレハブ.StartPopUp(gameObject, 親のcanvas);」と呼ぶ
/// ④スクリプトのUpdateでobjのtextsとtextProsを更新する。
/// </summary>
public class PopUp : MonoBehaviour
{
    GameObject hoverObject;
    public Text[] texts;
    public TextMeshProUGUI[] textPros;
    [NonSerialized]
    public Vector3 Distance = new Vector3(150.0f, 75.0f);
    public Action EnterAction;
    public Action ExitAction;

    public PopUp StartPopUp(GameObject hoverObject, Transform parent)
    {
        var tmp_PopUp = Instantiate(this, parent);
        tmp_PopUp.hoverObject = hoverObject;
        tmp_PopUp.Initialize();

        return tmp_PopUp;
    }

    void Initialize()
    {
        texts = GetComponentsInChildren<Text>();
        textPros = GetComponentsInChildren<TextMeshProUGUI>();

        hoverObject.AddComponent<EventTrigger>().triggers = new List<EventTrigger.Entry>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry2.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((x) => ApplyPosition());
        entry.callback.AddListener((x) => gameObject.SetActive(true));
        entry.callback.AddListener((x) => EnterAction?.Invoke());
        entry2.callback.AddListener((x) => gameObject.SetActive(false));
        entry2.callback.AddListener((x) => ExitAction?.Invoke());
        hoverObject.GetComponent<EventTrigger>().triggers.Add(entry);
        hoverObject.GetComponent<EventTrigger>().triggers.Add(entry2);
    }

    void ApplyPosition()
    {
        if (Input.mousePosition.y >= Screen.height / 2 && Input.mousePosition.x >= Screen.width / 2)//第一象限
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(-Distance.x, -Distance.y);
        }
        else if (Input.mousePosition.y >= Screen.height / 2 && Input.mousePosition.x < Screen.width / 2)//第二象限
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(Distance.x, -Distance.y);
        }
        else if (Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x > Screen.width / 2)//第四象限
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(-Distance.x, Distance.y);
        }
        else if (Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.width / 2)//第三象限
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(Distance.x, Distance.y);
        }
    }

    private void Update()
    {
        ApplyPosition();
    }
}
