namespace eviltwo.PolyMeshTools
{
    public interface IPolyMeshBlueprint
    {
        string GetName();

        PolyMeshTransform GetTransform();

        void Write(IPolyWriter writer);
    }
}
