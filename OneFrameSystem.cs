using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.OneFrame
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(OneFrameSystem))]
    public sealed class OneFrameSystem : LateUpdateSystem
    {
        private Filter filter;
        private Filter filterAssumed;

        public override void OnAwake()
        {
            filter = World.Filter.With<OneFrame>();
            filterAssumed = World.Filter.With<OneFrameAssumed>().Without<OneFrame>();
        }

        public override unsafe void OnUpdate(float deltaTime)
        {
            foreach (var entity in filter)
            {
                var oneFrame = entity.GetComponent<OneFrame>();
                oneFrame.Remove(oneFrame.forEntity);
                entity.RemoveComponent<OneFrame>();
            }

            foreach (var entity in filterAssumed)
            {
                entity.AddComponent<OneFramePooled>();
                entity.RemoveComponent<OneFrameAssumed>();
                OneFramePool.Retrieve(entity);
            }
        }

        public override void Dispose() => OneFramePool.Dispose();
    }
}