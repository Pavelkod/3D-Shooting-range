using System.Collections;

internal class TargetShootState : BaseState
{
    public override GamesStateId StateId => GamesStateId.TargetShoot;

    public override IEnumerator Start(GameManager gameManager)
    {
        Utils.EnableInput();
        return base.Start(gameManager);
    }

    public override void Lose(GameManager gameManager)
    {
        gameManager.SetState(new LoseGameState());
    }


    public override void HitTarget(GameManager gameManager)
    {
        if (TargetsManager.Instance.TargetsCount > 0)
            gameManager.SetState(new MoveToNextTargetState());
        else gameManager.SetState(new WinGameState());
    }
}

