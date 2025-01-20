using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public abstract class PolyMeshBlueprint : ScriptableObject, IPolyMeshBlueprint
    {
        public PolyMeshTransform Transform = new PolyMeshTransform
        {
            Position = Vector3.zero,
            Rotation = Vector3.zero,
            Scale = Vector3.one
        };

        public string GetName() => name;

        public PolyMeshTransform GetTransform() => Transform;

        public abstract void Write(IPolyWriter writer);
    }
}
