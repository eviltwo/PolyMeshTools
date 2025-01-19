using System.Collections.Generic;
using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public static class PolyMeshGenerator
    {
        public static Mesh GenerateMesh(PolyMeshBlueprint blueprint)
        {
            var mesh = new Mesh();
            GenerateMesh(blueprint, mesh);
            return mesh;
        }

        public static void GenerateMesh(PolyMeshBlueprint blueprint, Mesh result)
        {
            var matrix = Matrix4x4.TRS(blueprint.Transform.Position, Quaternion.Euler(blueprint.Transform.Rotation), blueprint.Transform.Scale);
            var builder = new TriangleMeshDataBuilder(matrix);
            blueprint.Write(builder);
            builder.ApplyToMesh(result);
            result.name = blueprint.name;
            result.RecalculateNormals();
            result.RecalculateBounds();
        }

        public class TriangleMeshDataBuilder : IPolyWriter
        {
            private readonly Matrix4x4 _matrix;

            private readonly List<Vector3> _vertices = new List<Vector3>();
            public IReadOnlyList<Vector3> Vertices => _vertices;

            private readonly List<Vector2> _uvs = new List<Vector2>();
            public IReadOnlyList<Vector2> UVs => _uvs;

            private readonly List<Color> _colors = new List<Color>();
            public IReadOnlyList<Color> Colors => _colors;

            private readonly List<int> _indices = new List<int>();
            public IReadOnlyList<int> Indices => _indices;

            public TriangleMeshDataBuilder(Matrix4x4 matrix)
            {
                _matrix = matrix;
            }

            public void WriteTriangle(Triangle triangle, bool mergeDuplicates = true)
            {
                WriteTriangle(triangle.V0, triangle.V1, triangle.V2, triangle.UV0, triangle.UV1, triangle.UV2, triangle.C0, triangle.C1, triangle.C2, mergeDuplicates);
            }

            public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, bool mergeDuplicates = true)
            {
                WriteTriangle(v0, v1, v2, uv0, uv1, uv2, Color.white, Color.white, Color.white, mergeDuplicates);
            }

            public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, Color c0, Color c1, Color c2, bool mergeDuplicates = true)
            {
                var i0 = AddVertex(v0, uv0, c0, mergeDuplicates);
                var i1 = AddVertex(v1, uv1, c1, mergeDuplicates);
                var i2 = AddVertex(v2, uv2, c2, mergeDuplicates);
                _indices.Add(i0);
                _indices.Add(i1);
                _indices.Add(i2);
            }

            private int AddVertex(Vector3 v, Vector2 uv, Color c, bool mergeDuplicates)
            {
                v = _matrix.MultiplyPoint(v);
                if (mergeDuplicates)
                {
                    for (var i = 0; i < _vertices.Count; i++)
                    {
                        if (_vertices[i] == v && _uvs[i] == uv && _colors[i] == c)
                        {
                            return i;
                        }
                    }
                }

                _vertices.Add(v);
                _uvs.Add(uv);
                _colors.Add(c);
                return _vertices.Count - 1;
            }

            public void ApplyToMesh(Mesh mesh)
            {
                mesh.Clear();
                mesh.SetVertices(_vertices);
                mesh.SetUVs(0, _uvs);
                mesh.SetColors(_colors);
                mesh.SetIndices(_indices, MeshTopology.Triangles, 0);
            }
        }
    }
}
