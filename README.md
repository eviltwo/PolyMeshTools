# PolyMeshTools
This is a tool that simplifies mesh generation in Unity. By writing triangles sequentially in a script, you can generate a Mesh. It is compatible with both runtime and the editor (.asset).

# Install with UPM
```
https://github.com/eviltwo/PolyMeshTools.git?path=PolyMeshTools/Assets/PolyMeshTools
```

# Usage  
## Implement a ScriptableAsset class
that inherits from PolyMeshBlueprint. Add as many triangles as needed in the Write(IPolyWriter writer) method.
https://github.com/eviltwo/PolyMeshTools/blob/c63a3ada6737ebd21b79add1e9dcfcbb964892b0/PolyMeshTools/Assets/PolyMeshTools/Scripts/Runtime/Blueprints/TriangleBlueprint.cs#L18-L25

## Create a ScriptableAsset instance
![image](https://github.com/user-attachments/assets/0eb5c0cd-c85a-4cad-af1d-b6fc240a8c43)


## Press the "Generate Mesh Asset"
button on the blueprint to generate a Mesh.

![image](https://github.com/user-attachments/assets/c556e846-c1ff-4ddb-a183-dc941acead9f)

![image](https://github.com/user-attachments/assets/35f25f7d-0804-454a-98dc-0ae1611b557d)


# Support My Work
As a solo developer, your financial support would be greatly appreciated and helps me continue working on this project.
- [Asset Store](https://assetstore.unity.com/publishers/12117)
- [GitHub Sponsors](https://github.com/sponsors/eviltwo)
