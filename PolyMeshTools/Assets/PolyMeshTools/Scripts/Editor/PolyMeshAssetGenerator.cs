using System.IO;
using UnityEditor;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Editor
{
    public static class PolyMeshAssetGenerator
    {
        public static void GenerateMeshAsset(PolyMeshBlueprint blueprint, bool includeColor)
        {
            var blueprintPath = AssetDatabase.GetAssetPath(blueprint);
            var directory = Path.GetDirectoryName(blueprintPath);
            var path = Path.Combine(directory, $"{blueprint.name}_mesh.asset");
            GenerateMeshAsset(blueprint, includeColor, path);
        }

        public static void GenerateMeshAsset(PolyMeshBlueprint blueprint, bool includeColor, string assetPath)
        {
            // Load or create mesh
            var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);
            var overwrite = mesh != null;
            if (!overwrite)
            {
                mesh = new Mesh();
            }

            // Generate mesh
            PolyMeshGenerator.GenerateMesh(blueprint, mesh, includeColor);
            mesh.name = Path.GetFileNameWithoutExtension(assetPath);

            // Save asset
            if (overwrite)
            {
                EditorUtility.SetDirty(mesh);
            }
            else
            {
                AssetDatabase.CreateAsset(mesh, assetPath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
