using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public class Triangle
    {
        public Vector3 V0;
        public Vector3 V1;
        public Vector3 V2;
        public Vector2 UV0;
        public Vector2 UV1;
        public Vector2 UV2;
        public Color C0;
        public Color C1;
        public Color C2;

        private int _index;

        public void Push(Vector3 v, Vector2 uv)
        {
            Push(v, uv, Color.white);
        }

        public void Push(Vector3 v, Vector2 uv, Color c)
        {
            switch (_index)
            {
                case 0:
                    V0 = v;
                    UV0 = uv;
                    C0 = c;
                    break;
                case 1:
                    V1 = v;
                    UV1 = uv;
                    C1 = c;
                    break;
                case 2:
                    V2 = v;
                    UV2 = uv;
                    C2 = c;
                    break;
                default:
                    Debug.LogError("Triangle.Push: too many vertices");
                    return;
            }
            _index++;
        }
    }
}
