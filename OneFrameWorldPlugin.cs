using UnityEngine.Scripting;

namespace Scellecs.Morpeh.OneFrame
{
    [Preserve]
    internal sealed class OneFrameWorldPlugin : IWorldPlugin
    {
        [Preserve]
        public void Initialize(World world)
        {
            OneFramePool.InitializeWorld(world.identifier);
            var sysGroup = world.CreateSystemsGroup();
            sysGroup.AddSystem(new OneFrameSystem());
            world.AddPluginSystemsGroup(sysGroup);
        }

    }
}
