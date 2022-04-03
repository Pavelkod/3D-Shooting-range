using System.Collections;

public abstract class BaseState : IGameState
{
    public virtual GamesStateId StateId => GamesStateId.None;

    public virtual void HitTarget(GameManager gameManager) { }
    public virtual void Lose(GameManager gameManager) { }
    public virtual void Next(GameManager gameManager) { }
    public virtual void StartGame(GameManager gameManager) { }
    public virtual void Restart(GameManager gameManager) => gameManager.SetState(new BeforeGameStartState());
    public virtual IEnumerator Start(GameManager gameManager)
    {
        yield break;
    }


}

