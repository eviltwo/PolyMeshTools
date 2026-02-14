using System;
using UnityEngine;

namespace eviltwo.PolyMeshTools.RuntimeGenerators
{
    public abstract class PolyMeshRuntimeGenerator : MonoBehaviour, IPolyMeshBlueprint
    {
        public MeshFilter MeshFilter;
        
        public MeshCollider MeshCollider;
        
        public string GetName() => name;
        
        public PolyMeshTransform MeshTransform = new ()
        {
            Position = Vector3.zero,
            Rotation = Vector3.zero,
            Scale = Vector3.one
        };

        public PolyMeshTransform GetTransform() => MeshTransform;
        
        public bool IncludeColor = false;

        private void Reset()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshCollider = GetComponent<MeshCollider>();
        }

#if UNITY_EDITOR
        public void OnValueChangedForEditor()
        {
            Generate();
        }
#endif

        public void Generate()
        {
            var hasComponent = false;
            Mesh mesh = null;
            if (MeshFilter != null)
            {
                hasComponent = true;
                if (MeshFilter.sharedMesh != null && MeshFilter.sharedMesh.isReadable)
                {
                    mesh = MeshFilter.sharedMesh;
                }
            }

            if (MeshCollider != null)
            {
                hasComponent = true;
                if (mesh == null && MeshCollider.sharedMesh != null && MeshCollider.sharedMesh.isReadable)
                {
                    mesh = MeshCollider.sharedMesh;
                }
            }
            
            if (!hasComponent) return;
            
            if (mesh == null)
            {
                mesh = new Mesh();
            }
            
            PolyMeshGenerator.GenerateMesh(this, mesh, IncludeColor);
            
            if (MeshFilter != null) MeshFilter.sharedMesh = mesh;
            if (MeshCollider != null) MeshCollider.sharedMesh = mesh;
        }

        public abstract void Write(IPolyWriter writer);
    }
}
