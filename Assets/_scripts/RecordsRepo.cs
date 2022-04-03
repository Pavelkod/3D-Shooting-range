using System.Linq;

public class RecordsRepo : Repo<PlayerRecords>
{
    public static RecordsRepo Instance;
    private void Awake() => Instance = this;
    public override void Save()
    {
        _data.RecordsList.Add(_data.LastResult);
        _data.RecordsList = _data.RecordsList.OrderBy(x => x).Take(10).ToList();
        base.Save();
    }
}
