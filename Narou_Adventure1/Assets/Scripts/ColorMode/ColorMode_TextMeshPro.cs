using UnityEngine;
using TMPro;

namespace ColorMode
{
    /// <summary>
    /// main.SR.colorModeが0の時は変更なし
    /// 1のときは配列colorの1番目.color[0]が入る
    /// 2のときは配列colorの２番目.color[1]が入る
    /// ...
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ColorMode_TextMeshPro : BASE
    {
        TextMeshProUGUI text;
        public Color[] color;

        private void Awake()
        {
            StartBASE();
            text = GetComponent<TextMeshProUGUI>();
            Apply();
        }

        void Apply()
        {
            if (main.SR.colorMode == 0)
            {
                return;//デフォルトのまま
            }
            if (main.SR.colorMode > 0 && main.SR.colorMode <= color.Length)
            {
                text.color = color[main.SR.colorMode - 1];
            }
        }
    }
}
