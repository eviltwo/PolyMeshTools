using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public interface IPolyWriter
    {
        public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, bool mergeDuplicates = true);

        public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, Color c0, Color c1, Color c2, bool mergeDuplicates = true);
    }
}
