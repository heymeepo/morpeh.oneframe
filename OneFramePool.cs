using System.Collections.Generic;
using UnityEngine;

namespace Scellecs.Morpeh.OneFrame
{
    internal static class OneFramePool
    {
        private static Stack<Entity> pooledEntities = new Stack<Entity>();

        internal static Entity Assume()
        {
            if (pooledEntities.Count > 0)
            {
                var entity = pooledEntities.Pop();
                entity.AddComponent<OneFrameAssumed>();
                return entity;
            }
            else
            {
                var oneframe = World.Default.CreateEntity();
                oneframe.AddComponent<OneFramePooled>();
                oneframe.AddComponent<OneFrameAssumed>();
                return oneframe;
            }
        }

        internal static void Retrieve(Entity entity) => pooledEntities.Push(entity);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        internal static void Dispose()
        {
            while (pooledEntities.Count > 0)
            {
                pooledEntities.Pop().RemoveComponent<OneFramePooled>();
            }
        }
    }
}