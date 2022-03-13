using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace ElementMachine.UI
{
    /// <summary>
    /// 感谢星虹集的代码参考
    /// </summary>
    public class Drawing
    {
        public static void DrawAdvBox(SpriteBatch sp, int x, int y, int w, int h, Color c, Texture2D img, Vector2 size4, float scale = 1f)
        {
            int num = (int)((float)w * scale);
            int num2 = (int)((float)h * scale);
            x += (w - num) / 2;
            y += (h - num2) / 2;
            w = num;
            h = num2;
            int num3 = (int)size4.X;
            int num4 = (int)size4.Y;
            if ((float)w < size4.X)
            {
                w = num3;
            }
            if ((float)h < size4.Y)
            {
                h = num3;
            }
            sp.Draw(img, new Rectangle(x, y, num3, num4), new Rectangle?(new Rectangle(0, 0, num3, num4)), c);
            sp.Draw(img, new Rectangle(x + num3, y, w - num3 * 2, num4), new Rectangle?(new Rectangle(num3, 0, img.Width - num3 * 2, num4)), c);
            sp.Draw(img, new Rectangle(x + w - num3, y, num3, num4), new Rectangle?(new Rectangle(img.Width - num3, 0, num3, num4)), c);
            sp.Draw(img, new Rectangle(x, y + num4, num3, h - num4 * 2), new Rectangle?(new Rectangle(0, num4, num3, img.Height - num4 * 2)), c);
            sp.Draw(img, new Rectangle(x + num3, y + num4, w - num3 * 2, h - num4 * 2), new Rectangle?(new Rectangle(num3, num4, img.Width - num3 * 2, img.Height - num4 * 2)), c);
            sp.Draw(img, new Rectangle(x + w - num3, y + num4, num3, h - num4 * 2), new Rectangle?(new Rectangle(img.Width - num3, num4, num3, img.Height - num4 * 2)), c);
            sp.Draw(img, new Rectangle(x, y + h - num4, num3, num4), new Rectangle?(new Rectangle(0, img.Height - num4, num3, num4)), c);
            sp.Draw(img, new Rectangle(x + num3, y + h - num4, w - num3 * 2, num4), new Rectangle?(new Rectangle(num3, img.Height - num4, img.Width - num3 * 2, num4)), c);
            sp.Draw(img, new Rectangle(x + w - num3, y + h - num4, num3, num4), new Rectangle?(new Rectangle(img.Width - num3, img.Height - num4, num3, num4)), c);
        }

        public static void DrawAdvBox(SpriteBatch sp, Rectangle rect, Color c, Texture2D img, Vector2 size4)
        {
            Drawing.DrawAdvBox(sp, rect.X, rect.Y, rect.Width, rect.Height, c, img, size4, 1f);
        }
    }
}
