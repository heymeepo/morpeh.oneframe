using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.OneFrame
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    internal struct OneFramePooled : IComponent { }
}