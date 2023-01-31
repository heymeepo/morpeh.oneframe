using System;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.OneFrame
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    internal unsafe struct OneFrame : IComponent
    {
        public Entity forEntity;
        [NonSerialized] public delegate* managed<Entity, void> Remove;
    }
}