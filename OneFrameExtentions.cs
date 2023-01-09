namespace Scellecs.Morpeh.OneFrame
{
    public static class OneFrameExtentions
    {
        public static void OneFrame<T>(this World world) where T : struct, IComponent
        {
            var entity = OneFramePool.Assume();

            entity.SetComponent(new T());
            entity.SetComponent(new OneFrame() 
            {
                forEntity = entity,
                Remove = &RemoveOneFrame<T> 
            });
        }

        public static void OneFrame<T>(this World world, in T component) where T : struct, IComponent
        {
            var entity = OneFramePool.Assume();

            entity.SetComponent(component);
            entity.SetComponent(new OneFrame()
            {
                forEntity = entity,
                Remove = &RemoveOneFrame<T>
            });
        }

        public static void OneFrame<T>(this Entity entity) where T : struct, IComponent
        {
            var oneFrameEntity = OneFramePool.Assume();

            entity.SetComponent(new T());
            oneFrameEntity.SetComponent(new OneFrame()
            {
                forEntity = entity,
                Remove = &RemoveOneFrame<T>
            });
        }

        public static void OneFrame<T>(this Entity entity, in T component) where T : struct, IComponent
        {
            var oneFrameEntity = OneFramePool.Assume();

            entity.SetComponent(component);
            oneFrameEntity.SetComponent(new OneFrame()
            {
                forEntity = entity,
                Remove = &RemoveOneFrame<T>
            });
        }

        public static void ReleaseOneFrame(this Entity entity)
        {
            if (entity.Has<OneFrameAssumed>())
            {
                entity.RemoveComponent<OneFrameAssumed>();
                entity.RemoveComponent<OneFramePooled>();
            }
        }

        private static void RemoveOneFrame<T>(Entity entity) where T : struct, IComponent => entity.RemoveComponent<T>();
    }
}
