# PolyMeshTools
This is a tool to simplify Mesh generation in C#. You don't have to worry about the number of vertices or indices; you can just keep sending triangles.

[日本語の記事](https://note.com/daiki_all/n/n2c0a9317d16a)

# Install with UPM
```
https://github.com/eviltwo/PolyMeshTools.git?path=PolyMeshTools/Assets/PolyMeshTools
```

# Usage
## Implement a ScriptableAsset class
that inherits from PolyMeshBlueprint. Add as many triangles as needed in the Write(IPolyWriter writer) method.
https://github.com/eviltwo/PolyMeshTools/blob/995663ded59a573c477123282ca12b71f449d29a/PolyMeshTools/Assets/PolyMeshTools/Scripts/Runtime/Blueprints/DiscBlueprint.cs#L6-L39

## Create ScriptableAsset instance
![image](https://github.com/user-attachments/assets/f060524a-98ba-42f0-8723-e24e7401b032)

![image](https://github.com/user-attachments/assets/3977d0bf-f431-4fbb-9ebd-e1d84dfae204)

![image](https://github.com/user-attachments/assets/96cc8fc8-479f-4cf5-9c6c-f980c22b83c7)



## Press the "Generate Mesh Asset"
button on the blueprint to generate a Mesh.

![image](https://github.com/user-attachments/assets/08aa6ee0-3aa7-4d22-a26c-fe633ba951e1)



# Support My Work
As a solo developer, your financial support would be greatly appreciated and helps me continue working on this project.
- [Asset Store](https://assetstore.unity.com/publishers/12117)
- [Steam](https://store.steampowered.com/curator/45066588)
- [GitHub Sponsors](https://github.com/sponsors/eviltwo)
