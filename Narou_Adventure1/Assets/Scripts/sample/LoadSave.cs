using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class LoadSave : BASE {

    //public string filename = "test.txt";
    //public Text displayText;    //表示するUI-Text

    ////ロードボタンのコールバックハンドラ
    //public void OnLoadClick()
    //{
    //    if (string.IsNullOrEmpty(filename) || displayText == null)
    //        return;

    //    string path = Path.Combine(Application.persistentDataPath, filename);  //プラットフォームによってパスは異なる

    //    if (!File.Exists(path))
    //    {
    //        Debug.Log("Not found : " + path);
    //        return;
    //    }

    //    string text = LoadText(path);
    //    if (!string.IsNullOrEmpty(text))
    //        displayText.text = text;
    //}

    ////セーブボタンのコールバックハンドラ
    //public void OnSaveClick()
    //{
    //    if (string.IsNullOrEmpty(filename) || displayText == null)
    //        return;

    //    string path = Path.Combine(Application.persistentDataPath, filename);  //プラットフォームによってパスは異なる

    //    if (SaveText(displayText.text, path))
    //        Debug.Log("Save to : " + path);
    //}
}
