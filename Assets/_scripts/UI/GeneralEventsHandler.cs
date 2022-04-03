using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralEventsHandler : MonoBehaviour
{
    public void OnRetry() => GameManager.Restart();


    public void OnMainMenu() => SceneManager.LoadScene("MainMenu");

}
