using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ElementMachine.World;
using Terraria.ID;

namespace ElementMachine.Command
{
    public class SetTiles : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "setTiles";

        public override string Usage
            => "/setTiles x y height width 或 /setTiles player height width";

        public override string Description
            => "卖东西";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if(args.Length == 4)
            {
                int x = int.Parse(args[0]), y = int.Parse(args[1]);
                int width = int.Parse(args[3]), height = int.Parse(args[2]);
                Main.NewText(x.ToString() + y.ToString() + width.ToString() + height.ToString());
                Main.LocalPlayer.GetModPlayer<CommandPlayer>().tilesInfo.SetTiles(new Vector2(x, y), height, width);
                Main.LocalPlayer.GetModPlayer<CommandPlayer>().tilesInfo.Output();
            }
            if(args.Length == 3)
            {
                Vector2 vec = new Vector2((int)(Main.LocalPlayer.position.X / 16), (int)(Main.LocalPlayer.position.Y / 16) - 4);
                int width = int.Parse(args[2]), height = int.Parse(args[1]);
                Main.NewText(vec.ToString() + width.ToString() + " " + height.ToString());
                Main.LocalPlayer.GetModPlayer<CommandPlayer>().tilesInfo.SetTiles(vec, height, width);
                Main.LocalPlayer.GetModPlayer<CommandPlayer>().tilesInfo.Output();
            }
        }
    }
    public class ClearI : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "ClearI";

        public override string Usage
            => "ClearI";

        public override string Description
            => "";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer.AnalyzedItemsName.Clear();
            MyPlayer.AnalyzedItemsValue.Clear();
        }
    }
    public class Create : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "Create";

        public override string Usage
            => "Create C/ Create S";

        public override string Description
            => "";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText(args);
            if(args.Length > 0)
            {
                if(args[0] == "R") 
                {
                    MyPlayer.randomCreate = new RandomCreate(int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                    Main.NewText("刷新randomCreate为" + args[1] + " " + args[2] + " " + args[3]);
                }
                if(args[0] == "C") 
                {
                    MyPlayer.randomCreate.Create();
                    Main.NewText("randomCreate创建成功,当前randomCreate为" + MyPlayer.randomCreate.MaxHeight + " " + MyPlayer.randomCreate.MaxWidth + " " + MyPlayer.randomCreate.step);
                }
                if(args[0] == "S")
                {
                    MyPlayer.randomCreate.setToWorld();
                }
            }
        }
    }
}