using System.Collections;

internal class WinGameState : BaseState
{
    public override GamesStateId StateId => GamesStateId.Win;
    public override IEnumerator Start(GameManager gameManager)
    {
        Utils.EnableInput(false);
        Utils.LockCursor(false);
        return base.Start(gameManager);
    }
}

