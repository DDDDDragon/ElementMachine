using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Buffs
{
    public class HydraulicEngine : ModBuff 
    {
        public override void SetDefaults() 
        {
            DisplayName.SetDefault("HydraulicEngine");
            DisplayName.AddTranslation(GameCulture.Chinese, "液压引擎");
        }
        public override void Update(Player player, ref int buffIndex) 
        {   
            if(player.GetModPlayer<BuffPlayer>().JuniorAlloyNum > 0)
            {
                player.buffTime[buffIndex] = 2;
                player.allDamage += 0.03f * player.GetModPlayer<BuffPlayer>().JuniorAlloyNum;
                player.GetModPlayer<BuffPlayer>().JuniorAlloyTimer--;
                if(player.GetModPlayer<BuffPlayer>().JuniorAlloyTimer == 0)
                {
                    player.GetModPlayer<BuffPlayer>().JuniorAlloyNum--;
                    player.GetModPlayer<BuffPlayer>().JuniorAlloyTimer = 120;
                }
            }
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "当前层数:" + Main.LocalPlayer.GetModPlayer<BuffPlayer>().JuniorAlloyNum.ToString();
            base.ModifyBuffTip(ref tip, ref rare);
        }
    }
}
