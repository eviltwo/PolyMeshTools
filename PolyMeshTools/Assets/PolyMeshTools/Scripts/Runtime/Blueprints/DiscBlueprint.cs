using eviltwo.PolyMeshTools.BlueprintModules;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Blueprints
{
    [CreateAssetMenu(fileName = "Disc", menuName = nameof(PolyMeshTools) + "/" + nameof(Blueprints) + "/" + "Disc")]
    public class DiscBlueprint : PolyMeshBlueprint
    {
        public float Radius = 1;
        public float StartAngle = 0;
        public float EndAngle = 360;
        public int Segments = 32;
        public Color Color = Color.white;

        private void OnValidate()
        {
            Radius = Mathf.Max(0.01f, Radius);
            Segments = Mathf.Max(2, Segments);
        }

        public override void Write(IPolyWriter writer)
        {
            var center = Vector3.zero;
            for (var i = 0; i < Segments - 1; i++)
            {
                var t0 = (float)i / (Segments - 1);
                var t1 = (float)(i + 1) / (Segments - 1);
                var angle0 = Mathf.Lerp(StartAngle, EndAngle, t0);
                var angle1 = Mathf.Lerp(StartAngle, EndAngle, t1);
                var edge0 = new Vector3(Mathf.Sin(angle0 * Mathf.Deg2Rad) * Radius, Mathf.Cos(angle0 * Mathf.Deg2Rad) * Radius, 0);
                var edge1 = new Vector3(Mathf.Sin(angle1 * Mathf.Deg2Rad) * Radius, Mathf.Cos(angle1 * Mathf.Deg2Rad) * Radius, 0);
                var triangle = new TriangleSequence();
                triangle.Push(center, new Vector2((t0 + t1) * 0.5f, 0), Color);
                triangle.Push(edge0, new Vector2(t0, 1), Color);
                triangle.Push(edge1, new Vector2(t1, 1), Color);
                triangle.Write(writer);
            }
        }
    }
}
