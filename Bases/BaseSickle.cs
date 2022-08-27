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
using Terraria.Audio;

namespace ElementMachine.Bases
{
	public abstract class BaseSickleItem : ElementItem
	{
		public override void SetStaticDefaults()
        {
			SetSickle();
            DisplayName.SetDefault(defaultName);
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), transName);
			Tooltip.SetDefault("shoot a chain blade and keep it on the first enemy it hits ");
			string trans = "[c/00FF00:「钩镰」]\n发射一个链刃并钩住第一个它命中的敌人,松开左键会收回\n若敌人超出160码则不会钩住\n每次钩中敌人可以增加一点命中点数\n每次钩中敌人时按右键可以向鼠标方向位移一段距离,位移期间无敌\n若你与敌人的距离大于160码则敌人会向你的方向同时位移更小的一段距离\n若位移未结束前收回钩镰会进行一次超远距离跳跃\n[c/8c6640:「特效」]\n";
			if(press) trans += "钩镰存在时,按下E键,";
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), trans + Effect);
        }
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset) 
		{
			base.PreDrawTooltipLine(line, ref yOffset);
			if (!line.OneDropLogo) 
			{
				string sepText = "[c/00FF00:「" + Main.LocalPlayer.GetModPlayer<BasePlayer>().SickleDamagePer * 100 + "%额外钩镰伤害」]";
				if (line.Name == "Damage" && line.Mod == "Terraria")
				{
					Color color = new Color(220,220,157);
					float drawX = line.X + line.Font.MeasureString(sepText).X - 150;
					float drawY = line.Y;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, sepText,
						new Vector2(drawX, drawY), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
				}
				else 
				{
					yOffset = 0;
				}
			}
			return true;
		}
		public bool press = true;
		public string defaultName = " ";
		public string transName = " ";
		public string Effect = " ";
		public int shootType = 0;
		public int damage = 0;
		public float shootSpeed = 15.1f;
        public override void SetDefaults()
		{
			SetSickle();
			Item.width = 44;
			Item.height = 44;
			Item.value = Item.sellPrice(silver: 5);
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.knockBack = 0;
			Item.damage = damage;
			Item.noUseGraphic = true;
			Item.shoot = shootType;
			Item.shootSpeed = shootSpeed;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.crit = 2;
			Item.channel = true;
		}  
		public virtual void SetSickle()
		{

		}
	}
    public abstract class BaseSickle : ElementProj
    {
        public int ItemType = -200;
		public virtual void Update(int Type)
        {
			ItemType = Type;
        }
        public virtual void SetSprite(string a)
        {
            chainTexture = ModContent.Request<Texture2D>(a).Value;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("wood");
		}
		public override void SetDefaults()
		{
			Projectile.width = 46;
			Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.knockBack = 0;
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
			player = Main.player[Projectile.owner];
			if (player.dead) 
			{
				Projectile.Kill();
				return;
			}
			mouseMovement = Main.MouseScreen - mouseOldPos;
            mouseOldPos = Main.MouseScreen;
			player.itemAnimation = 10;
			player.itemTime = 10;
			int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
			player.ChangeDir(newDirection);
			Projectile.direction = newDirection;
			var vectorToPlayer = player.MountedCenter - Projectile.Center;
			float currentChainLength = vectorToPlayer.Length();
			if (Projectile.ai[0] == 0f) 
            {
                CoolTimer++;
				float maxChainLength = 160f;
				Projectile.tileCollide = true;
				if (currentChainLength > maxChainLength) 
                {
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
				}
				else if (!player.channel)
                {
					if (Projectile.velocity.Y < 0f) Projectile.velocity.Y *= 0.9f;
					Projectile.velocity.Y += 1f;
					Projectile.velocity.X *= 0.9f;
				}
                if(Main.mouseLeft) mouseLeft = true;
                if(Main.mouseLeftRelease && mouseLeft && CoolTimer > 120) 
                {
                    Projectile.ai[0] = 1f;
                    mouseLeft = false;
                }
                
			}
			else if (Projectile.ai[0] == 1f) 
            {
                CoolTimer = 0;
				float elasticFactorA = 14f / player.GetAttackSpeed(DamageClass.Melee);
				float elasticFactorB = 0.9f / player.GetAttackSpeed(DamageClass.Melee);
				float maxStretchLength = 300f; 
				if (Projectile.ai[1] == 1f) Projectile.tileCollide = false;
				if (!player.channel || currentChainLength > maxStretchLength || !Projectile.tileCollide) 
                {
					Projectile.ai[1] = 1f;
					if (Projectile.tileCollide) Projectile.netUpdate = true;
					Projectile.tileCollide = false;
					if (currentChainLength < 20f) Projectile.Kill();
				}
				if (!Projectile.tileCollide) elasticFactorB *= 2f;
				int restingChainLength = 60;
				if (currentChainLength > restingChainLength || !Projectile.tileCollide) {
					var elasticAcceleration = vectorToPlayer * elasticFactorA / currentChainLength - Projectile.velocity;
					elasticAcceleration *= elasticFactorB / elasticAcceleration.Length();
					Projectile.velocity *= 0.98f;
					Projectile.velocity += elasticAcceleration;
				}
				else {
					if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 6f) 
                    {
						Projectile.velocity.X *= 0.96f;
						Projectile.velocity.Y += 0.2f;
					}
					if (player.velocity.X == 0f) Projectile.velocity.X *= 0.96f;
				}
			}
            if(Main.keyState.IsKeyDown(Keys.W)) Up = true;
            else Up = false;
            Projectile.rotation = vectorToPlayer.ToRotation() - Projectile.velocity.X * 0.1f;
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
				if(HitTimer % 60 == 0)
				{
					if(Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint < 30)
					{
						Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint++;
                    	CombatText.NewText(player.getRect(), Color.Blue, Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint);
					}
				}
                if(Main.mouseRight) mouseRight = true;
				if(Keyboard.GetState().IsKeyDown(Keys.E)) E = true;
				if(Keyboard.GetState().IsKeyUp(Keys.E) && E) 
				{
					Effect();
					E = false;
				}
				Projectile.Center = Target.Center;
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
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			bool shouldMakeSound = false;
			if (oldVelocity.X != Projectile.velocity.X) 
            {
				if (Math.Abs(oldVelocity.X) > 4f) 
                {
					shouldMakeSound = true;
				}
				Projectile.position.X += Projectile.velocity.X;
				Projectile.velocity.X = -oldVelocity.X * 0.2f;
			}
			if (oldVelocity.Y != Projectile.velocity.Y)
            {
				if (Math.Abs(oldVelocity.Y) > 4f) 
                {
					shouldMakeSound = true;
				}
				Projectile.position.Y += Projectile.velocity.Y;
				Projectile.velocity.Y = -oldVelocity.Y * 0.2f;
			}
			Projectile.ai[0] = 1f;
			if (shouldMakeSound) 
            {
				Projectile.netUpdate = true;
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			}
			return false;
		}
        Texture2D chainTexture;
		public override bool PreDraw(ref Color lightColor)
		{
			
			Vector2 mountedCenter = player.MountedCenter;
			SetSprite("");
			var drawPosition = Projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;
			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;
			if (Projectile.alpha == 0) 
            {
				int direction = -1;
				if (Projectile.Center.X < mountedCenter.X) direction = 1;
				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}
			while (true) 
            {
				float length = remainingVectorToPlayer.Length();
				if (length < 25f || float.IsNaN(length)) break;
				drawPosition += remainingVectorToPlayer * 12 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;
				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				Main.EntitySpriteDraw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			}
			return true;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(HitTimer == 0 && Projectile.ai[0] == 0f) 
			{
				Attack = true;
				Target = target;
			}
			base.OnHitNPC(target, damage, knockback, crit);
        }
    
	}
}