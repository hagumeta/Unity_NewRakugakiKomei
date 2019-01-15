using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {



    [SerializeField]
    public GameObject gameOverObject;

    public enum DeathMode
    {
        OneChanceMode,
        StockMode
    }
    public static DeathMode NowDeathMode = DeathMode.OneChanceMode;

    public void CallGameOver()
    {
        switch (NowDeathMode)
        {
            case DeathMode.OneChanceMode:
                Instantiate(this.gameOverObject);
                break;
        }
    }
}