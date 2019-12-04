using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// prefabにsetして使う。
/// PopUpで自信を複製してcanvasに出現させる。
/// positionにはtransform.positionを渡す。
/// </summary>
public class DamageText : MonoBehaviour
{
    Text text;
    RectTransform rect;
    /// <summary>
    /// PopUpで自信を複製してcanvasに出現させる。
    /// positionにはtransform.positionを渡す。
    /// </summary>
    public DamageText PopUp(Vector2 position, Transform parent, string sentense, Vector2 direction, float duration = 0.5f, int iteration = 10)
    {
        var temp_text = Instantiate(this, parent);
        temp_text.Initialize(position, direction, duration, iteration);
        temp_text.text.text = sentense;

        return temp_text;
    }

    void Initialize(Vector2 position, Vector2 direction, float duration = 0.1f, int iteration = 10)
    {
        text = GetComponent<Text>();
        rect = GetComponent<RectTransform>();
        transform.position = position;// + new Vector2(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));
        StartCoroutine(FadeOutAndUp(direction, duration, iteration));
    }

    IEnumerator FadeOutAndUp(Vector2 direction, float duration = 0.1f, int iteration = 10)
    {
        Color tempColor = text.color;

        float interval = duration / iteration;

        for (float i = 1.0f; i >= 0.0f; i -= 1.0f / iteration)
        {
            //color
            tempColor.a = i;
            text.color = tempColor;
            //position
            rect.anchoredPosition += direction;
            yield return new WaitForSeconds(interval);
        }
        Destroy(gameObject);
    }
}
