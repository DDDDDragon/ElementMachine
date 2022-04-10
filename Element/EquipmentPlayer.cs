using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;
using Terraria.GameContent;
using ReLogic.Graphics;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using ElementMachine.Buffs;
namespace ElementMachine.Element
{
    public class EquipmentPlayer : ModPlayer
    {
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if(FrozenProtector)
            {
                foreach(var i in Main.npc)
                {
                    if(Vector2.Distance(i.Center, player.Center) <= 200)
                    {
                        i.AddBuff(ModContent.BuffType<lowerSpeed>(), 120);
                    }
                }
            }
            base.OnHitByNPC(npc, damage, crit);
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
            SandCracker = false;
            FrozenProtector = false;
            AntlionDash = false;
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if(SandCracker) 
            {
                if(Main.rand.Next(0, 11) == 0)
                {
                    target.AddBuff(ModContent.BuffType<Dizziness>(), 60);
                }
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }
        DateTime AdateTime = new DateTime();
        DateTime DdateTime = new DateTime();
        Keys[] pressedKeys;
        bool pressedA = false;
        bool pressedD = false;
        public override void PreUpdate()
        { 
            pressedKeys = Main.keyState.GetPressedKeys();
            Rectangle rectangle = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
            foreach(var i in Main.npc)
            {
                if(i.getRect().Intersects(rectangle) && (AntlionDashLeftStage || AntlionDashRightStage) && i.active && !i.dontTakeDamage && !i.friendly)
                {
                    float num = 5f * player.meleeDamage;
					float num2 = 9f;
					bool crit = false;
					if (player.kbGlove)
					{
						num2 *= 2f;
					}
					if (player.kbBuff)
					{
						num2 *= 1.5f;
					}
					if (Main.rand.Next(100) < player.meleeCrit)
					{
						crit = true;
					}
					int num3 = player.direction;
					if (player.velocity.X < 0f)
					{
						num3 = -1;
					}
					if (player.velocity.X > 0f)
					{
						num3 = 1;
					}
					if (player.whoAmI == Main.myPlayer)
					{
						player.ApplyDamageToNPC(i, (int)num, num2, num3, crit);
					}
					player.velocity.X = (float)(-(float)num3 * 9);
					player.velocity.Y = -4f;
					player.immune = true;
					player.immuneNoBlink = true;
					player.immuneTime = 4;
                    AntlionDashLeftStage = false;
                    AntlionDashRightStage = false;
                }
            }
            if(AntlionDashCool && AntlionDashTimer < 90) AntlionDashTimer++;
            if(AntlionDashTimer == 20) 
            {
                AntlionDashRightStage = false;
                AntlionDashLeftStage = false;
            }
            if(AntlionDashRightStage && AntlionDashTimer > 10 && player.velocity.X > 2) player.velocity.X -= 2;
            if(AntlionDashLeftStage && AntlionDashTimer > 10 && player.velocity.X < -2) player.velocity.X += 2;
            if(Keyboard.GetState().IsKeyDown(Keys.A) && AntlionDash && AntlionDashTimer == 90 && !A) 
            {
                A = true;
                TimeSpan ts1 = new TimeSpan(AdateTime.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                int ts = ts1.Subtract(ts2).Duration().Milliseconds;
                if(ts <= 200) 
                {
                    AntlionDashCool = true;
                    AntlionDashLeftStage = true;
                    AntlionDashTimer = 0;
                    player.velocity = new Vector2(-20, 0);
                }
            }
            if(A && Keyboard.GetState().IsKeyUp(Keys.A) && !pressedD && !pressedA)
            {
                AdateTime = DateTime.Now;
                A = false;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D) && AntlionDash && AntlionDashTimer == 90 && !D) 
            {
                D = true;
                TimeSpan ts1 = new TimeSpan(DdateTime.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                int ts = ts1.Subtract(ts2).Duration().Milliseconds;
                if(ts <= 200) 
                {
                    AntlionDashCool = true;
                    AntlionDashRightStage = true;
                    AntlionDashTimer = 0;
                    player.velocity = new Vector2(20, 0);
                }
            }
            if(D && Keyboard.GetState().IsKeyUp(Keys.D) && !pressedD && !pressedA)
            {
                DdateTime = DateTime.Now;
                D = false;
            }
            pressedA = false;
            pressedD = false;
            foreach(var i in pressedKeys)
            {
                if(String.Concat(i) == Main.cLeft) pressedA = true;
                if(String.Concat(i) == Main.cRight) pressedD = true;
            }
        }
        public static bool SandCracker = false;
        public static bool FrozenProtector = false;//霜寒守卫者
        public bool AntlionDash = false;//荒漠突袭者
        public bool AntlionDashRightStage = false;
        public bool AntlionDashLeftStage = false;
        public int AntlionDashTimer = 90;
        public bool AntlionDashCool = false;
        public bool A = false, Aclick = false;
        public bool D = false, Dclick = false;
    }
}