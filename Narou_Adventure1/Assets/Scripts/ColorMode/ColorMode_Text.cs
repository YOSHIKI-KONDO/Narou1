using UnityEngine;
using UnityEngine.UI;

namespace ColorMode
{
    /// <summary>
    /// main.SR.colorModeが0の時は変更なし
    /// 1のときは配列colorの1番目.color[0]が入る
    /// 2のときは配列colorの２番目.color[1]が入る
    /// ...
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class ColorMode_Text : BASE
    {
        Text text;
        public Color[] color;

        private void Awake()
        {
            StartBASE();
            text = GetComponent<Text>();
            Apply();
        }

        void Apply()
        {
            if(main.SR.colorMode == 0)
            {
                return;//デフォルトのまま
            }
            if(main.SR.colorMode > 0 && main.SR.colorMode <= color.Length)
            {
                text.color = color[main.SR.colorMode - 1];
            }
        }
    }
}
