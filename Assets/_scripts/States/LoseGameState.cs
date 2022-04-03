using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

internal class LoseGameState : BaseState
{
    public override GamesStateId StateId => GamesStateId.Lose;
    public override IEnumerator Start(GameManager gameManager)
    {
        Utils.EnableInput(false);
        Utils.LockCursor(false);
        return base.Start(gameManager);
    }
}

