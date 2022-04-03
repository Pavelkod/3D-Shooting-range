using UnityEngine;

public class Repo<T> : MonoBehaviour where T : class
{
    public DataContext Ctx;

    protected T _data;

    public virtual T Get()
    {
        if (_data == null) Load();

        return _data;
    }

    public virtual void Load()
    {
        Ctx.Load();
        _data = Ctx.Get<T>();
    }
    public virtual void Save() => Ctx.Save();


}
