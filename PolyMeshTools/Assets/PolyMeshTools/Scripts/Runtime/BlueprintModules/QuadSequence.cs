using UnityEngine;

namespace eviltwo.PolyMeshTools.BlueprintModules
{
    public class QuadSequence
    {
        private Vector3[] _vertices = new Vector3[4];
        private Vector2[] _uvs = new Vector2[4];
        private Color[] _colors = new Color[4];

        private int _pushedCount;
        public int PushedCount => _pushedCount;

        public void Clear()
        {
            _pushedCount = 0;
        }

        public void Push(Vector3 v, Vector2 uv)
        {
            Push(v, uv, Color.white);
        }

        public void Push(Vector3 v, Vector2 uv, Color c)
        {
            if (_pushedCount >= 4)
            {
                Debug.LogError("QuadSequence.Push: too many vertices");
                return;
            }

            _vertices[_pushedCount] = v;
            _uvs[_pushedCount] = uv;
            _colors[_pushedCount] = c;

            _pushedCount++;
        }

        public void Write(IPolyWriter writer)
        {
            if (_pushedCount != 4)
            {
                Debug.LogError($"QuadSequence.Write: expected 4 vertices, got {_pushedCount}");
                return;
            }

            writer.WriteTriangle(_vertices[0], _vertices[1], _vertices[2], _uvs[0], _uvs[1], _uvs[2], _colors[0], _colors[1], _colors[2]);
            writer.WriteTriangle(_vertices[0], _vertices[2], _vertices[3], _uvs[0], _uvs[2], _uvs[3], _colors[0], _colors[2], _colors[3]);
        }
    }
}
