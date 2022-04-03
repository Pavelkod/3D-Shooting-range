using UnityEngine;

public abstract class DataContext : MonoBehaviour
{
    protected string fileName;
    protected GameData _data = new GameData();

    public abstract void Load();

    public abstract void Save();

    public T Get<T>() where T : class
    {
        if (typeof(T) == typeof(PlayerRecords)) return _data.Records as T;
        if (typeof(T) == typeof(GameSettingsData)) return _data.GameSettings as T;
        return null;
    }
}
