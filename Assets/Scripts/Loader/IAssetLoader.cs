using System.Threading.Tasks;

public interface IAssetLoader
{
    Task<T> Load<T>(string assetId);
    void Unload<T>(T asset);
}
