using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public abstract class PolyMeshBlueprint : ScriptableObject
    {
        public Vector3 Position = Vector3.zero;

        public Vector3 Rotation = Vector3.zero;

        public Vector3 Scale = Vector3.one;

        public abstract void Write(IPolyWriter writer);
    }
}
