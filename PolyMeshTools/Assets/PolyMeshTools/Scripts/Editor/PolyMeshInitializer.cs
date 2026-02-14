using System.Collections.Generic;
using eviltwo.PolyMeshTools.RuntimeGenerators;
using UnityEditor;
using UnityEngine;

namespace eviltwo.PolyMeshTools.Editor
{
    [InitializeOnLoad]
    public static class PolyMeshInitializer
    {
        private static readonly List<PolyMeshRuntimeGenerator> generatorBuffer = new();

        static PolyMeshInitializer()
        {
            ObjectChangeEvents.changesPublished -= OnChangesPublished;
            ObjectChangeEvents.changesPublished += OnChangesPublished;
        }

        private static void OnChangesPublished(ref ObjectChangeEventStream stream)
        {
            for (var i = 0; i < stream.length; i++)
            {
                var eventType = stream.GetEventType(i);
                switch (eventType)
                {
                    case ObjectChangeKind.CreateGameObjectHierarchy:
                        stream.GetCreateGameObjectHierarchyEvent(i, out var createGameObjectHierarchyEvent);
                        var newGameObject = EditorUtility.InstanceIDToObject(createGameObjectHierarchyEvent.instanceId) as GameObject;
                        if (newGameObject != null)
                        {
                            newGameObject.GetComponentsInChildren(generatorBuffer);
                            foreach (var generator in generatorBuffer)
                            {
                                InitializeComponent(generator);
                            }
                        }

                        generatorBuffer.Clear();
                        break;
                }
            }
        }

        private static void InitializeComponent(PolyMeshRuntimeGenerator generator)
        {
            if (generator.MeshFilter != null) generator.MeshFilter.sharedMesh = null;
            if (generator.MeshCollider != null) generator.MeshCollider.sharedMesh = null;
            generator.Generate();
        }
    }
}
