internal class MoveToNextTargetState : BaseState
{
    public override GamesStateId StateId => GamesStateId.NextTarget;
    public override void Next(GameManager gameManager)
    {
        gameManager.SetState(new TargetShootState());
    }
}

