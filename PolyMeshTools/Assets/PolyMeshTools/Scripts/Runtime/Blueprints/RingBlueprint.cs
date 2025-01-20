using eviltwo.PolyMeshTools.BlueprintModules;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Blueprints
{
    [CreateAssetMenu(fileName = "Ring", menuName = nameof(PolyMeshTools) + "/" + nameof(Blueprints) + "/" + "Ring")]
    public class RingBlueprint : PolyMeshBlueprint
    {
        public float InnerRadius = 0.5f;
        public float OuterRadius = 1.0f;
        public float InnerOffset = 0;
        public float OuterOffset = 0;
        public float StartAngle = 0;
        public float EndAngle = 360;
        public int Segments = 32;
        public Color Color = Color.white;

        private void OnValidate()
        {
            InnerRadius = Mathf.Max(0.01f, InnerRadius);
            OuterRadius = Mathf.Max(0.01f, OuterRadius);
            Segments = Mathf.Max(2, Segments);
        }

        public override void Write(IPolyWriter writer)
        {
            for (var i = 0; i < Segments - 1; i++)
            {
                var t0 = (float)i / (Segments - 1);
                var t1 = (float)(i + 1) / (Segments - 1);
                var angle0 = Mathf.Lerp(StartAngle, EndAngle, t0);
                var angle1 = Mathf.Lerp(StartAngle, EndAngle, t1);
                var inner0 = new Vector3(Mathf.Sin(angle0 * Mathf.Deg2Rad) * InnerRadius, Mathf.Cos(angle0 * Mathf.Deg2Rad) * InnerRadius, InnerOffset);
                var inner1 = new Vector3(Mathf.Sin(angle1 * Mathf.Deg2Rad) * InnerRadius, Mathf.Cos(angle1 * Mathf.Deg2Rad) * InnerRadius, InnerOffset);
                var outer0 = new Vector3(Mathf.Sin(angle0 * Mathf.Deg2Rad) * OuterRadius, Mathf.Cos(angle0 * Mathf.Deg2Rad) * OuterRadius, OuterOffset);
                var outer1 = new Vector3(Mathf.Sin(angle1 * Mathf.Deg2Rad) * OuterRadius, Mathf.Cos(angle1 * Mathf.Deg2Rad) * OuterRadius, OuterOffset);
                var quad = new QuadSequence();
                quad.Push(inner0, new Vector2(t0, 0), Color);
                quad.Push(outer0, new Vector2(t0, 1), Color);
                quad.Push(outer1, new Vector2(t1, 1), Color);
                quad.Push(inner1, new Vector2(t1, 0), Color);
                quad.Write(writer);
            }
        }
    }
}
