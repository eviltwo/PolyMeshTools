using System.Collections.Generic;
using UnityEngine;

namespace eviltwo.PolyMeshTools
{
    public static class PolyMeshGenerator
    {
        public static Mesh GenerateMesh(IPolyMeshBlueprint blueprint, bool includeColor)
        {
            var mesh = new Mesh();
            GenerateMesh(blueprint, mesh, includeColor);
            return mesh;
        }

        public static void GenerateMesh(IPolyMeshBlueprint blueprint, Mesh result, bool includeColor)
        {
            var transform = blueprint.GetTransform();
            var matrix = Matrix4x4.TRS(transform.Position, Quaternion.Euler(transform.Rotation), transform.Scale);
            var builder = new TriangleMeshDataBuilder(matrix);
            blueprint.Write(builder);
            builder.ApplyToMesh(result, includeColor);
            result.name = blueprint.GetName();
            result.RecalculateNormals();
            result.RecalculateTangents();
            result.RecalculateBounds();
        }

        private class TriangleMeshDataBuilder : IPolyWriter
        {
            private readonly Matrix4x4 _matrix;

            private readonly List<Vector3> _vertices = new List<Vector3>();
            public IReadOnlyList<Vector3> Vertices => _vertices;

            private readonly List<Vector2> _uvs = new List<Vector2>();
            public IReadOnlyList<Vector2> UVs => _uvs;

            private readonly List<Color32> _colors = new List<Color32>();
            public IReadOnlyList<Color32> Colors => _colors;

            private readonly List<int> _indices = new List<int>();
            public IReadOnlyList<int> Indices => _indices;

            public TriangleMeshDataBuilder(Matrix4x4 matrix)
            {
                _matrix = matrix;
            }

            public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, bool mergeDuplicates = true)
            {
                WriteTriangle(v0, v1, v2, uv0, uv1, uv2, Color.white, Color.white, Color.white, mergeDuplicates);
            }

            public void WriteTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2, Color32 c0, Color32 c1, Color32 c2, bool mergeDuplicates = true)
            {
                var i0 = AddVertex(v0, uv0, c0, mergeDuplicates);
                var i1 = AddVertex(v1, uv1, c1, mergeDuplicates);
                var i2 = AddVertex(v2, uv2, c2, mergeDuplicates);
                _indices.Add(i0);
                _indices.Add(i1);
                _indices.Add(i2);
            }

            private int AddVertex(Vector3 v, Vector2 uv, Color32 c, bool mergeDuplicates)
            {
                v = _matrix.MultiplyPoint(v);
                if (mergeDuplicates)
                {
                    for (var i = 0; i < _vertices.Count; i++)
                    {
                        if (_vertices[i] == v && _uvs[i] == uv && _colors[i].Equals(c))
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

            public void ApplyToMesh(Mesh mesh, bool includeColor)
            {
                mesh.Clear(false);
                mesh.SetVertices(_vertices);
                mesh.SetUVs(0, _uvs);

                if (includeColor)
                {
                    mesh.SetColors(_colors);
                }

                mesh.SetIndices(_indices, MeshTopology.Triangles, 0);
            }
        }
    }
}
