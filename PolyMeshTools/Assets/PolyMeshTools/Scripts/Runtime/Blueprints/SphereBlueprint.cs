using eviltwo.PolyMeshTools.BlueprintModules;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Blueprints
{
    [CreateAssetMenu(fileName = "Sphere", menuName = nameof(PolyMeshTools) + "/" + nameof(Blueprints) + "/" + "Sphere")]
    public class SphereBlueprint : PolyMeshBlueprint
    {
        public Vector2Int Segments = new Vector2Int(6, 3);

        private void OnValidate()
        {
            Segments.x = Mathf.Max(3, Segments.x);
            Segments.y = Mathf.Max(1, Segments.y);
        }

        public override void Write(IPolyWriter writer)
        {
            var offsetX = (1f / Segments.x) / 2;
            for (var segY = 0; segY < Segments.y + 1; segY++)
            {
                var yt0 = (float)segY / (Segments.y + 1);
                var yt1 = (float)(segY + 1) / (Segments.y + 1);
                var y0 = Mathf.Cos(yt0 * Mathf.PI);
                var y1 = Mathf.Cos(yt1 * Mathf.PI);
                var r0 = Mathf.Sin(yt0 * Mathf.PI);
                var r1 = Mathf.Sin(yt1 * Mathf.PI);
                for (var segX = 0; segX < Segments.x; segX++)
                {
                    var y0xt0 = (float)segX / Segments.x + segY * offsetX;
                    var y0xt1 = (float)(segX + 1) / Segments.x + segY * offsetX;
                    var y1xt0 = (float)segX / Segments.x + (segY + 1) * offsetX;
                    var y1xt1 = (float)(segX + 1) / Segments.x + (segY + 1) * offsetX;
                    var y0x0 = Mathf.Cos(y0xt0 * 2 * Mathf.PI);
                    var y0x1 = Mathf.Cos(y0xt1 * 2 * Mathf.PI);
                    var y0z0 = Mathf.Sin(y0xt0 * 2 * Mathf.PI);
                    var y0z1 = Mathf.Sin(y0xt1 * 2 * Mathf.PI);
                    var y1x0 = Mathf.Cos(y1xt0 * 2 * Mathf.PI);
                    var y1x1 = Mathf.Cos(y1xt1 * 2 * Mathf.PI);
                    var y1z0 = Mathf.Sin(y1xt0 * 2 * Mathf.PI);
                    var y1z1 = Mathf.Sin(y1xt1 * 2 * Mathf.PI);
                    if (r0 > 0)
                    {
                        var tri = new TriangleSequence();
                        tri.Push(new Vector3(y0x0 * r0, y0, y0z0 * r0), new Vector2(y0xt0, 1 - yt0));
                        tri.Push(new Vector3(y0x1 * r0, y0, y0z1 * r0), new Vector2(y0xt1, 1 - yt0));
                        tri.Push(new Vector3(y1x0 * r1, y1, y1z0 * r1), new Vector2(y1xt0, 1 - yt1));
                        tri.Write(writer);
                    }
                    if (r1 > 0)
                    {
                        var tri = new TriangleSequence();
                        tri.Push(new Vector3(y0x1 * r0, y0, y0z1 * r0), new Vector2(y0xt1, 1 - yt0));
                        tri.Push(new Vector3(y1x1 * r1, y1, y1z1 * r1), new Vector2(y1xt1, 1 - yt1));
                        tri.Push(new Vector3(y1x0 * r1, y1, y1z0 * r1), new Vector2(y1xt0, 1 - yt1));
                        tri.Write(writer);
                    }
                }
            }
        }
    }
}
