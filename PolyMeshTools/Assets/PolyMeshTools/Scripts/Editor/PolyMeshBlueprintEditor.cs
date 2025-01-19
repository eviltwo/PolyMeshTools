using UnityEditor;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Editor
{
    [CustomEditor(typeof(PolyMeshBlueprint), true)]
    public class PolyMeshBlueprintEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Mesh Asset"))
            {
                PolyMeshBlueprint blueprint = (PolyMeshBlueprint)target;
                PolyMeshAssetGenerator.GenerateMeshAsset(blueprint);
            }
        }
    }
}
