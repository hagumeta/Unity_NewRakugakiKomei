using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour{

	
	public void GameRetry()
    {
        StageManager.Retry();
    }

    public void ReturnToTitle()
    {
        GameManager.GotoTitle();
    }
}