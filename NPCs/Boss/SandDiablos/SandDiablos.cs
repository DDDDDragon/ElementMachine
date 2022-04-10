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

namespace ElementMachine.NPCs.Boss.SandDiablos
{
    public class SandDiablosShoulder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂角魔灵");
            base.SetStaticDefaults();
        }
        public int RorL
        {
            get => npc.spriteDirection;
            set => npc.spriteDirection = value;
        }
        public int Head
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        public bool Attack
        {
            get => (Main.npc[Head].modNPC as SandDiablos).Attack;
        }
        public override void SetDefaults()
        {
            npc.height = 40;
            npc.width = 74;
            npc.friendly = false;
            npc.value = 10000;
			npc.damage = 25;
			npc.defense = 6;
			npc.lifeMax = 500;
			npc.aiStyle = -1;
			npc.noGravity = false;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            npc.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            if(RorL == 1) Main.npc[(HeadN.modNPC as SandDiablos).rightClaw].dontTakeDamage = true;
            else Main.npc[(HeadN.modNPC as SandDiablos).leftClaw].dontTakeDamage = true;
            Main.npc[Head].dontTakeDamage = true;
            Vector2 nextCenter = new Vector2(HeadN.Center.X + RorL * (44 + (float)Math.Sin(HeadN.ai[1] / 30) * 3), HeadN.Center.Y + 20 - (float)Math.Sin(HeadN.ai[1] / 30) * 2);
            npc.velocity = Vector2.Normalize(nextCenter - npc.Center) * 4.5f * Vector2.Distance(nextCenter, npc.Center) / 35;
        }
    }
    public class SandDiablosClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂角魔灵");
            base.SetStaticDefaults();
        }
        public int RorL
        {
            get => npc.spriteDirection;
            set => npc.spriteDirection = value;
        }
        public int Head
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void SetDefaults()
        {
            npc.height = 40;
            npc.width = 74;
            npc.friendly = false;
            npc.value = 10000;
			npc.damage = 25;
			npc.defense = 6;
			npc.lifeMax = 500;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            npc.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public bool Attack
        {
            get => (Main.npc[Head].modNPC as SandDiablos).Attack || this.thisAttack;
        }
        public bool thisAttack = false;
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Main.npc[Head].dontTakeDamage = true;
            Vector2 nextCenter = new Vector2(HeadN.Center.X + RorL * (48 + (float)Math.Sin(HeadN.ai[1] / 30) * 3), HeadN.Center.Y + 60 - (float)Math.Sin(HeadN.ai[1] / 30) * 2);
            if(!Attack) npc.velocity = Vector2.Normalize(nextCenter - npc.Center) * 4.5f * Vector2.Distance(nextCenter, npc.Center) / 50;
        }
    }
    public class SandDiablosBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂角魔灵");
            base.SetStaticDefaults();
        }
        public int Head
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        public bool Attack
        {
            get => (Main.npc[Head].modNPC as SandDiablos).Attack;
        }
        public override void SetDefaults()
        {
            npc.height = 40;
            npc.width = 74;
            npc.friendly = false;
            npc.value = 10000;
			npc.damage = 25;
			npc.defense = 6;
			npc.lifeMax = 800;
			npc.aiStyle = -1;
			npc.noGravity = false;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            npc.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            NPC HeadN = new NPC();
            HeadN = Main.npc[Head];
            Main.npc[Head].dontTakeDamage = true;
            Vector2 nextCenter = new Vector2(HeadN.Center.X, HeadN.Center.Y + 30);
            npc.velocity = Vector2.Normalize(nextCenter - npc.Center) * 4.5f * Vector2.Distance(nextCenter, npc.Center) / 30;
            npc.rotation = npc.velocity.X * 0.1f;
            if((double)npc.rotation < -0.2)
            {
                npc.rotation = -0.2f;
            }
            if((double)npc.rotation > 0.2)
            {
                npc.rotation = 0.2f;
            }
        }
    }
    public class SandDiablosTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂角魔灵");
            base.SetStaticDefaults();
        }
        public int Head
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        public bool thisAttack = false;
        public bool Attack
        {
            get => this.thisAttack;
        }
        public override void SetDefaults()
        {
            npc.height = 40;
            npc.width = 74;
            npc.friendly = false;
            npc.value = 10000;
			npc.damage = 25;
			npc.defense = 6;
			npc.lifeMax = 500;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            npc.buffImmune[BuffID.Confused] = true;
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
                npc.velocity = Vector2.Normalize(nextCenter - npc.Center) * 4.5f * Vector2.Distance(nextCenter, npc.Center) / 55;
                npc.rotation = npc.velocity.X * 0.1f;
                if((double)npc.rotation < -0.2)
                {
                    npc.rotation = -0.2f;
                }
                if((double)npc.rotation > 0.2)
                {
                    npc.rotation = 0.2f;
                }
            }
        }
    }
    public class SandDiablos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablos");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂角魔灵");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            npc.height = 40;
            npc.width = 74;
            npc.friendly = false;
            npc.value = 10000;
			npc.damage = 25;
			npc.defense = 6;
			npc.lifeMax = 500;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[ModContent.BuffType<lowerSpeed>()] = true;
            npc.buffImmune[BuffID.Confused] = true;
            base.SetDefaults();
        }
        public override void NPCLoot()
		{
			switch(Main.rand.Next(1, 6))
            {
                case 0 : 
                    Item.NewItem(npc.Center, Vector2.Zero, ModContent.ItemType<SandCrackerShieldSpear>());
                    break;
                case 1 :
                    Item.NewItem(npc.Center, Vector2.Zero, ModContent.ItemType<SandCrackerBow>());
                    break;
            }
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
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            if (npc.localAI[0] == 0f) 
            {
                leftClaw = NPC.NewNPC((int)npc.position.X - 48, (int)npc.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[leftClaw].position = npc.position + new Vector2(-48, 60);
                (Main.npc[leftClaw].modNPC as SandDiablosClaw).Head = npc.whoAmI;
                (Main.npc[leftClaw].modNPC as SandDiablosClaw).RorL = -1;

                rightClaw = NPC.NewNPC((int)npc.position.X + 48, (int)npc.position.Y + 60, ModContent.NPCType<SandDiablosClaw>());
                Main.npc[rightClaw].position = npc.position + new Vector2(48, 60);
                (Main.npc[rightClaw].modNPC as SandDiablosClaw).Head = npc.whoAmI;
                (Main.npc[rightClaw].modNPC as SandDiablosClaw).RorL = 1;

                leftShoulder = NPC.NewNPC((int)npc.position.X - 44, (int)npc.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[leftShoulder].position = npc.position + new Vector2(-44, 40);
                (Main.npc[leftShoulder].modNPC as SandDiablosShoulder).Head = npc.whoAmI;
                (Main.npc[leftShoulder].modNPC as SandDiablosShoulder).RorL = -1;

                rightShoulder = NPC.NewNPC((int)npc.position.X + 44, (int)npc.position.Y + 40, ModContent.NPCType<SandDiablosShoulder>());
                Main.npc[rightShoulder].position = npc.position + new Vector2(44, 40);
                (Main.npc[rightShoulder].modNPC as SandDiablosShoulder).Head = npc.whoAmI;
                (Main.npc[rightShoulder].modNPC as SandDiablosShoulder).RorL = 1;

                body = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 30, ModContent.NPCType<SandDiablosBody>());
                Main.npc[body].position = npc.position + new Vector2(0, 30);
                (Main.npc[body].modNPC as SandDiablosBody).Head = npc.whoAmI;

                tail = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 70, ModContent.NPCType<SandDiablosTail>());
                Main.npc[tail].position = npc.position + new Vector2(0, 70);
                (Main.npc[tail].modNPC as SandDiablosTail).Head = npc.whoAmI;
				npc.netUpdate = true;
                npc.Center = player.Center + new Vector2(0, -250);
				npc.localAI[0] = 1f;
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
                npc.dontTakeDamage = false;
                npc.life = 0;
                Main.npc[leftShoulder].checkDead();
                Main.npc[leftClaw].checkDead();
                Main.npc[rightClaw].checkDead();
                Main.npc[rightShoulder].checkDead();
                Main.npc[body].checkDead();
                Main.npc[tail].checkDead();
                npc.checkDead();
            }
            
            if(npc.Distance(player.Center + new Vector2(0, -250)) > 10 && !Attack) npc.velocity = Vector2.Normalize(player.Center + new Vector2(0, - 250) - npc.Center) * 2.1f * npc.Distance(player.Center + new Vector2(0, - 250)) / 70;
            else if(Attack) npc.velocity = Vector2.Zero;;
            if(npc.ai[1] <= 600 && AttackType == 0)
            {
                if(npc.ai[1] % 60 <= 7)
                {
                    if(npc.ai[1] % 60 == 0)
                    {
                        if(Main.rand.Next(0, 2) == 0)
                        {
                            (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = true;
                            Main.npc[leftClaw].velocity = new Vector2(3, 3) * 10 / 7;
                            attackClaw = leftClaw;
                        }
                        else
                        {
                            (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = true;
                            Main.npc[rightClaw].velocity = new Vector2(-3, 3) * 10 / 7;
                            attackClaw = rightClaw;
                        }
                        Vector2 velo = Vector2.Normalize(player.Center - Main.npc[attackClaw].Center) * 6.5f;
                        int proj = Projectile.NewProjectile(Main.npc[attackClaw].Center, velo, ModContent.ProjectileType<SandBonePiece>(), 10, 0f, 255);
                        Main.projectile[proj].ai[0] = (Main.npc[attackClaw].modNPC as SandDiablosClaw).RorL;
                    }
                    Main.npc[attackClaw].velocity = new Vector2(3 * (Main.npc[attackClaw].modNPC as SandDiablosClaw).RorL * -1, 3) * (10 - npc.ai[1] % 60) / 7;
                    if(npc.ai[1] % 60 == 7) (Main.npc[attackClaw].modNPC as SandDiablosClaw).thisAttack = false;       
                }
            }
            if(AttackType == 1)
            {
                if(npc.ai[1] == 0)
                {
                    (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = true;
                    Main.npc[leftClaw].velocity = new Vector2(2, -2) * 10 / 7;
                    (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = true;
                    Main.npc[rightClaw].velocity = new Vector2(-2, -2) * 10 / 7;
                    Attack = true;
                    Main.npc[body].velocity = Vector2.Zero;
                    Main.npc[leftShoulder].velocity = Vector2.Zero;
                    Main.npc[rightShoulder].velocity = Vector2.Zero;
                    Main.npc[tail].velocity = Vector2.Zero;
                }
                if(npc.ai[1] <= 5)
                {
                    Main.npc[leftClaw].rotation -= 0.2f;
                    Main.npc[rightClaw].rotation += 0.2f;
                }
                if(npc.ai[1] == 5)
                {
                    Main.npc[leftClaw].velocity = Vector2.Zero;
                    Main.npc[rightClaw].velocity = Vector2.Zero;
                }
                if(npc.ai[1] <= 45 && npc.ai[1] >= 30)
                {
                    Main.npc[leftClaw].velocity = new Vector2(-3, 3) * (45 - npc.ai[1]) / 7;
                    Main.npc[rightClaw].velocity = new Vector2(3, 3) * (45 - npc.ai[1]) / 7;
                    Main.npc[leftClaw].rotation += 0.1f;
                    Main.npc[rightClaw].rotation -= 0.1f;
                }
                if(npc.ai[1] == 50)
                {
                    Main.npc[leftClaw].rotation = 0f;
                    Main.npc[rightClaw].rotation = 0f;
                    (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = false;
                    (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = false;
                    Projectile.NewProjectile(npc.Center + new Vector2(-300, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Projectile.NewProjectile(npc.Center + new Vector2(300, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Attack = false;
                }
                if(npc.ai[1] >= 80 && npc.ai[1] <= 110 && (npc.ai[1] - 50) % 30 == 0)
                {
                    Projectile.NewProjectile(npc.Center + new Vector2(-300 + (npc.ai[1] - 50) * 2, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Projectile.NewProjectile(npc.Center + new Vector2(300 - (npc.ai[1] - 50) * 2, 150), Vector2.Zero, ProjectileID.SandnadoHostile, 10, 0f);
                    Attack = false;
                }
            }
            if(AttackType == 2)
            {
                if(npc.ai[1] % 120 == 0)
                {
                    if(Main.rand.Next(0, 2) == 1)
                    {
                        (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = true;
                        Main.npc[leftClaw].velocity = new Vector2(-2, -2) * 10 / 7;
                        attackClaw = leftClaw;
                    }
                    else
                    {
                        (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = true;
                        Main.npc[rightClaw].velocity = new Vector2(2, -2) * 10 / 7;
                        attackClaw = rightClaw;
                    }
                }
                if(npc.ai[1] % 120 == 5) Main.npc[attackClaw].velocity *= 0;
                if(npc.ai[1] % 120 == 10) Main.npc[attackClaw].velocity = Vector2.Normalize(player.Center - Main.npc[attackClaw].Center) * 20f;
                if(npc.ai[1] % 120 <= 65) Main.npc[attackClaw].velocity *= 0.98f;
                if(npc.ai[1] % 120 == 70) Main.npc[attackClaw].velocity *= 0;
                if(npc.ai[1] % 120 == 75) (Main.npc[attackClaw].modNPC as SandDiablosClaw).thisAttack = false;

            }
            if(AttackType == 3)
            {
                if(npc.ai[1] == 0)
                {
                    Attack = true;
                    (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = true;
                    (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = true;
                }
                if(npc.ai[1] <= 10)
                {
                    Main.npc[leftClaw].velocity = new Vector2(0, -2) * (10 - npc.ai[1]) / 5;
                    Main.npc[rightClaw].velocity = new Vector2(0, -2) * (10 - npc.ai[1]) / 5;
                }
                if(npc.ai[1] == 10)
                {
                    Main.npc[leftClaw].noGravity = false;
                    Main.npc[rightClaw].noGravity = false;
                    Main.npc[leftClaw].noTileCollide = false;
                    Main.npc[rightClaw].noTileCollide = false;
                }
                if(npc.ai[1] >= 50)
                {
                    if(Main.npc[leftClaw].collideY || Main.npc[rightClaw].collideY )
                    {
                        if(npc.ai[1] >= 50)  Main.player[npc.target].AddBuff(ModContent.BuffType<Dizziness>(), 120);
                        if(npc.ai[1] <= 75)
                        {
                            PunchCameraModifier PCM = new PunchCameraModifier(npc.Center, (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2(), 20f, 6f, 20, 1000f, "Rock");
                            EffectPlayer.CMS.Add(PCM);
                        }
                    }
                }
                if(npc.ai[1] == 90)
                {
                    Attack = false;
                    (Main.npc[leftClaw].modNPC as SandDiablosClaw).thisAttack = false;
                    (Main.npc[rightClaw].modNPC as SandDiablosClaw).thisAttack = false;
                    Main.npc[leftClaw].noGravity = true;
                    Main.npc[rightClaw].noGravity = true;
                    Main.npc[leftClaw].noTileCollide = true;
                    Main.npc[rightClaw].noTileCollide = true;
                }
            }
            npc.ai[1]++;
            if(npc.ai[1] == 150 && AttackType == 1)
            {
                AttackType = Main.rand.Next(1, 4);
                npc.ai[1] = 0;
            }
            if(npc.ai[1] == 150 && AttackType == 3)
            {
                AttackType = 1;
                npc.ai[1] = 0;
            }
            if(npc.ai[1] == 600) 
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
                npc.ai[1] = 0;
            }
            npc.ai[2]++;
            if(npc.ai[2] == 60)
            {
                if(tailType == 0)
                {
                    (Main.npc[tail].modNPC as SandDiablosTail).thisAttack = true;
                }
            }
            if(npc.ai[2] <= 420 && tailType == 0 && npc.ai[2] > 60)
            {
                if((npc.ai[2] - 60) % 120 == 50)
                {
                    Main.npc[tail].velocity = Vector2.Normalize(player.Center - Main.npc[tail].Center) * 25f;
                }
                if((npc.ai[2] - 60) % 120 >= 51 && npc.ai[2] % 120 <= 111)
                {
                    Main.npc[tail].velocity *= 0.93f;
                    Main.npc[tail].rotation = Main.npc[tail].velocity.ToRotation() - (float)Math.PI / 2;
                }
                if((npc.ai[2] - 60) % 120 >= 111)
                {
                    Main.npc[tail].velocity = Vector2.Zero;
                    Main.npc[tail].rotation = (player.Center - Main.npc[tail].Center).ToRotation() - (float)Math.PI / 2;
                }
            }
            if(npc.ai[2] == 420)
            {
                npc.ai[2] = 0;
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
                (Main.npc[tail].modNPC as SandDiablosTail).thisAttack = false;
            }
            base.AI();
        }
    }
    public class SandBonePiece : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("SandBonePiece");
            DisplayName.AddTranslation(GameCulture.Chinese, "砂骨碎片");
        }
        public override void SetDefaults()
        {
            projectile.height = 30;
            projectile.width = 32;
            projectile.penetrate = 3;
            projectile.tileCollide = false;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.friendly = false;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            base.SetDefaults();
        }
		public override void AI()
		{
            projectile.rotation = projectile.velocity.ToRotation() * projectile.ai[0];
            projectile.alpha = 80;
            projectile.spriteDirection = (int)projectile.ai[0];
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            MyProjectile.ProjectileDrawTail(projectile, ModContent.GetTexture("ElementMachine/NPCs/Boss/SandDiablos/tail"), Color.RosyBrown);
            return base.PreDraw(spriteBatch, lightColor);
        }
    }
}