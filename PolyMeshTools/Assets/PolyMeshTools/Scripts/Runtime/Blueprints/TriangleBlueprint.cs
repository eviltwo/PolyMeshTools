using UnityEngine;

namespace eviltwo.PolyMeshTools.Blueprints
{
    [CreateAssetMenu(fileName = nameof(TriangleBlueprint), menuName = nameof(PolyMeshTools) + "/" + nameof(Blueprints) + "/" + nameof(TriangleBlueprint))]
    public class TriangleBlueprint : PolyMeshBlueprint
    {
        public Vector3 P0 = new Vector3(0, 1, 0);
        public Vector3 P1 = new Vector3(0.866f, -0.5f, 0);
        public Vector3 P2 = new Vector3(-0.866f, -0.5f, 0);
        public Vector2 UV0 = new Vector2(0, 1);
        public Vector2 UV1 = new Vector2(1, 0);
        public Vector2 UV2 = new Vector2(0, 0);
        public Color C0 = Color.white;
        public Color C1 = Color.white;
        public Color C2 = Color.white;

        public override void Write(IPolyWriter writer)
        {
            var triangle = new Triangle();
            triangle.Push(P0, UV0, C0);
            triangle.Push(P1, UV1, C1);
            triangle.Push(P2, UV2, C2);
            writer.WriteTriangle(triangle);
        }
    }
}
