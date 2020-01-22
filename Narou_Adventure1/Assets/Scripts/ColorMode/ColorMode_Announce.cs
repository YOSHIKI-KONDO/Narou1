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
    public class ColorMode_Announce : BASE
    {
        Announce announce;
        public Color[] color;

        private void Awake()
        {
            StartBASE();
            announce = GetComponent<Announce>();
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
                announce.baseColor = color[main.SR.colorMode - 1];
            }
        }
    }
}