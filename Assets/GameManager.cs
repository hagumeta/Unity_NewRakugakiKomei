using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {


    private static GameManager instance;


    public static PlatformActor NowMyPlayer;
    public static int StageNumber;

    /// <summary>
    /// このゲームオブジェクトはシーン遷移でリセットされないようにする
    /// </summary>
    void Start()
    {
        if (instance == null){
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }



    /// <summary>
    /// 各シーン名をまとめた構造体
    /// </summary>
    public struct Scene
    {
        public static string Title = "TitleScene";
        public static string StageSelect = "StageSelectScene";
        public static string Stage = "StageScene";
    }


    /// <summary>
    /// Static タイトルへシーン遷移を行う．
    /// </summary>
    public static void GotoTitle()
    {
        SceneTransitionManager.GotoScene(Scene.Title);
    }

    /// <summary>
    /// ステージセレクトへシーン遷移を行う．
    /// </summary>
    public static void GotoStageSelect()
    {
        SceneTransitionManager.GotoScene(Scene.Title);
    }

    /// <summary>
    /// 現在のシーンをリロードする
    /// </summary>
    public static void ReloadSameScene()
    {
        SceneTransitionManager.GotoScene(SceneManager.GetActiveScene());
    }

    /// <summary>
    /// 現在のシーンをリロードする
    /// フェード時間の設定が出来る(デフォルト値から倍率設定)
    /// </summary>
    public static void ReloadSameScene(float transitionRate)
    {
        SceneTransitionManager.GotoScene(SceneManager.GetActiveScene(), 
            SceneTransitionManager.FadeTime * transitionRate, SceneTransitionManager.WaitTime * transitionRate);
    }


    /// <summary>
    /// ステージ遷移を行う．
    /// </summary>
    /// <param name="stageNumber"></param>
    public static void GotoStage(int stageNumber)
    {
        StageNumber = stageNumber;   
        SceneTransitionManager.GotoScene(Scene.Stage);
    }


    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}