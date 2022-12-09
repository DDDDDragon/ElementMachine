using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ElementMachine.Effect;
using ElementMachine.Effect.CameraModifiers;
using ElementMachine.Buffs;
using ElementMachine.NPCs.BossItems.SandDiablos;
using ElementMachine.Oblation;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using ElementMachine.NPCs.ElementCreatures;
using Terraria.Audio;
using ElementMachine.NPCs.SandDiablosMonsters;

namespace ElementMachine.NPCs.Boss.SandDiablos
{
    public class SandDiablosShoulder : ElementCreaturesBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 4;
            base.SetStaticDefaults();
        }
        public int RorL
        {
            get => NPC.spriteDirection;
            set => NPC.spriteDirection = value;
        }
        public int Head
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public bool Attack
        {
            get => (Main.npc[Head].ModNPC as SandDiablos).Attack;
        }
        public override void SetDefaults()
        {
            NPC.height = 28;
            NPC.width = 32;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 6;
			NPC.lifeMax = 300;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            Element = 3;
            ElementLevel = 2;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC.frameCounter++;
            if(NPC.frameCounter == 30)
            {
                if (NPC.frame.Y == 90) NPC.frame.Y = 0;
                else NPC.frame.Y += 30;
                NPC.frameCounter = 0;
            }
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Vector2 nextCenter = new Vector2(HeadN.Center.X + RorL * (44 + (float)Math.Sin(HeadN.ai[1] / 30) * 3), HeadN.Center.Y + 20 - (float)Math.Sin(HeadN.ai[1] / 30) * 2);
            NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 35;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            if (projectile.penetrate == 1) projectile.Kill();
            else damage /= 3;
        }
    }
    public class SandDiablosClaw : ElementCreaturesBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 3;
            base.SetStaticDefaults();
        }
        public int RorL
        {
            get => NPC.spriteDirection;
            set => NPC.spriteDirection = value;
        }
        public int Head
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public override void SetDefaults()
        {
            NPC.height = 44;
            NPC.width = 34;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 12;
			NPC.lifeMax = 600;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            Element = 3;
            ElementLevel = 2;
            base.SetDefaults();
        }
        public bool Attack
        {
            get => Main.npc[Head].ModNPC == null ? this.thisAttack : (Main.npc[Head].ModNPC as SandDiablos).Attack || this.thisAttack;
        }
        public bool thisAttack = false;
        public Vector2 nextCenter = new Vector2();
        public bool broke = false;
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            if(DmgCnt >= NPC.lifeMax / 2)
            {
                if (!broke)
                {
                    SoundEngine.PlaySound(SoundID.Item127);
                    Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "砂角魔灵 部位破坏: 爪" : "SandDiablos Part Destroyed: Claw", Color.Gold);
                    if (!thisAttack) NPC.frame.Y = 44;
                    broke = true;
                }
                NPC.defense = 8;
                
            }
            if(!Attack)
            {
                nextCenter = new Vector2(HeadN.Center.X + RorL * (48 + (float)Math.Sin(HeadN.ai[1] / 30) * 3), HeadN.Center.Y + 60 - (float)Math.Sin(HeadN.ai[1] / 30) * 2);
                NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 50;
            }
            else
            {
                if(nextCenter != Vector2.Zero)
                {
                    NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 50;
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            if (projectile.penetrate == 1) projectile.Kill();
            else damage /= 3;
        }
    }
    public class SandDiablosBody_Bone : ModGore
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    public class SandDiablosHorn_Gore1 : ModGore
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    public class SandDiablosHorn_Gore2 : ModGore
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    public class SandDiablosBody : ElementCreaturesBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 5;
            base.SetStaticDefaults();
        }
        public int Head
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public bool Attack
        {
            get => (Main.npc[Head].ModNPC as SandDiablos).Attack;
        }
        public Texture2D tex = ModContent.Request<Texture2D>("ElementMachine/NPCs/Boss/SandDiablos/SandDiablosBody_Bone").Value;
        public override void SetDefaults()
        {
            NPC.height = 34;
            NPC.width = 46;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 15;
			NPC.lifeMax = 1000;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            Element = 3;
            ElementLevel = 2;
            base.SetDefaults();
        }
        public bool broke = false;
        public override void AI()
        {
            NPC.frameCounter++;
            if (NPC.frameCounter == 15)
            {
                if (NPC.frame.Y == 136) NPC.frame.Y = 0;
                else NPC.frame.Y += 34;
                NPC.frameCounter = 0;
            }
            if(DmgCnt >= NPC.lifeMax / 2)
            {
                NPC.defense = 8;
                if(!broke)
                {
                    SoundEngine.PlaySound(SoundID.Item127);
                    Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "砂角魔灵 部位破坏: 胸壳" : "SandDiablos Part Destroyed: Chest", Color.Gold);
                    if (Main.rand.NextBool()) Item.NewItem(null, NPC.Center, ModContent.ItemType<SandDiablosBody_bone>());
                    else Gore.NewGore(null, NPC.Center, Vector2.Zero, ModContent.GoreType<SandDiablosBody_Bone>());
                    broke = true;
                }
            }
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Vector2 nextCenter = new Vector2(HeadN.Center.X, HeadN.Center.Y + 34);
            NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 30;
            NPC.rotation = NPC.velocity.X * 0.1f;
            if((double)NPC.rotation < -0.2)
            {
                NPC.rotation = -0.2f;
            }
            if((double)NPC.rotation > 0.2)
            {
                NPC.rotation = 0.2f;
            }
        }
        public Vector2 vec = new Vector2(0, -4);
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            if (projectile.penetrate == 1) projectile.Kill();
            else damage /= 3;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            double x = Main.rand.NextFloat(0, (float)Math.PI * 2);
            vec = new Vector2(0, -4) + new Vector2((float)Math.Sin(x), (float)Math.Cos(x));
            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            base.PostDraw(spriteBatch, screenPos, drawColor);
            if(DmgCnt < NPC.lifeMax / 2) spriteBatch.Draw(tex, NPC.Center + vec - screenPos, new Rectangle(0, 0, tex.Width, tex.Height), drawColor, NPC.rotation, tex.Size() / 2, 1f, SpriteEffects.None, 0f);
            
        }
    }
    public class SandDiablosTail : ElementCreaturesBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 2;
            base.SetStaticDefaults();
        }
        public int Head
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public bool thisAttack = false;
        public bool Attack
        {
            get => this.thisAttack;
        }
        public override void SetDefaults()
        {
            NPC.height = 40;
            NPC.width = 34;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 12;
			NPC.lifeMax = 600;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            Element = 3;
            ElementLevel = 2;
            base.SetDefaults();
        }
        public bool broke = false;
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            if (DmgCnt >= NPC.lifeMax / 2)
            {
                NPC.defense = 8;
                if(!broke)
                {
                    SoundEngine.PlaySound(SoundID.Item127);
                    Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "砂角魔灵 部位破坏: 尾" : "SandDiablos Part Destroyed: Tail", Color.Gold);
                    NPC.frame.Y = 40;
                    broke = true;
                }
                
            }
            Vector2 nextCenter = new Vector2(HeadN.Center.X, HeadN.Center.Y + 70 + (float)Math.Sin(HeadN.ai[1] / 30) * 2);
            if(!Attack) 
            {
                NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 55;
                NPC.rotation = NPC.velocity.X * 0.1f;
                if((double)NPC.rotation < -0.2)
                {
                    NPC.rotation = -0.2f;
                }
                if((double)NPC.rotation > 0.2)
                {
                    NPC.rotation = 0.2f;
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            if (projectile.penetrate == 1) projectile.Kill();
            else damage /= 3;
        }
    }
    public class SandDiablos : ElementCreaturesBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 6;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            NPC.height = 40;
            NPC.width = 74;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 6;
			NPC.lifeMax = 800;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            Element = 3;
            ElementLevel = 2;
            base.SetDefaults();
        }
        public override void OnKill()
        {
            MyWorld.SandDiablos = true;
            Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "少量的荒砂已经附着到沙漠生灵的身上" : "Some Diablosand have cling to the creatures in the desert", new Color(182, 146, 86));
            Dust.NewDust(NPC.Center, 1, 1, MyDustId.YellowGoldenFire, NPC.velocity.X / 10, NPC.velocity.Y / 10);
            Dust.NewDust(NPC.Center, 1, 1, MyDustId.YellowFx1, NPC.velocity.X / 10, NPC.velocity.Y / 10);
            base.OnKill();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandDiablosCarapace>(), 1, 7, 18));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SandDiablosCore>()));
            base.ModifyNPCLoot(npcLoot);
        }
        public float QuadraticA = 0;
        public float QuadraticK = 0;
        public float QuadraticH = 0;
        public float QuadraticDis = 0;
        public Vector2 QuadraticBegin = new Vector2();
        public Vector2 getNextPos(float timer)
        {
            float X = QuadraticBegin.X + QuadraticDis / 20 * timer;
            return new Vector2(X, QuadraticA * (X - QuadraticK) + QuadraticH);
        }
        int body = 0;
        public int rightClaw = 0;
        public int leftClaw = 0;
        int rightShoulder = 0;
        int leftShoulder = 0;
        int tail = 0;
        public bool Attack = false;
        int AttackType = 0;
        int attackClaw = 0;
        public bool tailActive = true;
        int tailType = 0;
        int DmgAllCnt;
        /// <summary>
        /// NPC平滑追踪的点
        /// </summary>
        Vector2 chasePos;
        public bool broke = false;
        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if(DmgAllCnt >= 4200)
            {
                NPC.life = Main.npc[leftClaw].life = Main.npc[rightClaw].life = Main.npc[leftShoulder].life =
                Main.npc[rightShoulder].life = Main.npc[body].life = Main.npc[tail].life = 0;
                Main.npc[leftClaw].checkDead();
                Main.npc[rightClaw].checkDead();
                Main.npc[leftShoulder].checkDead();
                Main.npc[rightShoulder].checkDead();
                Main.npc[body].checkDead();
                Main.npc[tail].checkDead();
                NPC.checkDead();
            }
            if (DmgCnt >= NPC.lifeMax / 2)
            {
                NPC.defense = 8;
                if (!broke)
                {
                    SoundEngine.PlaySound(SoundID.Item127);
                    Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "砂角魔灵 部位破坏: 头" : "SandDiablos Part Destroyed: Head", Color.Gold);
                    if (Main.rand.NextBool())
                    {
                        Item.NewItem(null, NPC.Center, ModContent.ItemType<SandDiablosHorn>());
                    }
                    else
                    {
                        Gore.NewGore(null, NPC.Center, Vector2.Zero, ModContent.GoreType<SandDiablosHorn_Gore1>());
                        Gore.NewGore(null, NPC.Center, Vector2.Zero, ModContent.GoreType<SandDiablosHorn_Gore2>());
                    }
                    if (AttackType != 3) NPC.frame.Y = 120;
                    broke = true;
                }
            }
            if (NPC.localAI[0] == 0f) 
            {
                NPC.immortal = true;
                Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? 
                    "砂角魔灵 ≮特质≯: 【不可穿透】 所有击中它的穿透弹幕伤害减至33% 不可穿透的弹幕会被摧毁" : 
                    "SandDiablos ≮Idiosyncrasy≯: 【Unpenetratable】 Any penetratable projectile hit it will decrease damage to 33%, unpenetratable projectile will be crashed", Color.Gold);

                leftClaw = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X - 48, (int)NPC.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[leftClaw].position = NPC.position + new Vector2(-48, 60);
                Main.npc[leftClaw].immortal = true;
                (Main.npc[leftClaw].ModNPC as SandDiablosClaw).Head = NPC.whoAmI;
                (Main.npc[leftClaw].ModNPC as SandDiablosClaw).RorL = -1;

                rightClaw = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + 48, (int)NPC.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[rightClaw].position = NPC.position + new Vector2(48, 60);
                Main.npc[rightClaw].immortal = true;
                (Main.npc[rightClaw].ModNPC as SandDiablosClaw).Head = NPC.whoAmI;
                (Main.npc[rightClaw].ModNPC as SandDiablosClaw).RorL = 1;

                leftShoulder = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X - 44, (int)NPC.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[leftShoulder].position = NPC.position + new Vector2(-44, 40);
                Main.npc[leftShoulder].immortal = true;
                (Main.npc[leftShoulder].ModNPC as SandDiablosShoulder).Head = NPC.whoAmI;
                (Main.npc[leftShoulder].ModNPC as SandDiablosShoulder).RorL = -1;

                rightShoulder = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + 44, (int)NPC.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[rightShoulder].position = NPC.position + new Vector2(44, 40);
                Main.npc[rightShoulder].immortal = true;
                (Main.npc[rightShoulder].ModNPC as SandDiablosShoulder).Head = NPC.whoAmI;
                (Main.npc[rightShoulder].ModNPC as SandDiablosShoulder).RorL = 1;

                body = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y + 30, ModContent.NPCType<SandDiablosBody>());
                Main.npc[body].position = NPC.position + new Vector2(0, 30);
                Main.npc[body].immortal = true;
                (Main.npc[body].ModNPC as SandDiablosBody).Head = NPC.whoAmI;

                tail = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y + 70, ModContent.NPCType<SandDiablosTail>());
                Main.npc[tail].position = NPC.position + new Vector2(0, 70);
                Main.npc[tail].immortal = true;
                (Main.npc[tail].ModNPC as SandDiablosTail).Head = NPC.whoAmI;
				NPC.netUpdate = true;
                NPC.Center = player.Center + new Vector2(0, 1) * -250;
				NPC.localAI[0] = 1f;

                chasePos = player.Center + new Vector2(0, -250);

            }
            DmgAllCnt = DmgCnt + (Main.npc[leftClaw].ModNPC as SandDiablosClaw).DmgCnt + (Main.npc[rightClaw].ModNPC as SandDiablosClaw).DmgCnt
                + (Main.npc[leftShoulder].ModNPC as SandDiablosShoulder).DmgCnt + (Main.npc[rightShoulder].ModNPC as SandDiablosShoulder).DmgCnt
                + (Main.npc[body].ModNPC as SandDiablosBody).DmgCnt + (Main.npc[tail].ModNPC as SandDiablosTail).DmgCnt;
            if (AttackType != -1 && !Attack)
            {
                chasePos = player.Center + new Vector2(0, -250);
                if (NPC.Distance(chasePos) > 20)
                {
                    NPC.velocity = Vector2.Normalize(chasePos - NPC.Center) * 2.1f * NPC.Distance(chasePos) / 70;
                }
                else
                {

                }
            }
            else if (Attack) NPC.velocity = Vector2.Zero;
            else NPC.velocity = NPC.velocity * 8 / 10;
            /*
            if (AttackType == -1)
            {
                if (NPC.ai[1] <= 75 || NPC.ai[1] >= 225)
                {
                    NPC.velocity = Vector2.Normalize(player.Center + new Vector2(0, -250) - NPC.Center) * 2.1f * NPC.Distance(player.Center + new Vector2(0, -250)) / 70;
                }
                else
                {
                    if (NPC.ai[1] % 100 == 0)
                    {
                        NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SandAntlionCharger>());
                        npc.lifeMax = npc.life = npc.lifeMax / 4; 
                    }
                }
            }*/
            if(NPC.ai[1] <= 600 && AttackType == 0)
            {
                if(NPC.ai[1] % 60 <= 7)
                {
                    if(NPC.ai[1] % 60 == 0)
                    {
                        if(Main.rand.Next(0, 2) == 0)
                        {
                            (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                            (Main.npc[leftClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                            Main.npc[leftClaw].velocity = new Vector2(3, 3) * 10 / 7;
                            attackClaw = leftClaw;
                        }
                        else
                        {
                            (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                            (Main.npc[rightClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                            Main.npc[rightClaw].velocity = new Vector2(-3, 3) * 10 / 7;
                            attackClaw = rightClaw;
                        }
                        Vector2 velo = Vector2.Normalize(player.Center - Main.npc[attackClaw].Center) * 6.5f;
                        int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.npc[attackClaw].Center, velo, ModContent.ProjectileType<SandBonePiece>(), 10, 0f, 255);
                        Main.projectile[proj].ai[0] = (Main.npc[attackClaw].ModNPC as SandDiablosClaw).RorL;
                    }
                    Main.npc[attackClaw].velocity = new Vector2(3 * (Main.npc[attackClaw].ModNPC as SandDiablosClaw).RorL * -1, 3) * (10 - NPC.ai[1] % 60) / 7;
                    if(NPC.ai[1] % 60 == 7) (Main.npc[attackClaw].ModNPC as SandDiablosClaw).thisAttack = false;       
                }
            }
            if(AttackType == 1)
            {
                if(NPC.ai[1] == 0)
                {
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    Main.npc[leftClaw].velocity = new Vector2(2, -2) * 10 / 7;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    Main.npc[rightClaw].velocity = new Vector2(-2, -2) * 10 / 7;
                    Attack = true;
                    Main.npc[body].velocity = Vector2.Zero;
                    Main.npc[leftShoulder].velocity = Vector2.Zero;
                    Main.npc[rightShoulder].velocity = Vector2.Zero;
                    Main.npc[tail].velocity = Vector2.Zero;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                }
                if(NPC.ai[1] <= 5)
                {
                    Main.npc[leftClaw].rotation -= 0.2f;
                    Main.npc[rightClaw].rotation += 0.2f;
                }
                if(NPC.ai[1] == 5)
                {
                    Main.npc[leftClaw].velocity = Vector2.Zero;
                    Main.npc[rightClaw].velocity = Vector2.Zero;
                }
                if(NPC.ai[1] <= 45 && NPC.ai[1] >= 30)
                {
                    Main.npc[leftClaw].velocity = new Vector2(-3, 3) * (45 - NPC.ai[1]) / 7;
                    Main.npc[rightClaw].velocity = new Vector2(3, 3) * (45 - NPC.ai[1]) / 7;
                    Main.npc[leftClaw].rotation += 0.1f;
                    Main.npc[rightClaw].rotation -= 0.1f;
                }
                if(NPC.ai[1] == 50)
                {
                    Main.npc[leftClaw].rotation = 0f;
                    Main.npc[rightClaw].rotation = 0f;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(-300, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(300, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Attack = false;
                }
                if(NPC.ai[1] >= 80 && NPC.ai[1] <= 110 && (NPC.ai[1] - 50) % 30 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(-300 + (NPC.ai[1] - 50) * 2, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(300 - (NPC.ai[1] - 50) * 2, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Attack = false;
                }
            }
            if(AttackType == 2)
            {
                if(NPC.ai[1] % 120 == 0)
                {
                    if(Main.rand.Next(0, 2) == 1)
                    {
                        (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                        (Main.npc[leftClaw].ModNPC as SandDiablosClaw).nextCenter = NPC.Center + new Vector2(-200, 0);
                        attackClaw = leftClaw;
                    }
                    else
                    {
                        (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                        (Main.npc[rightClaw].ModNPC as SandDiablosClaw).nextCenter = NPC.Center + new Vector2(200, 0);
                        attackClaw = rightClaw;
                    }
                }
                if(NPC.ai[1] % 120 <= 20)
                {
                    Main.npc[attackClaw].rotation += -0.05f * (Main.npc[attackClaw].ModNPC as SandDiablosClaw).RorL;
                }
                if(NPC.ai[1] % 120 == 20) 
                {
                    Main.npc[attackClaw].velocity = new Vector2(2 * (Main.npc[attackClaw].ModNPC as SandDiablosClaw).RorL, -2) * 10 / 7;
                    (Main.npc[attackClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                }
                if (NPC.ai[1] % 120 == 25)
                {
                    Main.npc[attackClaw].noGravity = true;
                    Main.npc[attackClaw].velocity = Vector2.Normalize(player.Center - Main.npc[attackClaw].Center) * 20f;
                }
                if(NPC.ai[1] % 120 <= 85 && NPC.ai[1] % 120 >= 45) 
                {
                    Main.npc[attackClaw].velocity *= 0.98f;
                    if(Main.npc[attackClaw].rotation != 0) Main.npc[attackClaw].rotation += 0.03f * (Main.npc[attackClaw].ModNPC as SandDiablosClaw).RorL;
                }
                if(NPC.ai[1] % 120 == 90) Main.npc[attackClaw].velocity *= 0;
                if(NPC.ai[1] % 120 == 95) 
                {
                    (Main.npc[attackClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    Main.npc[attackClaw].rotation = 0;
                }
            }
            if(AttackType == 3)
            {
                if(NPC.ai[1] < 30)
                {
                    if(!broke) NPC.frame.Y = (int)(NPC.ai[1] / 10) * 40;
                    else NPC.frame.Y = (int)(NPC.ai[1] / 10) * 40 + 120;
                }
                if (NPC.ai[1] == 15) SoundEngine.PlaySound(SoundID.Roar);
                if(NPC.ai[1] == 50)
                {
                    Attack = true;
                    Main.npc[leftClaw].frame.Y += ((Main.npc[leftClaw].ModNPC as SandDiablosClaw).DmgCnt >= Main.npc[leftClaw].lifeMax / 2) ? 44 : 88;
                    Main.npc[rightClaw].frame.Y += ((Main.npc[rightClaw].ModNPC as SandDiablosClaw).DmgCnt >+ Main.npc[rightClaw].lifeMax / 2) ? 44 : 88;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                }
                if (NPC.ai[1] >= 30 && NPC.ai[1] <= 40)
                {
                    if (!broke) NPC.frame.Y = (4 - (int)(NPC.ai[1] / 10)) * 40;
                    else NPC.frame.Y = (4 - (int)(NPC.ai[1] / 10)) * 40 + 120;
                }
                if (NPC.ai[1] <= 60)
                {
                    Main.npc[leftClaw].velocity = new Vector2(0, -2) * (10 - NPC.ai[1] + 50) / 5;
                    Main.npc[rightClaw].velocity = new Vector2(0, -2) * (10 - NPC.ai[1] + 50) / 5;
                }
                if(NPC.ai[1] == 60)
                {
                    Main.npc[leftClaw].noGravity = false;
                    Main.npc[rightClaw].noGravity = false;
                    Main.npc[leftClaw].noTileCollide = false;
                    Main.npc[rightClaw].noTileCollide = false;
                }
                if(NPC.ai[1] >= 100)
                {
                    if(Main.npc[leftClaw].collideY || Main.npc[rightClaw].collideY )
                    {
                        if (NPC.ai[1] == 100)
                        {
                            foreach (var pl in Main.player)
                            {
                                for (int i = 0; i < (int)(pl.width / 16f); i++)
                                {
                                    int bx = (int)pl.BottomLeft.X / 16 + i;
                                    int by = (int)pl.BottomLeft.Y / 16;
                                    Tile tileBottom = Main.tile[bx, by];
                                    if (tileBottom.HasUnactuatedTile)
                                    {
                                        pl.AddBuff(ModContent.BuffType<Dizziness>(), 60);
                                    }
                                }
                            }
                        }
                        if (NPC.ai[1] >= 100)
                        {
                            foreach(var pl in Main.player)
                            {
                                if(pl.active && pl.HasBuff<Dizziness>())
                                {
                                    pl.fullRotationOrigin = player.Size / 2;
                                    if (pl.fullRotation < Math.PI / 2) pl.fullRotation += (float)Math.PI / 8; 
                                }
                            }
                            
                        }
                        if (NPC.ai[1] <= 125)
                        {
                            PunchCameraModifier PCM = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2(), 20f, 6f, 20, 1000f, "Rock");
                            EffectPlayer.CMS.Add(PCM);
                        }
                    }
                }
                if(NPC.ai[1] == 140)
                {
                    Attack = false;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    Main.npc[leftClaw].frame.Y -= (NPC.life < NPC.lifeMax / 2) ? 44 : 88;
                    Main.npc[rightClaw].frame.Y -= (NPC.life < NPC.lifeMax / 2) ? 44 : 88;
                    Main.npc[leftClaw].noGravity = true;
                    Main.npc[rightClaw].noGravity = true;
                    Main.npc[leftClaw].noTileCollide = true;
                    Main.npc[rightClaw].noTileCollide = true;
                }
            }
            NPC.ai[1]++;
            if(NPC.ai[1] == 150 && AttackType == 1)
            {
                AttackType = Main.rand.Next<int>(new int[]{ 0, 2, 3 });
                NPC.ai[1] = 0;
            }
            if(NPC.ai[1] == 150 && AttackType == 3)
            {
                AttackType = Main.rand.Next<int>(new int[] { 0, 1, 2 });
                NPC.ai[1] = 0;
            }
            if(NPC.ai[1] == 600) 
            {
                switch(AttackType)
                {
                    case 0:
                        AttackType = Main.rand.Next<int>(new int[] { 1, 2, 3 });
                        break;
                    case 2:
                        AttackType = Main.rand.Next<int>(new int[] { 0, 1, 3 });
                        break;
                }
                NPC.ai[1] = 0;
            }
            if(tailActive)
            {
                NPC.ai[2]++;
                if(NPC.ai[2] == 60)
                {
                    if(tailType == 0)
                    {
                        (Main.npc[tail].ModNPC as SandDiablosTail).thisAttack = true;
                    }
                }
                if(NPC.ai[2] <= 420 && tailType == 0 && NPC.ai[2] > 60)
                {
                    if((NPC.ai[2] - 60) % 120 == 50)
                    {
                        Main.npc[tail].velocity = Vector2.Normalize(player.Center - Main.npc[tail].Center) * 25f;
                    }
                    if((NPC.ai[2] - 60) % 120 >= 51 && NPC.ai[2] % 120 <= 111)
                    {
                        Main.npc[tail].velocity *= 0.93f;
                        Main.npc[tail].rotation = Main.npc[tail].velocity.ToRotation() - (float)Math.PI / 2;
                    }
                    if((NPC.ai[2] - 60) % 120 >= 111)
                    {
                        Main.npc[tail].velocity = Vector2.Zero;
                        Main.npc[tail].rotation = (player.Center - Main.npc[tail].Center).ToRotation() - (float)Math.PI / 2;
                    }
                }
                if(NPC.ai[2] == 420)
                {
                    NPC.ai[2] = 0;
                    switch(tailType)
                    {
                        case 0:
                            tailType = 2;
                            break;
                        case 2:
                            tailType = 0;
                            break;
                        //case 3:
                    }
                    (Main.npc[tail].ModNPC as SandDiablosTail).thisAttack = false;
                }
            
            }
            foreach(var pl in Main.player)
            {
                if (pl.active && !pl.HasBuff<Dizziness>() && pl.fullRotation > 0) pl.fullRotation -= (float)Math.PI / 8;
            }
            base.AI();
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            if (projectile.penetrate == 1) projectile.Kill();
            else damage /= 3;
        }
    }
    public class SandBonePiece : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("SandBonePiece");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂骨碎片");
        }
        public override void SetDefaults()
        {
            Projectile.height = 30;
            Projectile.width = 32;
            Projectile.penetrate = 3;
            Projectile.tileCollide = false;
			Projectile.aiStyle = -1;
			Projectile.hostile = true;
			Projectile.friendly = false;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            base.SetDefaults();
        }
		public override void AI()
		{
            Projectile.rotation = Projectile.velocity.ToRotation() * Projectile.ai[0];
            Projectile.alpha = 80;
            Projectile.spriteDirection = (int)Projectile.ai[0];
		}
        public override bool PreDraw(ref Color lightColor)
        {
            MyProjectile.ProjectileDrawTail(Projectile, ModContent.Request<Texture2D>("ElementMachine/NPCs/Boss/SandDiablos/tail").Value, Color.RosyBrown);
            return base.PreDraw(ref lightColor);
        }
    }
}