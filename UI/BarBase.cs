using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.ModLoader;

namespace ElementMachine.UI
{
    public class BarBase
    {
        //这四个是用来处理拖动的
        private bool moveFlag = false;
        private bool mousePress = false;
        private Vector2 oldPos;
        private Vector2 startVec;
        private Vector2 Position;
        private Vector2 Center 
        {
            get => Position + new Vector2(BoxWidth, BoxHeight) / 2;
            set => Position = value - new Vector2(BoxWidth, BoxHeight) / 2;
        }
        private readonly Func<float> maxValue, value;
        private readonly Func<bool> visible;
        private Texture2D box, bar;
        private int BoxHeight => box.Height;
        private int BoxWidth => box.Width;
        private int BarHeight => bar.Height;
        private int BarWidth => bar.Width;
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, BoxWidth, BoxHeight);
        public bool MouseInRectangle => Rectangle.Intersects(new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 1, 1));
        private readonly bool canMove;
        public bool IsSetBarAndBox = false;
        public void SetBoxAndBar(Texture2D box, Texture2D bar)
        {
            IsSetBarAndBox = true;
            this.bar = bar;
            this.box = box;
            IsSetBarAndBox = false;
        }
        internal BarBase(Func<float> value, Func<float> maxValue, Texture2D box, Texture2D bar, Func<bool> visible, Vector2 startPos, bool canMove)
        {
            this.value = value;
            this.maxValue = maxValue;
            this.box = box;
            this.bar = bar;
            this.visible = visible;
            this.canMove = canMove;
            Center = startPos;
        }
        public int top, left;
        internal BarBase(int top, int left, Func<float> value, Func<float> maxValue, Texture2D box, Texture2D bar, Func<bool> visible, Vector2 startPos)
        {
            this.top = top;
            this.left = left;
            this.value = value;
            this.maxValue = maxValue;
            this.bar = bar;
            this.box = box;
            this.visible = visible;
            Position = startPos;
        }
        internal void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (!visible() || IsSetBarAndBox) return;
            float progress = value() / maxValue();
            Vector2 vec = new Vector2(0, -10);
            spriteBatch.Draw(bar, Position + new Vector2(BoxWidth - BarWidth, BoxHeight - BarHeight) / 2, new Rectangle(0, 0, (int)(BarWidth * progress), BarHeight), Color.White);       
            spriteBatch.Draw(box, Position, Color.White);
        }
        internal void Update()
        {
            if (!visible() || IsSetBarAndBox) return;
            if (canMove)
            {
                if (MouseInRectangle)
                {
                    if (Main.mouseLeft && !mousePress)
                    {
                        moveFlag = true;
                        startVec = Main.MouseScreen;
                        oldPos = Position;
                    }
                }
                if (moveFlag && Main.mouseLeft)
                {
                    Position = Main.MouseScreen - startVec + oldPos;
                }
                else
                {
                    moveFlag = false;
                }
                mousePress = Main.mouseLeft;
            }
        }
    }
}