internal class ShowFirstTargetState : BaseState
{
    public override GamesStateId StateId => GamesStateId.StartGame;
    public override void StartGame(GameManager gameManager)
    {
        gameManager.SetState(new TargetShootState());
    }
}

