using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour {

    public string StageSceneName;

    public GameObject GameOverObject;
    public GameObject FadeEffect;

    private Transform NowGameOverTransform;
    private static StageManager instance;


    /// <summary>
    /// ステージのリトライ
    /// </summary>
    public static void Retry()
    {
        instance.RetryStage();
    }
    public static PlayerOperationablePlatformActor NowPlayer;



    private void Start()
    {
        instance = this;
        //初期化する
        this.InitializeStage();
    }


    /// <summary>
    /// ステージを初期化して生成する
    /// </summary>
    public void InitializeStage()
    {
        StageTelop.IsTelopEnabled = true;
        this.CreateStage();
    }


    /// <summary>
    /// ステージをリトライさせる
    /// </summary>
    public void RetryStage()
    {
        //ゲームオーバーオブジェクトを消す
        if (this.NowGameOverTransform != null) { Destroy(this.NowGameOverTransform.gameObject); }
        //ステージ部分のシーンをリロードする
        StartCoroutine(this.ReLoadStage());
    }
    private IEnumerator ReLoadStage()
    {
        var fade = Instantiate(this.FadeEffect);
        var fadeEffect = fade.GetComponent<FadeEffect>();
        yield return new WaitForSeconds(fadeEffect.FadeTime + fadeEffect.WaitTime);
        SceneManager.UnloadScene(this.StageSceneName);
        this.CreateStage();
    }


    /// <summary>
    /// 対象ステージのシーンを生成して現在のシーンに加える
    /// ただし，生成前に対象ステージを消して被りを防止する
    /// </summary>
    private void CreateStage()
    {
        SceneManager.LoadScene(this.StageSceneName, LoadSceneMode.Additive);
    }
    
    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public static void GameOver()
    {
        instance.NowGameOverTransform = Instantiate(instance.GameOverObject).transform;
    }




}