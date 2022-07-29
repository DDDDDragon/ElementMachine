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

namespace ElementMachine.NPCs.Boss.SandDiablos
{
    public class SandDiablosShoulder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
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
			NPC.lifeMax = 400;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            if(RorL == 1) Main.npc[(HeadN.ModNPC as SandDiablos).rightClaw].dontTakeDamage = true;
            else Main.npc[(HeadN.ModNPC as SandDiablos).leftClaw].dontTakeDamage = true;
            Main.npc[Head].dontTakeDamage = true;
            Vector2 nextCenter = new Vector2(HeadN.Center.X + RorL * (44 + (float)Math.Sin(HeadN.ai[1] / 30) * 3), HeadN.Center.Y + 20 - (float)Math.Sin(HeadN.ai[1] / 30) * 2);
            NPC.velocity = Vector2.Normalize(nextCenter - NPC.Center) * 4.5f * Vector2.Distance(nextCenter, NPC.Center) / 35;
        }
    }
    public class SandDiablosClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
            Main.npcFrameCount[NPC.type] = 2;
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
			NPC.defense = 6;
			NPC.lifeMax = 400;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public bool Attack
        {
            get => (Main.npc[Head].ModNPC as SandDiablos).Attack || this.thisAttack;
        }
        public bool thisAttack = false;
        public Vector2 nextCenter = new Vector2();
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Main.npc[Head].dontTakeDamage = true;
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
    }
    public class SandDiablosBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
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
        public Texture2D tex = ModContent.Request<Texture2D>("ElementMachine/NPCs/Boss/SandDiablos/SandDiablosBody_bone").Value;
        public override void SetDefaults()
        {
            NPC.height = 28;
            NPC.width = 50;
            NPC.friendly = false;
            NPC.value = 10000;
			NPC.damage = 20;
			NPC.defense = 6;
			NPC.lifeMax = 600;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Main.npc[Head].dontTakeDamage = true;
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
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            base.PostDraw(spriteBatch, screenPos, drawColor);
            spriteBatch.Draw(tex, NPC.position + new Vector2(16, 10) - screenPos, new Rectangle(0, 0, tex.Width, tex.Height), Color.White, NPC.rotation, tex.Size() / 2, 1f, SpriteEffects.None, 0f);
            
        }
    }
    public class SandDiablosTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
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
			NPC.defense = 6;
			NPC.lifeMax = 400;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Main.npc[Head].dontTakeDamage = true;
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
    }
    public class SandDiablos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵");
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
			NPC.lifeMax = 400;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void OnKill()
        {
            MyWorld.SandDiablos = true;
            Main.NewText("少量的荒砂已经附着到沙漠生灵的身上", new Color(182, 146, 86));
            Dust.NewDust(NPC.Center, 1, 1, MyDustId.YellowGoldenFire, NPC.velocity.X / 10, NPC.velocity.Y / 10);
            Dust.NewDust(NPC.Center, 1, 1, MyDustId.YellowFx1, NPC.velocity.X / 10, NPC.velocity.Y / 10);
            base.OnKill();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandDiablosCarapace>(), 1, 7, 18));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandDiablosHorn>(), 1, 2, 2));
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
        bool tailAttack = false;
        int tailType = 0;
        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (NPC.localAI[0] == 0f) 
            {
                leftClaw = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X - 48, (int)NPC.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[leftClaw].position = NPC.position + new Vector2(-48, 60);
                (Main.npc[leftClaw].ModNPC as SandDiablosClaw).Head = NPC.whoAmI;
                (Main.npc[leftClaw].ModNPC as SandDiablosClaw).RorL = -1;

                rightClaw = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + 48, (int)NPC.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[rightClaw].position = NPC.position + new Vector2(48, 60);
                (Main.npc[rightClaw].ModNPC as SandDiablosClaw).Head = NPC.whoAmI;
                (Main.npc[rightClaw].ModNPC as SandDiablosClaw).RorL = 1;

                leftShoulder = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X - 44, (int)NPC.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[leftShoulder].position = NPC.position + new Vector2(-44, 40);
                (Main.npc[leftShoulder].ModNPC as SandDiablosShoulder).Head = NPC.whoAmI;
                (Main.npc[leftShoulder].ModNPC as SandDiablosShoulder).RorL = -1;

                rightShoulder = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + 44, (int)NPC.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[rightShoulder].position = NPC.position + new Vector2(44, 40);
                (Main.npc[rightShoulder].ModNPC as SandDiablosShoulder).Head = NPC.whoAmI;
                (Main.npc[rightShoulder].ModNPC as SandDiablosShoulder).RorL = 1;

                body = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y + 30, ModContent.NPCType<SandDiablosBody>());
                Main.npc[body].position = NPC.position + new Vector2(0, 30);
                (Main.npc[body].ModNPC as SandDiablosBody).Head = NPC.whoAmI;

                tail = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y + 70, ModContent.NPCType<SandDiablosTail>());
                Main.npc[tail].position = NPC.position + new Vector2(0, 70);
                (Main.npc[tail].ModNPC as SandDiablosTail).Head = NPC.whoAmI;
				NPC.netUpdate = true;
                NPC.Center = player.Center + new Vector2(0, -250);
				NPC.localAI[0] = 1f;
			}
            if(!Main.npc[body].active) 
            {
                if(!Main.npc[leftShoulder].active) Main.npc[leftClaw].dontTakeDamage = false;
                if(!Main.npc[rightShoulder].active) Main.npc[rightClaw].dontTakeDamage = false;
            }
            if(!Main.npc[leftShoulder].active && !Main.npc[rightShoulder].active 
                && !Main.npc[body].active && !Main.npc[tail].active 
                && !Main.npc[leftClaw].active && !Main.npc[rightClaw].active) 
            {
                NPC.dontTakeDamage = false;
                NPC.life = 0;
                Main.npc[leftShoulder].checkDead();
                Main.npc[leftClaw].checkDead();
                Main.npc[rightClaw].checkDead();
                Main.npc[rightShoulder].checkDead();
                Main.npc[body].checkDead();
                Main.npc[tail].checkDead();
                NPC.checkDead();
            }
            
            if(NPC.Distance(player.Center + new Vector2(0, -250)) > 10 && !Attack) NPC.velocity = Vector2.Normalize(player.Center + new Vector2(0, - 250) - NPC.Center) * 2.1f * NPC.Distance(player.Center + new Vector2(0, - 250)) / 70;
            else if(Attack) NPC.velocity = Vector2.Zero;;
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
                if(NPC.ai[1] % 120 == 25) Main.npc[attackClaw].noGravity = true;
                if(NPC.ai[1] % 120 <= 45 && NPC.ai[1] % 120 >= 25) Main.npc[attackClaw].velocity = Vector2.Normalize(player.Center - Main.npc[attackClaw].Center) * 20f; 
                if(NPC.ai[1] % 120 <= 85 && NPC.ai[1] % 120 >= 25) 
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
                if(NPC.ai[1] == 0)
                {
                    Attack = true;
                    Main.npc[leftClaw].frame.Y += 44;
                    Main.npc[rightClaw].frame.Y += 44;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = true;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).nextCenter = Vector2.Zero;
                }
                if(NPC.ai[1] <= 10)
                {
                    Main.npc[leftClaw].velocity = new Vector2(0, -2) * (10 - NPC.ai[1]) / 5;
                    Main.npc[rightClaw].velocity = new Vector2(0, -2) * (10 - NPC.ai[1]) / 5;
                }
                if(NPC.ai[1] == 10)
                {
                    Main.npc[leftClaw].noGravity = false;
                    Main.npc[rightClaw].noGravity = false;
                    Main.npc[leftClaw].noTileCollide = false;
                    Main.npc[rightClaw].noTileCollide = false;
                }
                if(NPC.ai[1] >= 50)
                {
                    if(Main.npc[leftClaw].collideY || Main.npc[rightClaw].collideY )
                    {
                        if(NPC.ai[1] >= 50)  Main.player[NPC.target].AddBuff(ModContent.BuffType<Dizziness>(), 60);
                        if(NPC.ai[1] <= 75)
                        {
                            PunchCameraModifier PCM = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2(), 20f, 6f, 20, 1000f, "Rock");
                            EffectPlayer.CMS.Add(PCM);
                        }
                    }
                }
                if(NPC.ai[1] == 90)
                {
                    Attack = false;
                    (Main.npc[leftClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    (Main.npc[rightClaw].ModNPC as SandDiablosClaw).thisAttack = false;
                    Main.npc[leftClaw].frame.Y -= 44;
                    Main.npc[rightClaw].frame.Y -= 44;
                    Main.npc[leftClaw].noGravity = true;
                    Main.npc[rightClaw].noGravity = true;
                    Main.npc[leftClaw].noTileCollide = true;
                    Main.npc[rightClaw].noTileCollide = true;
                }
            }
            NPC.ai[1]++;
            if(NPC.ai[1] == 150 && AttackType == 1)
            {
                AttackType = Main.rand.Next(1, 4);
                NPC.ai[1] = 0;
            }
            if(NPC.ai[1] == 150 && AttackType == 3)
            {
                AttackType = 1;
                NPC.ai[1] = 0;
            }
            if(NPC.ai[1] == 600) 
            {
                switch(AttackType)
                {
                    case 0:
                        AttackType = 1;
                        break;
                    case 2:
                        AttackType = Main.rand.Next(0, 4);
                        break;
                }
                NPC.ai[1] = 0;
            }
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
            base.AI();
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