namespace Scellecs.Morpeh.OneFrame
{
    public sealed class OneFrameSystem : ICleanupSystem
    {
        private Filter filter;
        private Filter filterAssumed;

        private Stash<OneFrame> oneFrameStash;
        private Stash<OneFrameAssumed> assumedStash;

        public World World { get; set; }

        public void OnAwake()
        {
            filter = World.Filter.With<OneFrame>();
            filterAssumed = World.Filter.With<OneFrameAssumed>().Without<OneFrame>();

            oneFrameStash = World.GetStash<OneFrame>();
            assumedStash = World.GetStash<OneFrameAssumed>();
        }

        public unsafe void OnUpdate(float deltaTime)
        {
            foreach (var entity in filter)
            {
                ref var oneFrame = ref oneFrameStash.Get(entity);

                if (oneFrame.forEntity.IsNullOrDisposed() == false)
                {
                    oneFrame.Remove(oneFrame.forEntity);
                }
                oneFrameStash.Remove(entity);
            }

            foreach (var entity in filterAssumed)
            {
                assumedStash.Remove(entity);
                OneFramePool.Retrieve(entity);
            }
        }

        public void Dispose() => OneFramePool.DisposeWorldPool(World.identifier);
    }
}