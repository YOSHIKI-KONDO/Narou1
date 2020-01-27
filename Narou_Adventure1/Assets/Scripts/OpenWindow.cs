using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    //こちらはうまくいった。
   public void Open(string url)
    {
        Application.ExternalEval("window.open(\"http://www.unity3d.com\")");
    }

    //こちらは動作しない。
    public void Open2(string url)
    {
        Application.ExternalEval(@"window.open(\" + url + @")");
    }
}
