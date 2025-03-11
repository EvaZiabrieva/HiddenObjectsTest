public interface IConfigSystem
{
    public abstract T GetConfig<T>(string id) where T : BaseConfig, new();
}
