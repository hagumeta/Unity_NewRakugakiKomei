using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// フェードエフェクト
/// フェードアウト☞フェードインで自爆して終了
/// </summary>
public class FadeEffect : MonoBehaviour
{
    //シーン遷移の際に使用するエフェクトに掛ける時間
    //シーン遷移全体の時間は fadeTime(暗転遷移) + waitTime(真っ暗のまま待機) + fadeTime(明転遷移)
    [SerializeField]
    private float fadeTime, waitTime;
    public float FadeTime
    {
        get => this.fadeTime;
        set => this.fadeTime = Mathf.Clamp(value, 0, 100000);
    }
    public float WaitTime
    {
        get => this.waitTime;
        set => this.waitTime = Mathf.Clamp(value, 0, 100000);
    }

    private Fade fade;

    private void Start()
    {
        this.fade = this.GetComponent<Fade>();
        StartCoroutine(this.transitionFade(this.FadeTime, this.WaitTime));
    }


    /// <summary>
    /// 要オーバーライド
    /// フェードアウトした時に行う処理をここに書く
    /// </summary>
    virtual protected void MethodInFadeout(){}


    /// <summary>
    /// 遷移エフェクト管理を行う．
    /// </summary>
    /// <param name="FadeTime"></param>
    /// <param name="WaitTime"></param>
    /// <returns></returns>
    private IEnumerator transitionFade(float FadeTime, float WaitTime)
    {
        yield return new WaitForSeconds(Time.deltaTime + Time.deltaTime*3);


        this.fade.FadeIn(FadeTime);
        yield return new WaitForSeconds(FadeTime + WaitTime);

        this.MethodInFadeout();

        yield return new WaitForSeconds(Time.deltaTime);
        this.fade.FadeOut(FadeTime);


        yield return new WaitForSeconds(FadeTime);
        Destroy(this.gameObject);
    }
}
