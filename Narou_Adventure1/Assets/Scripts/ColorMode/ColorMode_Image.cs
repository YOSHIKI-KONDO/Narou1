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
    [RequireComponent(typeof(Image))]
    public class ColorMode_Image : BASE
    {
        Image image;
        public Color[] color;
        public Sprite[] sourceImage;

        private void Awake()
        {
            StartBASE();
            image = GetComponent<Image>();
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
                image.color = color[main.SR.colorMode - 1];
            }
            if (main.SR.colorMode > 0 && main.SR.colorMode <= sourceImage.Length)
            {
                image.sprite = sourceImage[main.SR.colorMode - 1];
            }
        }
    }
}
