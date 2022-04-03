using System.Collections;
using System.Collections.Generic;

public class SettingsRepo : Repo<GameSettingsData>
{
    public static SettingsRepo Instance;
    private void Awake() => Instance = this;
}
