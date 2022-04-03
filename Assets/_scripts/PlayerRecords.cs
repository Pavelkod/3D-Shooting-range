using System;
using System.Collections.Generic;

[Serializable]
public class PlayerRecords
{
    public string PlayerName = "Player";
    public List<float> RecordsList = new List<float>();
    public float LastResult = 0;
}
