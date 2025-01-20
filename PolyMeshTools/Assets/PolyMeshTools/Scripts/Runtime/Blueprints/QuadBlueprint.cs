using eviltwo.PolyMeshTools.BlueprintModules;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Blueprints
{
    [CreateAssetMenu(fileName = "Quad", menuName = nameof(PolyMeshTools) + "/" + nameof(Blueprints) + "/" + "Quad")]
    public class QuadBlueprint : PolyMeshBlueprint
    {
        public Color Color = Color.white;

        public override void Write(IPolyWriter writer)
        {
            var quad = new QuadSequence();
            quad.Push(new Vector3(-0.5f, -0.5f, 0.0f), new Vector2(0, 0), Color);
            quad.Push(new Vector3(-0.5f, 0.5f, 0.0f), new Vector2(0, 1), Color);
            quad.Push(new Vector3(0.5f, 0.5f, 0.0f), new Vector2(1, 1), Color);
            quad.Push(new Vector3(0.5f, -0.5f, 0.0f), new Vector2(1, 0), Color);
            quad.Write(writer);
        }
    }
}
