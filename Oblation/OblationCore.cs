using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Tiles;
using System.Collections.Generic;

namespace ElementMachine.Oblation
{
    public class OblationRecipe
    {
        public static Recipe CreateOblationRecipe(ModItem item, int stack = 1)
        {
            Recipe recipe = item.CreateRecipe(stack);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            recipe.AddCondition(NetworkText.Empty, r => {
                return (item as OblationCore).single && !MyPlayer.Oblations.Contains(recipe.createItem.ModItem.Name);
            });
            recipe.AddOnCraftCallback(delegate (Recipe recipe, Item item, List<Item> consumedItems) {
                if (!MyPlayer.Oblations.Contains(recipe.createItem.ModItem.Name) && (recipe.createItem.ModItem as OblationCore).single) MyPlayer.Oblations.Add(recipe.createItem.ModItem.Name);
            });
            return recipe;
        }
    }

    public abstract class OblationCore : ElementItem
    {
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {

            base.Update(ref gravity, ref maxFallSpeed);
        }
        public bool single = false;
        public virtual MyPlayer.AltarType RequestSacrifice()
        {
            return MyPlayer.AltarType.None;
        }
        public virtual void OnSacrifice(Player player)
        {
            if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) Main.NewText("成功献祭" + player.HeldItem.Name);
            else Main.NewText("successfully sacrifice" + player.HeldItem.Name);
        }
    }
    public abstract class BossCore : OblationCore
    {

    }
}