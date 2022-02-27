using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Input;
using System.Collections.ObjectModel;
using System.Linq;
namespace ElementMachine.Bases
{
	public abstract class BaseSickleItem : ModItem
	{
		public override void SetStaticDefaults()
        {
			SetSickle();
            DisplayName.SetDefault(defaultName);
            DisplayName.AddTranslation(GameCulture.Chinese, transName);
			Tooltip.SetDefault("shoot a chain blade and keep it on the first enemy it hits ");
			Tooltip.AddTranslation(GameCulture.Chinese, "[c/00FF00:「钩镰」]\n发射一个链刃并钩住第一个它命中的敌人,松开左键会收回\n若敌人超出160码则不会钩住\n每次钩中敌人可以增加一点命中点数\n每次钩中敌人时按右键可以向鼠标方向位移一段距离,位移期间无敌\n若你与敌人的距离大于160码则敌人会向你的方向同时位移更小的一段距离\n若位移未结束前收回钩镰会进行一次超远距离跳跃\n[c/8c6640:「特效」]\n钩镰存在时,按下E键," + Effect);
        }
		public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y) 
		{
			return true;
		}
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset) {
			if (!line.oneDropLogo) 
			{
				string sepText = "[c/00FF00:「" + Main.LocalPlayer.GetModPlayer<BasePlayer>().SickleDamagePer * 100 + "%额外钩镰伤害」]";
				if (line.Name == "Damage" && line.mod == "Terraria")
				{
					Color color = new Color(220,220,157);
					float drawX = line.X + line.font.MeasureString(sepText).X - 150;
					float drawY = line.Y;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.font, sepText,
						new Vector2(drawX, drawY), line.color, line.rotation, line.origin, line.baseScale, line.maxWidth, line.spread);
				}
				else 
				{
					yOffset = 0;
				}
			}
			return true;
		}
		public string defaultName = " ";
		public string transName = " ";
		public string Effect = " ";
		public int shootType = 0;
		public int damage = 0;
		public float shootSpeed = 15.1f;
        public override void SetDefaults()
		{
			SetSickle();
			item.width = 44;
			item.height = 44;
			item.value = Item.sellPrice(silver: 5);
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 0;
			item.damage = damage;
			item.noUseGraphic = true;
			item.shoot = shootType;
			item.shootSpeed = shootSpeed;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.crit = 2;
			item.channel = true;
		}  
		public virtual void SetSickle()
		{

		}
	}
    public abstract class BaseSickle : ModProjectile
    {
        public int ItemType = -200;
		public virtual void Update(int Type)
        {
			ItemType = Type;
        }
        public virtual void SetSprite(string a)
        {
            chainTexture = ModContent.GetTexture(a);
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("wood");
		}
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 28;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
            projectile.knockBack = 0;
		}
		public static bool Attack = false;			
		public static Player player;		
		public static int HitTimer = 0;
		public NPC Target = new NPC();
		public int CoolTimer = 0;
		public bool Cool = true;
        public int CoolTimer2 = 0;
		public bool Cool2 = true;
        bool mouseLeft = false;
        bool mouseRight = false;
        Vector2 mouseOldPos;
        Vector2 mouseMovement;
		public override void AI()
		{
			player = Main.player[projectile.owner];
			if (player.dead) 
			{
				projectile.Kill();
				return;
			}
			mouseMovement = Main.MouseScreen - mouseOldPos;
            mouseOldPos = Main.MouseScreen;
			player.itemAnimation = 10;
			player.itemTime = 10;
			int newDirection = projectile.Center.X > player.Center.X ? 1 : -1;
			player.ChangeDir(newDirection);
			projectile.direction = newDirection;
			var vectorToPlayer = player.MountedCenter - projectile.Center;
			float currentChainLength = vectorToPlayer.Length();
			if (projectile.ai[0] == 0f) 
            {
                CoolTimer++;
				float maxChainLength = 160f;
				projectile.tileCollide = true;
				if (currentChainLength > maxChainLength) 
                {
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
				}
				else if (!player.channel)
                {
					if (projectile.velocity.Y < 0f) projectile.velocity.Y *= 0.9f;
					projectile.velocity.Y += 1f;
					projectile.velocity.X *= 0.9f;
				}
                if(Main.mouseLeft) mouseLeft = true;
                if(Main.mouseLeftRelease && mouseLeft && CoolTimer > 120) 
                {
                    projectile.ai[0] = 1f;
                    mouseLeft = false;
                }
                
			}
			else if (projectile.ai[0] == 1f) 
            {
                CoolTimer = 0;
				float elasticFactorA = 14f / player.meleeSpeed;
				float elasticFactorB = 0.9f / player.meleeSpeed;
				float maxStretchLength = 300f; 
				if (projectile.ai[1] == 1f) projectile.tileCollide = false;
				if (!player.channel || currentChainLength > maxStretchLength || !projectile.tileCollide) 
                {
					projectile.ai[1] = 1f;
					if (projectile.tileCollide) projectile.netUpdate = true;
					projectile.tileCollide = false;
					if (currentChainLength < 20f) projectile.Kill();
				}
				if (!projectile.tileCollide) elasticFactorB *= 2f;
				int restingChainLength = 60;
				if (currentChainLength > restingChainLength || !projectile.tileCollide) {
					var elasticAcceleration = vectorToPlayer * elasticFactorA / currentChainLength - projectile.velocity;
					elasticAcceleration *= elasticFactorB / elasticAcceleration.Length();
					projectile.velocity *= 0.98f;
					projectile.velocity += elasticAcceleration;
				}
				else {
					if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f) 
                    {
						projectile.velocity.X *= 0.96f;
						projectile.velocity.Y += 0.2f;
					}
					if (player.velocity.X == 0f) projectile.velocity.X *= 0.96f;
				}
			}
            if(Main.keyState.IsKeyDown(Keys.W)) Up = true;
            else Up = false;
            projectile.rotation = vectorToPlayer.ToRotation() - projectile.velocity.X * 0.1f;
            if(Attack) HitTimer++;
			if(HitTimer == 600) HitTimer = 0;
			if(Target.life <= 0) HitTimer = 0;
            if(!Cool) CoolTimer++;
            if(CoolTimer == 180) 
            {
                Cool = true;
                CoolTimer = 0;
            }
			if(HitTimer < 600 && HitTimer > 0 && Target != new NPC() && Target.life >= 0 && player.channel && Target.type != NPCID.TargetDummy && !Target.immortal) 
			{
                if(CanAdd) 
                {
                    Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint++;
                    CombatText.NewText(player.getRect(), Color.Blue, Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint);
                    CanAdd = false;
                }
                if(Main.mouseRight) mouseRight = true;
				if(Keyboard.GetState().IsKeyDown(Keys.E)) E = true;
				if(Keyboard.GetState().IsKeyUp(Keys.E) && E) 
				{
					Effect();
					E = false;
				}
				projectile.Center = Target.Center;
                foreach(var i in Main.npc)
                {
                    if(Target == i && i.type != NPCID.TargetDummy)
                    {
                        CF(i);
                    }
                }
				
			}
			else Attack = false;
		}
        public bool CanAdd = true;
        public void CF(NPC i)
        {
            if(mouseRight && Main.mouseRightRelease && Cool && !Up)
            {
                dash = true;
				player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 20;
                if(Vector2.Distance(i.Center, player.Center) > 160 && !i.boss) i.velocity = Vector2.Normalize(player.Center - i.Center) * 10;
                Cool = false;
                mouseRight = false;
            }
            if(dash)
            {
                timer1++;
                player.immune = true;
                player.immuneTime = 10;
				player.immuneNoBlink = true;
                if(timer1 == 12)
                {
                    player.velocity = Vector2.Zero;
                    i.velocity = Vector2.Zero;
                    timer1 = 0;
                    dash = false;
                    if(Vector2.Distance(i.Center, player.Center) <= 100) Move2 = true;
                }
            }
        }
		public virtual void Effect()
		{

		}
		public bool E = false;
        public bool Up = false;
        public int timer1 = 0;        
        public int timer2 = 0;        
        public bool dash = false;  
        public bool pullUp = false;
        public bool Move2 = false;          
        float t = 0;
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			bool shouldMakeSound = false;
			if (oldVelocity.X != projectile.velocity.X) 
            {
				if (Math.Abs(oldVelocity.X) > 4f) 
                {
					shouldMakeSound = true;
				}
				projectile.position.X += projectile.velocity.X;
				projectile.velocity.X = -oldVelocity.X * 0.2f;
			}
			if (oldVelocity.Y != projectile.velocity.Y)
            {
				if (Math.Abs(oldVelocity.Y) > 4f) 
                {
					shouldMakeSound = true;
				}
				projectile.position.Y += projectile.velocity.Y;
				projectile.velocity.Y = -oldVelocity.Y * 0.2f;
			}
			projectile.ai[0] = 1f;
			if (shouldMakeSound) 
            {
				projectile.netUpdate = true;
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y);
			}
			return false;
		}
        Texture2D chainTexture;
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			
			Vector2 mountedCenter = player.MountedCenter;
			SetSprite("");
			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;
			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;
			if (projectile.alpha == 0) 
            {
				int direction = -1;
				if (projectile.Center.X < mountedCenter.X) direction = 1;
				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}
			while (true) 
            {
				float length = remainingVectorToPlayer.Length();
				if (length < 25f || float.IsNaN(length)) break;
				drawPosition += remainingVectorToPlayer * 12 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;
				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(HitTimer == 0 && projectile.ai[0] == 0f) 
			{
				Attack = true;
				Target = target;
			}
			base.OnHitNPC(target, damage, knockback, crit);
        }
    
	}
}