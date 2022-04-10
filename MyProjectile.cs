using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine
{
    public class MyProjectile : GlobalProjectile
    {
        /// <summary>
        /// 寻敌跟踪(弹幕，忽略物块，最大寻敌距离，跟踪速度，偏移量)
        /// </summary>
        public static void ProjectileTrack(Projectile projectile, bool IgnoreBlock, float DistanceMax, float Velocity, float Offset)
        {
            Vector2 center = projectile.Center;
            bool flag = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    float num = (float)(Main.npc[i].width / 2) + (float)(Main.npc[i].height / 2);
                    bool flag2 = true;
                    if (num < DistanceMax && !IgnoreBlock)
                    {
                        flag2 = Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1);
                    }
                    if (Vector2.Distance(Main.npc[i].Center, projectile.Center) < DistanceMax + num && flag2)
                    {
                        center = Main.npc[i].Center;
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
            {
                Vector2 vector = projectile.DirectionTo(center);
                if (Utils.HasNaNs(vector))
                {
                    vector = Vector2.UnitY;
                }
                projectile.velocity = (projectile.velocity * Offset + vector * Velocity) / (Offset + 1f);
            }
        }
        /// <summary>
        /// 弹幕拖尾(弹幕，拖尾贴图，拖尾颜色)
        /// </summary>
        public static void ProjectileDrawTail(Projectile projectile,Texture2D Tail,Color TailColor)
        {
            Vector2 drawOrigin;
            drawOrigin = new Vector2(Tail.Width * 0.5f, Tail.Height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = TailColor * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = projectile.scale * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float rotation;
                float length;
                if (k + 1 >= projectile.oldPos.Length)
                {
                    length = (projectile.position - projectile.oldPos[k]).Length();
                    rotation = (projectile.position - projectile.oldPos[k]).ToRotation() + MathHelper.PiOver2;
                }
                else
                {
                    length = (projectile.oldPos[k + 1] - projectile.oldPos[k]).Length();
                    rotation = (projectile.oldPos[k + 1] - projectile.oldPos[k]).ToRotation() + MathHelper.PiOver2;
                }
                Main.spriteBatch.Draw(Tail, projectile.Center - projectile.position + projectile.oldPos[k] - Main.screenPosition, new Rectangle(0, 0, Tail.Width, Tail.Height), color * projectile.Opacity, rotation, drawOrigin, new Vector2(scale, 1), SpriteEffects.None, 0f);
            }
        }
    }
}