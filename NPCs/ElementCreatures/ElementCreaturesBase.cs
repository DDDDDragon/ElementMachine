using ElementMachine.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementMachine.NPCs.ElementCreatures
{
    public abstract class ElementCreaturesBase : ModNPC
    {
        public float ElementLevel;
        /// <summary>
        /// 1-Flame 2-Ice 3-Earth 4-Water 5-Nature
        /// </summary>
        public int Element;
        public int FlameReactionTimer = 0; public bool Flame = false;
        public int IceReactionTimer = 0; public bool Ice = false;
        public int EarthReactionTimer = 0; public bool Earth = false;
        public int WaterReactionTimer = 0; public bool Water = false;
        public virtual void OnCatch()
        {
            NPC.life = 0;
        }
        public override void ResetEffects()
        {
            if (Flame) FlameReactionTimer++; 
            if(FlameReactionTimer == 150)
            {
                Flame = false;
                FlameReactionTimer = 0;
            }
            if (Ice) IceReactionTimer++;
            if (IceReactionTimer == 150)
            {
                Ice = false;
                IceReactionTimer = 0;
            }
            base.ResetEffects();
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            ElementProj EProj = projectile.ModProjectile as ElementProj;
            if (EProj == null) return;
            Player player = Main.player[projectile.owner];
            if (EProj.Element == 1 && FlameReactionTimer == 0) OnHitByFlameProj(player, projectile, ref damage, ref knockback, ref crit);
            if (EProj.Element == 2 && IceReactionTimer == 0) OnHitByIceProj(player, projectile, ref damage, ref knockback, ref crit);
            
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(player, item, ref damage, ref knockback, ref crit);
            ElementItem EItem = item.ModItem as ElementItem;
            if (EItem == null) return;
            if (EItem.Element == 1 && FlameReactionTimer == 0) OnHitByFlameItem(player, item, ref damage, ref knockback, ref crit);
            if (EItem.Element == 2 && IceReactionTimer == 0) OnHitByIceItem(player, item, ref damage, ref knockback, ref crit);
            
        }
        public virtual void OnHitByIceProj(Player player, Projectile projectile, ref int damage, ref float knockback, ref bool crit)
        {
            ElementProj EProj = projectile.ModProjectile as ElementProj;
            Ice = true;
            float reactionStrength = 0;
            if (EProj.ElementLevel > ElementLevel) reactionStrength = (EProj.ElementLevel - ElementLevel) * EProj.ElementLevel / ElementLevel;//反应强度始终按照 (大 - 小) * 大 / 小 计算
            else reactionStrength = (ElementLevel - EProj.ElementLevel) * ElementLevel / EProj.ElementLevel;
            if (Element == 1)//火属性被冰属性攻击
            {
                if (EProj.ElementLevel > ElementLevel)
                {
                    damage = (int)(damage * (1 + reactionStrength));//弹幕元素浓度高就增加伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Azure, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "融化" : "Melt");
                }
                else
                {
                    if (reactionStrength >= 0.5f) damage = damage / 2;//NPC元素浓度高 如果反应强度高于0.5f 降低50%伤害
                    else damage = (int)(damage * (1 - reactionStrength));//如果反应强度低于0.5f 按照反应强度降低伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Red, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "衰减" : "Weaken");
                }
                //TODO：释放水属性冲击
            }
            if (Element == 3)//地属性被冰属性攻击
            {
                if (EProj.ElementLevel < ElementLevel)
                {
                    damage = (int)(damage * 0.9);//NPC元素浓度高 地属性被冰属性攻击固定减伤10% 不能暴击
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Brown, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "抵抗" : "Resist");
                }
                else
                {
                    //冻土 反应叫啥没想好
                }
                crit = false;//反正不能暴击
            }
            if (Element == 4)//水属性被冰属性攻击
            {
                if (EProj.ElementLevel > ElementLevel) damage = (int)((EProj.ElementLevel / 10 + 1) * damage);//水属性被冰属性攻击 冰属性浓度高增伤 且只根据冰属性浓度计算增伤
                NPC.AddBuff(ModContent.BuffType<lowerSpeed>(), 60 + (int)((EProj.ElementLevel - ElementLevel) * 10));//根据冰属性浓度和水属性浓度计算减速时间 基础时间1s
            }
            if (Element == 5)//自然属性被冰属性攻击
            {
                //持续扣血 冻伤 自然恢复能力减弱
            }
        }
        public virtual void OnHitByFlameProj(Player player, Projectile projectile, ref int damage, ref float knockback, ref bool crit)
        {
            ElementProj EProj = projectile.ModProjectile as ElementProj;
            Flame = true;
            float reactionStrength = 0;
            if (EProj.ElementLevel > ElementLevel) reactionStrength = (EProj.ElementLevel - ElementLevel) * EProj.ElementLevel / ElementLevel;//反应强度始终按照 (大 - 小) * 大 / 小 计算
            else reactionStrength = (ElementLevel - EProj.ElementLevel) * ElementLevel / EProj.ElementLevel;
            if (Element == 2)//冰属性被火属性攻击
            {
                if (EProj.ElementLevel > ElementLevel)
                {
                    damage = (int)(damage * (1 + reactionStrength));//弹幕元素浓度高就增加伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Red, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "融化" : "Melt");
                }
                else
                {
                    if (reactionStrength >= 0.5f) damage /= 2;//NPC元素浓度高 如果反应强度高于0.5f 降低50%伤害
                    else damage = (int)(damage * (1 - reactionStrength));//如果反应强度低于0.5f 按照反应强度降低伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Azure, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "衰减" : "Weaken");
                }
                //TODO：释放水属性冲击
            }
            if (Element == 3)//地属性被火属性攻击
            {
                if (EProj.ElementLevel > ElementLevel)//如果弹幕的元素浓度高
                {
                    if (reactionStrength >= 1.5f)
                    {
                        //过载 爆炸 减防
                    }
                    else if (reactionStrength >= 1f)
                    {
                        //熔化 (不是融化！) 减防
                    }
                }
                else//NPC元素浓度高
                {
                    damage = (int)(damage * 0.9);//NPC元素浓度高 地属性被火属性攻击固定减伤10% 不能暴击
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Brown, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "抵抗" : "Resist");
                }
            }
            if (Element == 4)//水属性被火属性攻击
            {

            }
        }
        public virtual void OnHitByIceItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ElementItem EItem = item.ModItem as ElementItem;
            Ice = true;
            float reactionStrength = 0;
            if (EItem.ElementLevel > ElementLevel) reactionStrength = (EItem.ElementLevel - ElementLevel) * EItem.ElementLevel / ElementLevel;//反应强度始终按照 (大 - 小) * 大 / 小 计算
            else reactionStrength = (ElementLevel - EItem.ElementLevel) * ElementLevel / EItem.ElementLevel;
            if (Element == 1)//火属性被冰属性攻击
            {
                if (EItem.ElementLevel > ElementLevel)
                {
                    damage = (int)(damage * (1 + reactionStrength));//弹幕元素浓度高就增加伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Azure, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "融化" : "Melt");
                }
                else
                {
                    if (reactionStrength >= 0.5f) damage = damage / 2;//NPC元素浓度高 如果反应强度高于0.5f 降低50%伤害
                    else damage = (int)(damage * (1 - reactionStrength));//如果反应强度低于0.5f 按照反应强度降低伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Red, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "衰减" : "Weaken");
                }
                //TODO：释放水属性冲击
            }
            if (Element == 3)//地属性被冰属性攻击
            {
                if (EItem.ElementLevel < ElementLevel)
                {
                    damage = (int)(damage * 0.9);//NPC元素浓度高 地属性被冰属性攻击固定减伤10% 不能暴击
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Brown, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "抵抗" : "Resist");
                }
                else
                {
                    //冻土 反应叫啥没想好
                }
                crit = false;//反正不能暴击
            }
            if (Element == 4)//水属性被冰属性攻击
            {
                if (EItem.ElementLevel > ElementLevel) damage = (int)((EItem.ElementLevel / 10 + 1) * damage);//水属性被冰属性攻击 冰属性浓度高增伤 且只根据冰属性浓度计算增伤
                NPC.AddBuff(ModContent.BuffType<lowerSpeed>(), 60 + (int)((EItem.ElementLevel - ElementLevel) * 10));//根据冰属性浓度和水属性浓度计算减速时间 基础时间1s
            }
            if (Element == 5)//自然属性被冰属性攻击
            {
                //持续扣血 冻伤 自然恢复能力减弱
            }
        }
        public virtual void OnHitByFlameItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ElementItem EItem = item.ModItem as ElementItem;
            Flame = true;
            float reactionStrength = 0;
            if (EItem.ElementLevel > ElementLevel) reactionStrength = (EItem.ElementLevel - ElementLevel) * EItem.ElementLevel / ElementLevel;//反应强度始终按照 (大 - 小) * 大 / 小 计算
            else reactionStrength = (ElementLevel - EItem.ElementLevel) * ElementLevel / EItem.ElementLevel;
            if(Element == 2)//冰属性被火属性攻击
            {
                if (EItem.ElementLevel > ElementLevel)
                {
                    damage = (int)(damage * (1 + reactionStrength));//弹幕元素浓度高就增加伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Red, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "融化" : "Melt");
                }
                else
                {
                    if (reactionStrength >= 0.5f) damage /= 2;//NPC元素浓度高 如果反应强度高于0.5f 降低50%伤害
                    else damage = (int)(damage * (1 - reactionStrength));//如果反应强度低于0.5f 按照反应强度降低伤害
                    CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, NPC.width, NPC.height), Color.Azure, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "衰减" : "Weaken");
                }
                //TODO：释放水属性冲击
            }
            if(Element == 3)//地属性被火属性攻击
            {
                if(EItem.ElementLevel > ElementLevel)//如果武器的元素浓度高
                {
                    if(reactionStrength >= 1.5f)
                    {
                        //过载 爆炸 减防
                    }
                    else if(reactionStrength >= 1f)
                    {
                        //熔化 (不是融化！) 减防
                    }
                }
                else//NPC元素浓度高
                {
                    //伤害减免
                }
            }
            if(Element == 4)//水属性被火属性攻击
            {

            }
        }
    }
}
