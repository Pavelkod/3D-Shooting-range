using System.Collections;
using UnityEngine;

public class BeforeGameStartState : BaseState
{
    public override GamesStateId StateId => GamesStateId.PrepareGame;
    public override IEnumerator Start(GameManager gameManager)
    {
        Utils.LockCursor();
        Utils.EnableInput(false);
        yield return new WaitForSeconds(1f);
        gameManager.SetState(new ShowFirstTargetState());
    }
}

