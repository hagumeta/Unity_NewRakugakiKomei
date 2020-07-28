using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public Transform TitleMenu;

    public void ShowMenu()
    {
        this.TitleMenu.gameObject.SetActive(true);
    }

    public void BeginNewGame()
    {
        GameManager.GotoStage(1);
    }

    public void BeginContinueGame()
    {

    }

    public void OpenConfiguration()
    {

    }

    public void GameEnd()
    {
        GameManager.ExitGame();
    }
}
