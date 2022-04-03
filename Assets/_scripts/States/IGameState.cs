using System.Collections;

public interface IGameState
{
    GamesStateId StateId { get; }
    IEnumerator Start(GameManager gameManager);
    void HitTarget(GameManager gameManager);
    void Next(GameManager gameManager);
    void Lose(GameManager gameManager);
    void Restart(GameManager gameManager);
    void StartGame(GameManager gameManager);
}

