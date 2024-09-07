using Assets.Scripts.Gameplay.PlayerSystem;
using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Mono;

namespace Assets.Scripts.Entities.Items
{
    internal abstract class BaseUsableItem : Item
    {
        public abstract bool Use(PlayerEntity player);

        protected Container GetContainer(PlayerEntity player)
        {
            return player.GetComponentInChildren<ContainerMono>()?.GetContainer();
        }

        protected void RemoveFromContainer(PlayerEntity player)
        {
            GetContainer(player)?.RemoveItem(this);
        }
    }
}
