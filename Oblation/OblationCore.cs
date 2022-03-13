using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Oblation
{
    public class OblationRecipe : ModRecipe
    {
        public OblationRecipe(Mod mod) : base(mod)
        {

        }
        public bool Single = false;
        public override void OnCraft(Item item)
        {
            if(!MyPlayer.Oblations.Contains(this.createItem.modItem.Name)) MyPlayer.Oblations.Add(this.createItem.modItem.Name);
            base.OnCraft(item);
        }
        public override bool RecipeAvailable()
        {
            if(Single) return !MyPlayer.Oblations.Contains(this.createItem.modItem.Name);
            return base.RecipeAvailable();
        }
    }

    public abstract class OblationCore : ElementItem
    {
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {

            base.Update(ref gravity, ref maxFallSpeed);
        }
        public bool single = false;
    }
}