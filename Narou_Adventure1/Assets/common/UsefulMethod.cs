using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
//using MathNet.Numerics.Distributions;
using static System.Math;

public class UsefulMethod : MonoBehaviour
{

    /// <summary>
    /// テキストを指定するだけで，マウスオーバーでテキストを表示させる関数．
    /// </summary>
    public GameObject windowPre;
    public Transform windowTransform;
    public static GameObject window;

    public delegate void usefuleDelegate();

    public static void addWindow(string text, ref GameObject game)
    {
        game.AddComponent<EventTrigger>().triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry2.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((x) => setActive(window, text));
        entry2.callback.AddListener((x) => setFalse(window)); //ラムダ式の右側は追加するメソッドです。

        game.AddComponent<EventTrigger>().triggers.Add(entry);
        game.AddComponent<EventTrigger>().triggers.Add(entry2);

        //window.transform.GetChild(0).GetComponent<Text>().text = text;
    }
    public static void addWindowOver(string text, ref Button game)
    {
        game.gameObject.AddComponent<EventTrigger>().triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry2.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((x) => setActive(window, text));
        entry2.callback.AddListener((x) => setFalse(window)); //ラムダ式の右側は追加するメソッドです。

        game.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
        game.gameObject.AddComponent<EventTrigger>().triggers.Add(entry2);

        //window.transform.GetChild(0).GetComponent<Text>().text = text;
    }
    public static void setOnPointerClick(ref GameObject game)
    {
        game.AddComponent<EventTrigger>().triggers = new List<EventTrigger.Entry>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        game.GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public static void setActive(GameObject go, string text)
    {
        go.SetActive(true);
        go.transform.GetChild(0).GetComponent<Text>().text = text;
    }
    public static void setActive(GameObject go)
    {
        if (go == null) { return; }
        if (!go.activeSelf)
        {
            go.SetActive(true);
        }
    }

    public static void setActive(GameObject go, bool bo)
    {
        if (go == null) { return; }
        if (!go.activeSelf&&bo)
        {
            go.SetActive(true);
        }
    }

    public static void setFalse(GameObject go)
    {
        if(go == null) { return; }
        if (go.activeSelf)
        {
            go.SetActive(false);
        }
    }
    /// <summary>
    /// 桁数が大きいものを，アルファベット表記に変える関数
    /// </summary>
    public static string[] digit = new string[]
{
       "", "K","M","B","T","Qa","Qi","Sx","Sp","No","Dc","Ud","Dd","Td","Qad","Qid","Sxd","Spd","Ods","Nod","Vg","Uvg","Dvg","Tvg","Qavg","Qivg","Sxvg","Spvg","Ocvs","Novg",
       "Tg","Utg","Dtg","Ttg","Qatg","Qitg","Sxts","Sptg","Octg","Notg"
};
    public static string tDigit(double i)
    {
        string tempString;
        string showNum = "";
        double tempNum = i;
        string iString = tempNum.ToString("F");

        if (i == 1000)
        {
            return "1K";
        }

        if (i == 1000000)
        {
            return "1M";
        }

        if (i >= 1000)
        {
            int count = 0;
            int digitNum = (int)Math.Log10(i) + 1;
            while (true)
            {
                double syou = tempNum / 1000;
                tempNum = syou;
                count++;
                if (tempNum < 1)
                {
                    break;
                }

            }
            if (digitNum % 3 == 0)
            {
                tempString = iString.Substring(0, 6);
                showNum = tempString.Substring(0, 3) + "." + tempString.Substring(3, 1);
            }
            else if (digitNum % 3 == 1)
            {
                tempString = iString.Substring(0, 4);
                showNum = tempString.Substring(0, 1) + "." + tempString.Substring(1, 1);
            }
            else if (digitNum % 3 == 2)
            {
                tempString = iString.Substring(0, 5);
                showNum = tempString.Substring(0, 2) + "." + tempString.Substring(2, 1);
            }

            return showNum + digit[count - 1];

        }

        return ((int)i).ToString("F0");

    }
    public static string tDigit(double i,int j, bool truncate = false)
    {
        string tempString;
        string showNum = "";
        double tempNum = i;
        string iString = tempNum.ToString("F");

        if (i > 1000)
        {
            int count = 0;
            int digitNum = (int)Math.Log10(i) + 1;
            while (true)
            {
                double syou = tempNum / 1000;
                tempNum = syou;
                count++;
                if (tempNum < 1)
                {
                    break;
                }

            }
            if (digitNum % 3 == 0)
            {
                tempString = iString.Substring(0, 6);
                showNum = tempString.Substring(0, 3) + "." + tempString.Substring(3, 1);
            }
            else if (digitNum % 3 == 1)
            {
                tempString = iString.Substring(0, 4);
                showNum = tempString.Substring(0, 1) + "." + tempString.Substring(1, 1);
            }
            else if (digitNum % 3 == 2)
            {
                tempString = iString.Substring(0, 5);
                showNum = tempString.Substring(0, 2) + "." + tempString.Substring(2, 1);
            }

            return showNum + digit[count - 1];

        }

        switch(j){
            case 0:
                /* 追加 */
                if (truncate)
                {
                    i = Math.Floor(i);
                }
                /*  */
                return i.ToString("F0");
            case 1:
                return i.ToString("F1");
            case 2:
                return i.ToString("F2");
            case 3:
                return i.ToString("F3");
            default:
                return "";
        }

    }
    public static string tDigitColor(double i, int? j = null)
    {
        string tempString;
        string showNum = "";
        double tempNum = i;
        string iString = tempNum.ToString("F");

        if (i > 1000)
        {
            int count = 0;
            int digitNum = (int)Math.Log10(i) + 1;
            while (true)
            {
                double syou = tempNum / 1000;
                tempNum = syou;
                count++;
                if (tempNum < 1)
                {
                    break;
                }

            }
            if (digitNum % 3 == 0)
            {
                tempString = iString.Substring(0, 6);
                showNum = tempString.Substring(0, 3) + "." + tempString.Substring(3, 1);
            }
            else if (digitNum % 3 == 1)
            {
                tempString = iString.Substring(0, 4);
                showNum = tempString.Substring(0, 1) + "." + tempString.Substring(1, 1);
            }
            else if (digitNum % 3 == 2)
            {
                tempString = iString.Substring(0, 5);
                showNum = tempString.Substring(0, 2) + "." + tempString.Substring(2, 1);
            }

            return showNum +"<color=\"yellow\">" +digit[count - 1];

        }

        switch (j)
        {
            case 0:
                return i.ToString("F0");
            case 1:
                return i.ToString("F1");
            case 2:
                return i.ToString("F2");
            case 3:
                return i.ToString("F3");
            default:
                return i.ToString("F0");
        }
    }
    public static void writeGyoretsu<T>(T[,] ary)
    {
        string str = "";
        int FEcount = 0;
        foreach (var expInt in ary)
        {
            str = str + "," + expInt.ToString();
            FEcount++;
            if (FEcount % ary.GetLength(1) == 0)
            {
                str = str + "\n";
            }

        }
        Debug.Log(str);
    }
    public static void writeList(List<int> list)
    {
        string str = "";
        foreach (int num in list)
        {
            str = str + "," + num.ToString();
        }
        Debug.Log(str);
    }

    public static void writeList(List<double> list)
    {
        string str = "";
        foreach (double num in list)
        {
            str = str + "," + num.ToString("F1");
        }
        Debug.Log(str);
    }

    public static void writeArray(int[] Array)
    {
        string str = "";
        foreach (int num in Array)
        {
            str = str + "," + num.ToString("F1");
        }
        Debug.Log(str);
    }
    public static Vector2 normalize(Vector2 vector)
    {
        float x = vector.x;
        float y = vector.y;
        float normalizeFactor = 1.0f / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        if(float.IsNaN(normalizeFactor))
        {
            return new Vector2(0, 0);
        }
        Vector2 vector2 = new Vector2(x * normalizeFactor, y * normalizeFactor);
        return vector2;
    }

    public static float vectorAbs(Vector2 vector)
    {
        float x = vector.x;
        float y = vector.y;
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }

    public static IEnumerator NewInvokeCor(usefuleDelegate Dele, float Time)
    {
        yield return new WaitForSeconds(Time);
        Dele();
    }

    //減算の処理
    public static bool IfCanSubSub(ref double Target, double Value)
    {
        if (Target >= Value)
        {
            Target -= Value;
            return true;
        }
        else
        {
            Debug.Log("値が大きすぎます");
            return false;
        }
    }



    //windowを出すコルーチン
    public static IEnumerator bigAndBig(GameObject targetUI)
    {
        targetUI.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        targetUI.SetActive(true);
        for (float i = 0.5f; i <= 1.0f; i += 0.1f)
        {
            targetUI.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        targetUI.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    //windowを消すコルーチン
    public static IEnumerator smallAndSmall(GameObject targetUI)
    {
        targetUI.GetComponent<RectTransform>().localScale = Vector3.one;
        for (float i = 1.0f; i >= 0.5f; i -= 0.1f)
        {
            targetUI.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        targetUI.SetActive(false);
        targetUI.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    //windowを非アクティブにせず、消すコルーチン
    public static IEnumerator smallAndSmallActive(GameObject targetUI)
    {
        targetUI.GetComponent<RectTransform>().localScale = Vector3.one;
        for (float i = 1.0f; i >= 0.5f; i -= 0.1f)
        {
            targetUI.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        //targetUI.SetActive(false);
        targetUI.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    //ポジティブな音
    public static void playPositiveSound()
    {
        GameObject tempObject = GameObject.FindWithTag("mainCtrl");
        Main tempMain = tempObject.GetComponent<Main>();
        tempMain.SoundEffectSource.PlayOneShot(tempMain.sound.positiveClip);
    }

    //ネガティブな音
    public static void playNegativeSound()
    {
        GameObject tempObject = GameObject.FindWithTag("mainCtrl");
        Main tempMain = tempObject.GetComponent<Main>();
        tempMain.SoundEffectSource.PlayOneShot(tempMain.sound.negativeClip);
    }

    //なんでもいい音
    public static void playClip(AudioClip Clip)
    {
        GameObject tempObject = GameObject.FindWithTag("mainCtrl");
        Main tempMain = tempObject.GetComponent<Main>();
        tempMain.SoundEffectSource.PlayOneShot(Clip);
    }

    //
    public static Main GetMain()
    {
        GameObject mainCtrl = GameObject.FindGameObjectWithTag("mainCtrl");
        return mainCtrl.GetComponent<Main>();
    }

    //
    public static IEnumerator EasyForCor(usefuleDelegate dele, int num, float interval)
    {
        for (int i = 0; i < num; i++)
        {
            dele();
            yield return new WaitForSeconds(interval);
        }
    }

    //the difference of Datetime to float
    public static float DeltaTimeFloat(DateTime DT)
    {
        if(DT == null)
        {
            return 0;
        }

        float ans = ((float)DateTime.Now.Subtract(DT).Seconds
            + DateTime.Now.Subtract(DT).Minutes * 60
            + DateTime.Now.Subtract(DT).Hours * 3600
            + DateTime.Now.Subtract(DT).Days * 86400);

        if(ans >= 0)
        {
            return ans;
        }
        else
        {
            return 0;
        }
    }

    public static string DoubleTimeToDate(double Time, bool CoronMode = false)
    {
        int Day = 0;
        int Hour = 0;
        int Minute = 0;
        int Second = 0;
        string DayString = "";
        string HourString = "";
        string MinuteString = "";
        string SecondString = "";

        Day = (int)Math.Floor(Time / 86400);
        Hour = (int)Math.Floor((Time % 86400) / 3600);
        Minute = (int)Math.Floor((Time % 3600) / 60);
        Second = (int)Math.Floor((Time % 60));

        if (CoronMode)
        {
            Hour = (int)Math.Floor(Time / 3600);
            Minute = (int)Math.Floor((Time % 3600) / 60);
            Second = (int)Math.Floor((Time % 60));
            if (Hour > 0) { HourString = Hour.ToString("D2") + ":"; }
            MinuteString = Minute.ToString("D2") + ":";
            SecondString = Second.ToString("D2");
        }
        else
        {
            Day = (int)Math.Floor(Time / 86400);
            Hour = (int)Math.Floor((Time % 86400) / 3600);
            Minute = (int)Math.Floor((Time % 3600) / 60);
            Second = (int)Math.Floor((Time % 60));
            if (Day > 0) { DayString = Day.ToString() + "d"; }
            if (Hour > 0) { HourString = Hour.ToString() + "h"; }
            if (Minute > 0) { MinuteString = Minute.ToString() + "m"; }
            if (Second > 0) { SecondString = Second.ToString() + "s"; }
        }

        return DayString + HourString + MinuteString + SecondString;
    }

    public static float Pdf_Ed(double Ramda, double X)
    {
        return 1.0f - (float)Math.Pow(Math.E, (-1 * Ramda * X));
    }

    public static double ZC(double number)//ZC stands for "Zero Check";
    {
        return Math.Max(0.000000001, number);
    }

    public static double Domain(double Value, double Max, double Min)
    {
        double tempDouble = 0;
        //tempDouble = Math.Max(Value, Min);
        //tempDouble = Math.Min(Value, Max);

        tempDouble = Value <= Min ? Min : Value;
        tempDouble = tempDouble >= Max ? Max : tempDouble;
        return tempDouble;
    }

    //public static double LogNormalDistribution(double Mode,double Mean)
    //{
    //    return (LogNormal.Sample(1d / 3d * Math.Log(Mean * Mean * Mode), Math.Sqrt(2d / 3d * Math.Log(Mean / Mode))));
    //}


    /// <summary>
    /// Componentを全探索するので重い。
    /// 指定されたインターフェイスを実装したコンポーネントのリストを返す
    /// </summary>
    public static List<Component> FindObjectsOfInterface<T>() where T : class
    {
        var list = new List<Component>();
        foreach (var n in FindObjectsOfType<Component>())
        {
            var Interface = n as T;
            if (Interface != null)
            {
                list.Add(n);
            }
        }
        return list;
    }


    public static bool isRange(float range, GameObject targetObject, GameObject homeObject)
    {
        if (homeObject.GetComponent<RectTransform>().anchoredPosition.x > targetObject.GetComponent<RectTransform>().anchoredPosition.x - range
                    && homeObject.GetComponent<RectTransform>().anchoredPosition.x < targetObject.GetComponent<RectTransform>().anchoredPosition.x + range
                     && homeObject.GetComponent<RectTransform>().anchoredPosition.y > targetObject.GetComponent<RectTransform>().anchoredPosition.y - range
                    && homeObject.GetComponent<RectTransform>().anchoredPosition.y < targetObject.GetComponent<RectTransform>().anchoredPosition.y + range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isRange(float range, Vector2 targetObject, GameObject homeObject)
    {
        if (homeObject.GetComponent<RectTransform>().anchoredPosition.x > targetObject.x - range
                    && homeObject.GetComponent<RectTransform>().anchoredPosition.x < targetObject.x + range
                     && homeObject.GetComponent<RectTransform>().anchoredPosition.y > targetObject.y - range
                    && homeObject.GetComponent<RectTransform>().anchoredPosition.y < targetObject.y + range)
        {
            return true;
        }
        else
        {
            return false;

        }
    }



    public static bool isRange(float range, Vector2 targetObject, Vector2 homeObject)
    {
        if (homeObject.x > targetObject.x - range
                    && homeObject.x < targetObject.x + range
                     && homeObject.y > targetObject.y - range
                    && homeObject.y < targetObject.y + range)
        {
            return true;
        }
        else
        {
            return false;

        }
    }


    /// <summary>
    /// delegateに代入して使うと、引数なしで第一引数の値がそのまま出力され、引数ありで第二引数が第一引数に代入される。
    /// </summary>
    public static Type Sync<Type>(ref Type Value, Type? Sub = null)
        where Type : struct
    {
        if (Sub == null)
        {
            return Value;
        }
        else
        {
            Value = (Type)Sub;
            return Value;
        }
    }

    /// <summary>
    /// 配列のサイズが第二引数のものと違ったら合わせる関数
    /// </summary>
    public static void InitializeArray<Type>(ref Type[] Obj, int Length)
    {
        if (Obj == null) { Obj = new Type[0]; }
        if (Obj.Length != Length)
        {
            Array.Resize(ref Obj, Length);
        }
    }

    public static void InstantiateIdle(Component IdleUnit, Transform Parent)
    {
        Component instaComponent;
        instaComponent = Instantiate(IdleUnit, Parent);
        if (IdleUnit is IIdleAction)
        {
            GetMain().idleActionCtrl.Add(instaComponent);
        }
    }


    public static void Vector3ToFloat(ref float x, ref float y, ref float z, Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public static void QuaternionToFloat(ref float x, ref float y, ref float z, ref float w, Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public static void Vector3ToFloat_Array(ref float[] x, ref float[] y, ref float[] z, Vector3[] vector3s)
    {
        for (int i = 0; i < x.Length; i++)
        {
            Vector3ToFloat(ref x[i], ref y[i], ref z[i], vector3s[i]);
        }
    }

    public static void QuaternionToFloat_Array(ref float[] x, ref float[] y, ref float[] z, ref float[] w, Quaternion[] quaternions)
    {
        for (int i = 0; i < x.Length; i++)
        {
            QuaternionToFloat(ref x[i], ref y[i], ref z[i], ref w[i], quaternions[i]);
        }
    }

    public static void FloatToVector3_Array(float[] x, float[] y, float[] z, ref Vector3[] vector3s)
    {
        for (int i = 0; i < x.Length; i++)
        {
            vector3s[i] = new Vector3(x[i], y[i], z[i]);
        }
    }

    public static void FloatToQuaternion_Array(float[] x, float[] y, float[] z, float[] w, ref Quaternion[] quaternions)
    {
        for (int i = 0; i < x.Length; i++)
        {
            quaternions[i] = new Quaternion(x[i], y[i], z[i], w[i]);
        }
    }


    /// <summary>
    /// Imageをアクティブにし、フェードインさせるコルーチン
    /// </summary>
    public static IEnumerator ImageFadeInCoroutine(Image img, float Duration = 0.1f, int iteration = 10)
    {
        img.gameObject.SetActive(true);
        Color cashColor = new Color(img.color.r, img.color.g, img.color.b, 1);
        Color tempColor = img.color;
        float interval = Duration / iteration;

        for (float i = 0.0f; i <= 1.0f; i += 1.0f / iteration)
        {
            tempColor.a = i;
            img.color = tempColor;
            yield return new WaitForSeconds(interval);
        }
        img.color = cashColor;
    }

    /// <summary>
    /// Imageをfalseにし、フェードアウトさせるコルーチン
    /// </summary>
    public static IEnumerator ImageFadeOutCoroutine(Image img, float Duration = 0.1f, int iteration = 10)
    {
        img.gameObject.SetActive(true);
        Color cashColor = img.color;
        Color tempColor = img.color;

        float interval = Duration / iteration;

        for (float i = 1.0f; i >= 0.0f; i -= 1.0f / iteration)
        {
            tempColor.a = i;
            img.color = tempColor;
            yield return new WaitForSeconds(interval);
        }
        img.gameObject.SetActive(false);
        img.color = cashColor;
    }

    /// <summary>
    /// Textをアクティブにし、フェードインさせるコルーチン
    /// </summary>
    public static IEnumerator TextFadeInCoroutine(Text txt, float Duration = 0.1f, int iteration = 10)
    {
        txt.gameObject.SetActive(true);
        Color cashColor = txt.color;
        Color tempColor = txt.color;
        txt.color = Color.clear;
        float interval = Duration / iteration;

        for (float i = 0.0f; i <= 1.0f; i += 1.0f / iteration)
        {
            tempColor.a = i;
            txt.color = tempColor;
            yield return new WaitForSeconds(interval);
        }
        txt.color = cashColor;
    }

    /// <summary>
    /// Textをfalseにし、フェードアウトさせるコルーチン
    /// </summary>
    public static IEnumerator TextFadeOutCoroutine(Text txt, float Duration = 0.1f, int iteration = 10)
    {
        Color cashColor = txt.color;
        Color tempColor = txt.color;

        float interval = Duration / iteration;

        for (float i = 1.0f; i >= 0.0f; i -= 1.0f / iteration)
        {
            tempColor.a = i;
            txt.color = tempColor;
            yield return new WaitForSeconds(interval);
        }
        txt.color = Color.clear;
        txt.gameObject.SetActive(false);
        txt.color = cashColor;
    }

    //overload
    public static IEnumerator TextFadeOutCoroutine(TextMeshProUGUI txt, float Duration = 0.1f, int iteration = 10)
    {
        Color cashColor = txt.color;
        Color tempColor = txt.color;

        float interval = Duration / iteration;

        for (float i = 1.0f; i >= 0.0f; i -= 1.0f / iteration)
        {
            tempColor.a = i;
            txt.color = tempColor;
            yield return new WaitForSeconds(interval);
        }
        txt.color = Color.clear;
        txt.gameObject.SetActive(false);
        txt.color = cashColor;
    }

    /// <summary>
    /// canvasのalpha0 -> 1
    /// </summary>
    public static IEnumerator CanvasFadeInCoroutine(CanvasGroup canvas, float Duration = 0.1f, int iteration = 10)
    {
        float interval = Duration / iteration;

        for (float i = 0.0f; i <= 1.0f; i += 1.0f / iteration)
        {
            canvas.alpha = i;
            yield return new WaitForSeconds(interval);
        }
        canvas.alpha = 1;
    }

    /// <summary>
    /// canvasのalpha1 -> 0
    /// </summary>
    public static IEnumerator CanvasFadeOutCoroutine(CanvasGroup canvas, float Duration = 0.1f, int iteration = 10)
    {
        float interval = Duration / iteration;
        for (float i = 1.0f; i >= 0.0f; i -= 1.0f / iteration)
        {
            canvas.alpha = i;
            yield return new WaitForSeconds(interval);
        }
        canvas.alpha = 0;
    }

    /// <summary>
    /// 2時間数的にRecttransformを移動させる関数。
    /// 調整が難しい。
    /// 例)0.5fで200fずらすなら,v0は-500ほど
    /// </summary>
    public static IEnumerator MoveParabora(RectTransform rect, Vector2 X, Vector2 v0, float t, int iteration, bool debug = false)
    {
        float ax = 0;
        float ay = 0;
        float x0 = rect.anchoredPosition.x;
        float y0 = rect.anchoredPosition.y;
        try
        {
            checked
            {
                ax = 2f / (t * t) * ((X.x - x0) - v0.x * t);
                ay = 2f / (t * t) * ((X.y - y0) - v0.y * t);
            }
        }
        catch (OverflowException ex)
        {
            Debug.Log(ex.Message);
        }
        if (debug)
        {
            float x_barance = x0 + (-1f * v0.x * v0.x) / (2f * ax);
            float y_barance = y0 + (-1f * v0.y * v0.y) / (2f * ay);
            float v0x_just = 2f * (X.x - x0) / t;
            float v0y_just = 2f * (X.y - y0) / t;
            Debug.Log("a : " + new Vector2(ax, ay) +
                 "\n 速度が0と予想される座標 :" + new Vector2(x_barance, y_barance) +
                 "\n 速度が0で到着する初速度 :" + new Vector2(v0x_just, v0y_just));
        }

        float interval = (float)t / iteration;
        var tmp_pos = rect.anchoredPosition;
        float T = 0;
        for (int i = 1; i <= iteration; i++)
        {
            T = i * interval;
            tmp_pos.x = x0 + v0.x * T + 0.5f * ax * T * T;
            tmp_pos.y = y0 + v0.y * T + 0.5f * ay * T * T;
            rect.anchoredPosition = tmp_pos;
            yield return new WaitForSeconds(interval);
        }
        rect.anchoredPosition = X;
    }

    /// <summary>
    /// 複数の値から最大値を取る関数
    /// </summary>
    public static (T max, int index) Max<T>(params T[] nums)
        where T : IComparable
    {
        if (nums.Length == 0) return (default(T), 0);
        T max = nums[0];
        int index = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (max.CompareTo(nums[i]) <= 0)
            {
                max = nums[i];
                index = i;
            }
        }
        return (max, index);
    }

    /// <summary>
    /// 複数の値から最大値を取る関数
    /// </summary>
    public static (T max, int index) Min<T>(params T[] nums)
        where T : IComparable
    {
        if (nums.Length == 0) return (default(T), 0);
        T min = nums[0];
        int index = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (min.CompareTo(nums[i]) > 0)
            {
                min = nums[i];
                index = i;
            }
        }
        return (min, index);
    }

    /// <summary>
    /// 平均二乗誤差、2乗和誤差
    /// </summary>
    public static double Mean_squared_error(double[,] y, double[,] t)
    {
        double[,] sub = new double[y.GetLength(0), y.GetLength(1)];
        double sum = 0;
        for (int i = 0; i < y.GetLength(0); i++)
        {
            for (int j = 0; j < y.GetLength(1); j++)
            {
                sub[i, j] = Math.Pow(y[i, j] - t[i, j], 2);
            }
        }
        foreach (var _sub in sub)
        {
            sum += _sub;
        }
        return 0.5d * sum;
    }

    /// <summary>
    /// RectTransformを持たないオブジェクトのscaleの最適な値を求める関数。
    /// targetObj...大きさを求めたいオブジェクト。
    /// parentObj...合わせたいUIの親のcanvas(object)。
    /// ui_x, ui_y...合わせたいUIの横、縦
    /// target_x, target_y...targetObjの縦、横。
    /// </summary>
    public static (float scale_x, float scale_y) ResizeObjForUI(GameObject targetObj, GameObject parentObj, float ui_x, float ui_y, float target_x = 1.0f, float target_y = 1.0f)
    {
        float d_obj = (targetObj.transform.position.z - Camera.main.gameObject.transform.position.z);
        float d_canvas = (parentObj.transform.position.z - Camera.main.gameObject.transform.position.z);
        return ((d_obj / d_canvas) * ui_x / target_x, (d_obj / d_canvas) * ui_y / target_y);
    }

    public static void ArrayToDefault<T>(T[] ary)
        where T : struct
    {
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = default(T);
        }
    }

    public static double CalDmg(double dmg, double def)
    {
        double baseDmg = (dmg / 2d) - (def / 4d);
        int randomRange = (int)((1d / 2d) * Math.Sqrt(baseDmg));
        double randomFactor = UnityEngine.Random.Range(-randomRange, randomRange + 1);
        return (baseDmg + randomFactor) > 0.5 ? baseDmg + randomFactor : 0.5;
    }

    public static void ChangeTextAdaptive(string sentense, Text text, params GameObject[] objects)
    {
        if (sentense == "" || sentense == null)
        {
            foreach (var obj in objects)
            {
                setFalse(obj);
            }
        }
        else
        {
            text.text = sentense;
        }
    }

    //必要な時にのみ初期化する関数。軽量化用
    public static void InitializeList<T>(ref List<T> list)
    {
        if (list == null)
        {
            list = new List<T>();
        }
    }

    //boolを引数にとり、active状態を反転させる関数。インスペクターでボタンに設定して使う
    public static void SwitchActive(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }


    //テキストをファイルから読み込む（行読み）
    //※Android で SD カードから読み込みをするには、「AndroidManifest.xml」にパーミッション（"READ_EXTERNAL_STORAGE" または "WRITE_EXTERNAL_STORAGE"）が必要。
    public static string LoadText(string path)
    {
        StringBuilder sb = new StringBuilder(1024);  //※capacity は任意

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    sb.Append(line).Append("\n");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }

        return sb.ToString();
    }

    //テキストをファイルに保存
    //※Android で External Storage に書き込みをするには、「AndroidManifest.xml」にパーミッション（"WRITE_EXTERNAL_STORAGE"）が必要。
    //※セキュリティ上、Unity から直接 SD カードには保存できない。
    public static bool SaveText(string text, string path)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(text);
                writer.Flush();
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);  //Access to the path "filename" is denied. → パーミッションが無い, 書き込みアクセス不可（SDカードなど）
            return false;
        }
        return true;
    }



// Use this for initialization
void Start()
    {
        window = Instantiate(windowPre, windowTransform);
        window.name = "debaggudayo";
    }
    // Update is called once per frame
    void Update()
    {
        if (window != null)
        {
            if (Input.mousePosition.y >= 360 && Input.mousePosition.x >= 540)
            {
                window.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(-250.0f, -50.0f);
            }
            else if (Input.mousePosition.y >= 360 && Input.mousePosition.x < 540)
            {
                window.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(50.0f, -50.0f);
            }
            else if (Input.mousePosition.y < 360 && Input.mousePosition.x > 540)
            {
                window.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(-250.0f, -50.0f);
            }
            else if (Input.mousePosition.y < 360 && Input.mousePosition.x < 540)
            {
                window.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition + new Vector3(50.0f, 50.0f);
            }
        }
    }
}