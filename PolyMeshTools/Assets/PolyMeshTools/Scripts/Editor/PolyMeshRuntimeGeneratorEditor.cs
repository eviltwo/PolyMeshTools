using System;
using eviltwo.PolyMeshTools.RuntimeGenerators;
using UnityEditor;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Editor
{
    [CustomEditor(typeof(PolyMeshRuntimeGenerator), true)]
    [CanEditMultipleObjects]
    public class PolyMeshRuntimeGeneratorEditor : UnityEditor.Editor
    {
        private static bool AutoUpdate = true;
        
        private void OnEnable()
        {
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }
        
        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
        }

        public override void OnInspectorGUI()
        {
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (changeCheck.changed && AutoUpdate)
                {
                    CallGenerate();
                }
            }
            
            EditorGUILayout.Space();

            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                AutoUpdate = EditorGUILayout.Toggle("Auto Update", AutoUpdate);
                if (changeCheck.changed && AutoUpdate)
                {
                    CallGenerate();
                    EditorUtility.SetDirty(target);
                }
            }

            if (GUILayout.Button("Update Mesh"))
            {
                CallGenerate();
            }
        }

        private void OnUndoRedoPerformed()
        {
            if (AutoUpdate)
            {
                CallGenerate();
            }
        }

        private void CallGenerate()
        {
            if (target is PolyMeshRuntimeGenerator generator)
            {
                generator.OnValueChangedForEditor();
            }
        }
    }
}
