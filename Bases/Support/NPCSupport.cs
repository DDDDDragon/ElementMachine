using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Bases.Support
{
    public static class NPCSupport
    {
        public static NPC FindNPCByDistance(Vector2 findcenter, bool findhostile, bool findclosest = true, bool ignoreinvincible = false, float mindistance = 0, float maxdistance = float.MaxValue)
        {
            if (mindistance > maxdistance)
            {
                (maxdistance, mindistance) = (mindistance, maxdistance);
            }
            NPC result = null;
            float dis, Reference;
            if (findclosest)
            {
                Reference = maxdistance;
                foreach (NPC target in Main.npc)
                {
                    try
                    {
                        if (target.active && target.friendly == !findhostile && (ignoreinvincible || !target.dontTakeDamage) && !target.immortal)
                        {
                            dis = Vector2.Distance(target.Center, findcenter);
                            if (mindistance <= dis && dis <= maxdistance && dis < Reference)
                            {
                                Reference = dis;
                                result = target;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                Reference = mindistance;
                foreach (NPC target in Main.npc)
                {
                    try
                    {
                        if (target.active && target.friendly == !findhostile && (ignoreinvincible || !target.dontTakeDamage) && !target.immortal)
                        {
                            dis = Vector2.Distance(target.Center, findcenter);
                            if (mindistance <= dis && dis <= maxdistance && dis > Reference)
                            {
                                Reference = dis;
                                result = target;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        public static NPC FindNPCByLife(bool findhostile, bool findweakest = true, bool ignoreinvincible = false, float minlife = 0, float maxlife = 1)
        {
            if (minlife > maxlife)
            {
                (maxlife, minlife) = (minlife, maxlife);
            }
            NPC result = null;
            float life, Reference;
            if (findweakest)
            {
                Reference = maxlife;
                foreach (NPC target in Main.npc)
                {
                    try
                    {
                        if (target.active && target.friendly == !findhostile && (ignoreinvincible || !target.dontTakeDamage) && !target.immortal)
                        {
                            life = (float)target.life / target.lifeMax;
                            if (minlife <= life && life <= maxlife && life <= Reference)
                            {
                                Reference = life;
                                result = target;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                Reference = maxlife;
                foreach (NPC target in Main.npc)
                {
                    try
                    {
                        if (target.active && target.friendly == !findhostile && (ignoreinvincible || !target.dontTakeDamage) && !target.immortal)
                        {
                            life = (float)target.life / target.lifeMax;
                            if (minlife <= life && life <= maxlife && life >= Reference)
                            {
                                Reference = life;
                                result = target;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        public static void ApplyDamage(this NPC npc, int damage, Color color, SoundStyle soundStyle = default, int showlimit = 5, bool? quiet = null, Player player = null)
        {
            if (!npc.active || npc.life <= 0 || npc.dontTakeDamage || npc.dontTakeDamageFromHostiles)
            {
                return;
            }
            if (quiet.HasValue)
            {
                if (!quiet.Value)
                {
                    CombatText.NewText(npc.Hitbox, color, damage);
                }
            }
            else if (damage >= showlimit)
            {
                CombatText.NewText(npc.Hitbox, color, damage);
            }
            if (!npc.immortal)
            {
                if (npc.realLife >= 0)
                {
                    Main.npc[npc.realLife].life -= damage;
                    npc.life = Main.npc[npc.realLife].life;
                    npc.lifeMax = Main.npc[npc.realLife].lifeMax;
                }
                else
                {
                    npc.life -= damage;
                }
            }
            npc.HitEffect(0, damage);
            if (soundStyle != default && SoundEngine.FindActiveSound(soundStyle) is not null)
            {
                SoundEngine.PlaySound(soundStyle, npc.position);
            }
            if (npc.realLife >= 0)
            {
                Main.npc[npc.realLife].checkDead();
            }
            else
            {
                npc.checkDead();
            }
            if (player is not null)
            {
                player.addDPS(damage);
            }
        }
        public static void ApplyDamage(this NPC npc, int damage, int showlimit = 5, bool? quiet = null, Player player = null, SoundStyle soundStyle = default)
        {
            ApplyDamage(npc, damage, CombatText.DamagedHostile, soundStyle, showlimit, quiet, player);
        }
        public static void Cure(this NPC npc, int cure, Color color, int showlimit = 5, bool? quiet = null, SoundStyle soundStyle = default)
        {
            if (!npc.active)
            {
                return;
            }
            if (!npc.immortal)
            {
                if (npc.realLife >= 0)
                {
                    cure = (Main.npc[npc.realLife].life + cure) > Main.npc[npc.realLife].lifeMax ? (Main.npc[npc.realLife].lifeMax - Main.npc[npc.realLife].life) : cure;
                    Main.npc[npc.realLife].life += cure;
                    npc.life = Main.npc[npc.realLife].life;
                    npc.lifeMax = Main.npc[npc.realLife].lifeMax;
                }
                else
                {
                    cure = (npc.life + cure) > npc.lifeMax ? (npc.lifeMax - npc.life) : cure;
                    npc.life += cure;
                }
            }
            if (SoundEngine.FindActiveSound(soundStyle) is not null)
            {
                SoundEngine.PlaySound(soundStyle, npc.position);
            }
            if (quiet.HasValue)
            {
                if (!quiet.Value)
                {
                    CombatText.NewText(npc.Hitbox, color, cure);
                }
            }
            else if (cure >= showlimit)
            {
                CombatText.NewText(npc.Hitbox, color, cure);
            }
        }
        public static void DropMoney(this NPC npc, Rectangle dropfrom = default, Player closestPlayer = null, Func<NPC, long, long> modifydropmoney = null)
        {
            long num = 0;
            float luck = closestPlayer is null ? 0 : closestPlayer.luck;
            int num2 = 1;
            if (Main.rand.NextFloat() < Math.Abs(luck))
            {
                num2 = 2;
            }
            for (int i = 0; i < num2; i++)
            {
                float num3 = npc.value;
                if (npc.midas)
                {
                    num3 *= 1f + Main.rand.Next(10, 51) * 0.01f;
                }
                num3 *= 1f + Main.rand.Next(-20, 76) * 0.01f;
                if (Main.rand.NextBool(2))
                {
                    num3 *= 1f + Main.rand.Next(5, 11) * 0.01f;
                }
                if (Main.rand.NextBool(4))
                {
                    num3 *= 1f + Main.rand.Next(10, 21) * 0.01f;
                }
                if (Main.rand.NextBool(8))
                {
                    num3 *= 1f + Main.rand.Next(15, 31) * 0.01f;
                }
                if (Main.rand.NextBool(16))
                {
                    num3 *= 1f + Main.rand.Next(20, 41) * 0.01f;
                }
                if (Main.rand.NextBool(32))
                {
                    num3 *= 1f + Main.rand.Next(25, 51) * 0.01f;
                }
                if (Main.rand.NextBool(64))
                {
                    num3 *= 1f + Main.rand.Next(50, 101) * 0.01f;
                }
                if (Main.bloodMoon)
                {
                    num3 *= 1f + Main.rand.Next(101) * 0.01f;
                }
                if (i == 0)
                {
                    num = (long)num3;
                }
                else if (luck < 0f)
                {
                    if (num3 < num)
                    {
                        num = (long)num3;
                    }
                }
                else if (num3 > num)
                {
                    num = (long)num3;
                }
            }
            num += npc.extraValue;
            if (modifydropmoney is not null)
            {
                num = modifydropmoney.Invoke(npc, num);
            }
            if (dropfrom == default)
            {
                dropfrom = npc.Hitbox;
            }
            while ((int)num > 0)
            {
                if (num > 1000000f)
                {
                    int num4 = (int)(num / 1000000f);
                    if (num4 > 50 && Main.rand.NextBool(5))
                    {
                        num4 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        num4 /= Main.rand.Next(3) + 1;
                    }
                    int j = num4;
                    while (j > 999)
                    {
                        j -= 999;
                        Item.NewItem(npc.GetSource_DropAsItem(), dropfrom, ItemID.PlatinumCoin, 999);
                    }
                    num -= 1000000 * num4;
                    Item.NewItem(npc.GetSource_DropAsItem(), dropfrom, ItemID.PlatinumCoin, j);
                }
                else if (num > 10000f)
                {
                    int num5 = (int)(num / 10000f);
                    if (num5 > 50 && Main.rand.NextBool(5))
                    {
                        num5 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        num5 /= Main.rand.Next(3) + 1;
                    }
                    num -= 10000 * num5;
                    Item.NewItem(npc.GetSource_DropAsItem(), dropfrom, ItemID.GoldCoin, num5);
                }
                else if (num > 100f)
                {
                    int num6 = (int)(num / 100f);
                    if (num6 > 50 && Main.rand.NextBool(5))
                    {
                        num6 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        num6 /= Main.rand.Next(3) + 1;
                    }
                    num -= 100 * num6;
                    Item.NewItem(npc.GetSource_DropAsItem(), dropfrom, ItemID.SilverCoin, num6);
                }
                else
                {
                    int num7 = (int)num;
                    if (num7 > 50 && Main.rand.NextBool(5))
                    {
                        num7 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        num7 /= Main.rand.Next(4) + 1;
                    }
                    if (num7 < 1)
                    {
                        num7 = 1;
                    }
                    num -= num7;
                    Item.NewItem(npc.GetSource_DropAsItem(), dropfrom, ItemID.CopperCoin, num7);
                }
            }
        }
        public static List<NPC> FindNPC(Predicate<NPC> predicate)
        {
            List<NPC> list = new();
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && predicate(npc))
                {
                    list.Add(npc);
                }
            }
            return list;
        }
        public static NPC FindNPCFirstOrNull(Predicate<NPC> predicate)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && predicate(npc))
                {
                    return npc;
                }
            }
            return null;
        }
        public static void SetBuffImmune(this NPC npc, IEnumerable<int> immunes)
        {
            if (!immunes.Any())
            {
                return;
            }
            List<int> list = immunes.ToList();
            list.Sort();
            if (list[0] < 0)
            {
                return;
            }
            foreach (int i in list)
            {
                if (i >= npc.buffImmune.Length)
                {
                    break;
                }
                npc.buffImmune[i] = true;
            }
        }
        public static double StrikeNPC(this NPC npc, int Damage, float knockBack, int hitDirection, Color damagecolor = default, bool crit = false, Color critcolor = default)
        {
            if (!npc.active || npc.life <= 0)
            {
                return 0;
            }
            double dam = Damage;
            int def = npc.defense;
            if (npc.ichor)
            {
                def -= 15;
            }
            if (npc.betsysCurse)
            {
                def -= 40;
            }
            if (def < 0)
            {
                def = 0;
            }
            if (NPCLoader.StrikeNPC(npc, ref dam, def, ref knockBack, hitDirection, ref crit))
            {
                dam = Main.CalculateDamageNPCsTake((int)dam, def);
                if (crit)
                {
                    dam *= 2.0;
                }
                if (npc.takenDamageMultiplier > 1f)
                {
                    dam *= npc.takenDamageMultiplier;
                }
            }
            if ((npc.takenDamageMultiplier > 1f || Damage != 9999) && npc.lifeMax > 1)
            {
                if (npc.friendly)
                {
                    Color color;
                    if (crit)
                    {
                        color =
                            critcolor == default ?
                                (damagecolor == default ?
                                    CombatText.DamagedFriendlyCrit
                                    :
                                    new Color(damagecolor.R, damagecolor.G, damagecolor.B, damagecolor.A / 2))
                                :
                                critcolor;
                    }
                    else
                    {
                        color = damagecolor == default ? CombatText.DamagedFriendly : damagecolor;
                    }
                    CombatText.NewText(npc.getRect(), color, (int)dam, crit, false);
                }
                else
                {
                    Color color;
                    if (crit)
                    {
                        color =
                            critcolor == default ?
                                (damagecolor == default ?
                                    CombatText.DamagedHostileCrit
                                    :
                                    new Color(damagecolor.R, damagecolor.G, damagecolor.B, damagecolor.A / 2))
                                :
                                critcolor;
                    }
                    else
                    {
                        color = damagecolor == default ? CombatText.DamagedHostile : damagecolor;
                    }
                    CombatText.NewText(npc.getRect(), color, (int)dam, crit, false);
                }
            }
            if (dam >= 1)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    npc.PlayerInteraction(Main.myPlayer);
                }
                npc.justHit = true;
                if ((npc.type == NPCID.CultistDevote ||
                    npc.type == NPCID.CultistArcherBlue) &&
                    Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int ai3 = -(int)(npc.ai[3] + 1);
                    if (ai3 > -1 && Main.npc[ai3].localAI[0] == 0f)
                    {
                        Main.npc[ai3].localAI[0] = 1f;
                    }
                }
                if (npc.townNPC)
                {
                    if (npc.aiStyle == 7 &&
                        (npc.ai[0] == 3f ||
                        npc.ai[0] == 4f ||
                        npc.ai[0] == 16f ||
                        npc.ai[0] == 17f))
                    {
                        NPC nPC = Main.npc[(int)npc.ai[2]];
                        if (nPC.active)
                        {
                            nPC.ai[0] = 1f;
                            nPC.ai[1] = Main.rand.Next(300, 600);
                            nPC.ai[2] = 0f;
                            nPC.localAI[3] = 0f;
                            nPC.direction = hitDirection;
                            nPC.netUpdate = true;
                        }
                    }
                    npc.ai[0] = 1f;
                    npc.ai[1] = Main.rand.Next(300, 600);
                    npc.ai[2] = 0f;
                    npc.localAI[3] = 0f;
                    npc.direction = hitDirection;
                    npc.netUpdate = true;
                }
                if (npc.aiStyle == 8 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (npc.type == NPCID.RuneWizard)
                    {
                        npc.ai[0] = 450f;
                    }
                    else if (npc.type is NPCID.Necromancer or NPCID.NecromancerArmored)
                    {
                        if (Main.rand.NextBool())
                        {
                            npc.ai[0] = 390f;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.type == NPCID.DesertDjinn)
                    {
                        if (!Main.rand.NextBool(3))
                        {
                            npc.ai[0] = 181f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[0] = 400f;
                    }
                    npc.TargetClosest(true);
                }
                if (npc.aiStyle == 97 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.localAI[1] = 1f;
                    npc.TargetClosest(true);
                }
                if (npc.type == NPCID.DetonatingBubble)
                {
                    dam = 0.0;
                    npc.ai[0] = 1f;
                    npc.ai[1] = 4f;
                    npc.dontTakeDamage = true;
                }
                if (npc.type == NPCID.SantaNK1 &&
                    npc.life >= npc.lifeMax * 0.5 &&
                    npc.life - dam < npc.lifeMax * 0.5)
                {
                    Gore.NewGore(npc.GetSource_Death(), npc.position, npc.velocity, 517, 1f);
                }
                if (npc.type == NPCID.SpikedIceSlime)
                {
                    npc.localAI[0] = 60f;
                }
                if (npc.type == NPCID.SlimeSpiked)
                {
                    npc.localAI[0] = 60f;
                }
                if (npc.type == NPCID.SnowFlinx)
                {
                    npc.localAI[0] = 1f;
                }
                if (!npc.immortal)
                {
                    if (npc.realLife >= 0)
                    {
                        Main.npc[npc.realLife].life -= (int)dam;
                        npc.life = Main.npc[npc.realLife].life;
                        npc.lifeMax = Main.npc[npc.realLife].lifeMax;
                    }
                    else
                    {
                        npc.life -= (int)dam;
                    }
                }
                if (knockBack > 0f && npc.knockBackResist > 0f)
                {
                    float knock = knockBack * npc.knockBackResist;
                    if (npc.onFire2)
                    {
                        knock *= 1.1f;
                    }
                    if (knock > 8f)
                    {
                        float num5 = knock - 8f;
                        num5 *= 0.9f;
                        knock = 8f + num5;
                    }
                    if (knock > 10f)
                    {
                        float num6 = knock - 10f;
                        num6 *= 0.8f;
                        knock = 10f + num6;
                    }
                    if (knock > 12f)
                    {
                        float num7 = knock - 12f;
                        num7 *= 0.7f;
                        knock = 12f + num7;
                    }
                    if (knock > 14f)
                    {
                        float num8 = knock - 14f;
                        num8 *= 0.6f;
                        knock = 14f + num8;
                    }
                    if (knock > 16f)
                    {
                        knock = 16f;
                    }
                    if (crit)
                    {
                        knock *= 1.4f;
                    }
                    if ((Main.expertMode ? 15 : 10) * dam > npc.lifeMax)
                    {
                        if (hitDirection < 0 && npc.velocity.X > 0f - knock)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X -= knock;
                            }
                            npc.velocity.X -= knock;
                            if (npc.velocity.X < 0f - knock)
                            {
                                npc.velocity.X = 0f - knock;
                            }
                        }
                        else if (hitDirection > 0 && npc.velocity.X < knock)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X += knock;
                            }
                            npc.velocity.X += knock;
                            if (npc.velocity.X > knock)
                            {
                                npc.velocity.X = knock;
                            }
                        }
                        if (npc.type == NPCID.SnowFlinx)
                        {
                            knock *= 1.5f;
                        }
                        knock = (npc.noGravity ? (knock * -0.5f) : (knock * -0.75f));
                        if (npc.velocity.Y > knock)
                        {
                            npc.velocity.Y += knock;
                            if (npc.velocity.Y < knock)
                            {
                                npc.velocity.Y = knock;
                            }
                        }
                    }
                    else
                    {
                        if (!npc.noGravity)
                        {
                            npc.velocity.Y = (0f - knock) * 0.75f * npc.knockBackResist;
                        }
                        else
                        {
                            npc.velocity.Y = (0f - knock) * 0.5f * npc.knockBackResist;
                        }
                        npc.velocity.X = knock * hitDirection * npc.knockBackResist;
                    }
                }
                if ((npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye)
                    && npc.life <= 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active &&
                            (Main.npc[i].type == NPCID.WallofFlesh ||
                            Main.npc[i].type == NPCID.WallofFleshEye))
                        {
                            Main.npc[i].HitEffect(hitDirection, dam);
                        }
                    }
                }
                else
                {
                    npc.HitEffect(hitDirection, dam);
                }
                if (npc.HitSound is not null)
                {
                    SoundEngine.PlaySound(npc.HitSound.Value, npc.position);
                }
                if (npc.realLife >= 0)
                {
                    Main.npc[npc.realLife].checkDead();
                }
                else
                {
                    npc.checkDead();
                }
                return dam;
            }
            return 0;
        }
    }
}