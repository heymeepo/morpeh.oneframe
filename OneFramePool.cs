using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scellecs.Morpeh.OneFrame
{
    internal static class OneFramePool
    {
        private static Stack<Entity>[] pool = Array.Empty<Stack<Entity>>();

        internal static void InitializeWorld(int worldID)
        {
            if (pool.Length >= worldID + 1)
            {
                DisposeWorldPool(worldID);
            }
            else
            {
                Array.Resize(ref pool, worldID + 1);
            }

            pool[worldID] = new Stack<Entity>();
        }

        internal static Entity Assume(World world)
        {
            var worldPool = pool[world.identifier];

            if (worldPool.Count > 0)
            {
                var entity = worldPool.Pop();
                entity.AddComponent<OneFrameAssumed>();
                return entity;
            }
            else
            {
                var oneframe = world.CreateEntity();
                oneframe.AddComponent<OneFramePooled>();
                oneframe.AddComponent<OneFrameAssumed>();
                return oneframe;
            }
        }

        internal static void Retrieve(Entity entity) => pool[entity.worldID].Push(entity);

        internal static void DisposeWorldPool(int worldID)
        {
            var worldPool = pool[worldID];

            if (worldPool != null)
            {
                while (worldPool.Count > 0)
                {
                    worldPool.Pop().RemoveComponent<OneFramePooled>();
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Dispose()
        {
            foreach (var worldPool in pool)
            {
                if (worldPool != null)
                {
                    while (worldPool.Count > 0)
                    {
                        worldPool.Pop().RemoveComponent<OneFramePooled>();
                    }
                }
            }
        }
    }
}
