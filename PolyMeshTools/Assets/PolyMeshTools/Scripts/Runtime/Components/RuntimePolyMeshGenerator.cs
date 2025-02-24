using UnityEngine;

namespace eviltwo.PolyMeshTools.Components
{
    public class RuntimePolyMeshGenerator : MonoBehaviour
    {
        public PolyMeshBlueprint Blueprint = null;

        public MeshFilter MeshFilter = null;

        public bool IncludeColor = false;

        public bool GenerateOnAwake = true;

        [Header("Result")]
        public Mesh GeneratedMesh = null;

        private void Reset()
        {
            MeshFilter = GetComponent<MeshFilter>();
        }

        private void Awake()
        {
            if (GenerateOnAwake)
            {
                GenerateMesh();
                AttachMesh();
            }
        }

        public void GenerateMesh()
        {
            GeneratedMesh = PolyMeshGenerator.GenerateMesh(Blueprint, IncludeColor);
        }

        public void AttachMesh()
        {
            if (MeshFilter != null)
            {
                MeshFilter.sharedMesh = GeneratedMesh;
            }
        }
    }
}
