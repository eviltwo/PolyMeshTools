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

        private GUIContent _previewTitle;
        private Mesh _previewMesh;
        private MeshPreview _meshPreview;
        public void OnEnable()
        {
            _previewTitle = new GUIContent(target.GetType().Name);
            _previewMesh = new Mesh();
            _meshPreview = new MeshPreview(_previewMesh);
        }

        public void OnDisable()
        {
            DestroyImmediate(_previewMesh);
            _previewMesh = null;
            _meshPreview.Dispose();
            _meshPreview = null;
        }

        public override bool HasPreviewGUI() => true;

        public override GUIContent GetPreviewTitle() => _previewTitle;

        public override void OnPreviewSettings()
        {
            _meshPreview.OnPreviewSettings();
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            PolyMeshBlueprint blueprint = (PolyMeshBlueprint)target;
            PolyMeshGenerator.GenerateMesh(blueprint, _previewMesh);
            _meshPreview.OnPreviewGUI(r, background);
        }
    }
}
