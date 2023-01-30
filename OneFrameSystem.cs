namespace Scellecs.Morpeh.OneFrame
{
    public sealed class OneFrameSystem : ICleanupSystem
    {
        private Filter filter;
        private Filter filterAssumed;

        public World World { get; set; }

        public void OnAwake()
        {
            filter = World.Filter.With<OneFrame>();
            filterAssumed = World.Filter.With<OneFrameAssumed>().Without<OneFrame>();
        }

        public unsafe void OnUpdate(float deltaTime)
        {
            foreach (var entity in filter)
            {
                ref var oneFrame = ref entity.GetComponent<OneFrame>();
                oneFrame.Remove(oneFrame.forEntity);
                entity.RemoveComponent<OneFrame>();
            }

            foreach (var entity in filterAssumed)
            {
                entity.RemoveComponent<OneFrameAssumed>();
                OneFramePool.Retrieve(entity);
            }
        }

        public void Dispose() => OneFramePool.DisposeWorldPool(World.identifier);
    }
}