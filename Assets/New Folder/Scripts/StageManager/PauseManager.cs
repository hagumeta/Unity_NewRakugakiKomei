/*
 *このソースは以下のサイト様から引用/追実装しました
引用：http://wordpress.notargs.com/blog/blog/2015/01/31/unity%E6%9C%80%E3%82%82%E3%82%B7%E3%83%B3%E3%83%97%E3%83%AB%E3%81%AA%E3%83%9D%E3%83%BC%E3%82%BA%E5%87%A6%E7%90%86/
追実装：Animatorが停止するように追実装
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Rigidbodyの速度を保存しておくクラス
/// </summary>
public class Rigidbody2DVelocity
{
    public Vector2 velocity;
    public float angularVeloccity;
    public Rigidbody2DVelocity(Rigidbody2D rigidbody)
    {
        velocity = rigidbody.velocity;
        angularVeloccity = rigidbody.angularVelocity;
    }
}

public class PauseManager : MonoBehaviour
{

    /// <summary>
    /// 現在Pause中か？
    /// </summary>
    public bool IsPausing;

    /// <summary>
    /// 無視するGameObject
    /// </summary>
    public GameObject[] IgnoreGameObjects;

    /// <summary>
    /// 前フレームのポーズ状態を保存
    /// </summary>
    private bool prevIsPausing;

    /// <summary>
    /// Rigidbodyのポーズ前の速度の配列
    /// </summary>
    private Rigidbody2DVelocity[] rigidbodyVelocities;


    /// <summary>
    /// ポーズ中のanimatorの配列
    /// </summary>
    private Animator[] pausingAnimators;

    /// <summary>
    /// Animatorのポーズ前のspeed配列
    /// </summary>
    private float[] animatorSpeeds;


    /// <summary>
    /// ポーズ中のRigidbodyの配列
    /// </summary>
    private Rigidbody2D[] pausingRigidbodies;

    /// <summary>
    /// ポーズ中のMonoBehaviourの配列
    /// </summary>
    private MonoBehaviour[] pausingMonoBehaviours;



    /// <summary>
    /// 毎フレームの更新
    /// </summary>
    void Update()
    {
        // ポーズ状態が変更されていたら、Pause/Resumeを呼び出す。
        if (this.prevIsPausing != this.IsPausing)
        {
            if (this.IsPausing)
            {   //ポーズ状態に変更
                Pause();
            }
            else
            {   //ポーズ状態から復帰
                Resume();
            }
            //変更されたポーズ状態を保存
            prevIsPausing = IsPausing;
        }
    }


    /// <summary>
    /// ポーズ状態にする
    /// 対象は自身の子オブジェクト．
    /// MonoBehaviorとRigidbodyを停止させる．
    /// </summary>
    void Pause()
    {
        /// Rigidbodyの停止
        // 子要素から、スリープ中でなく、IgnoreGameObjectsに含まれていないRigidbodyを抽出
        Predicate<Rigidbody2D> rigidbodyPredicate =
            rigid => !rigid.IsSleeping() &&
                   Array.FindIndex(this.IgnoreGameObjects, gameObject => gameObject == rigid.gameObject) == -1;
//ポーズするRigidbodyを取得
        this.pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody2D>(), rigidbodyPredicate);
        this.rigidbodyVelocities = new Rigidbody2DVelocity[pausingRigidbodies.Length];
        
        //対象のrigidbodyの現在の状態を保存してから止める
        for (int i = 0; i < pausingRigidbodies.Length; i++)
        {
            // 速度、角速度も保存しておく
            rigidbodyVelocities[i] = new Rigidbody2DVelocity(pausingRigidbodies[i]);
            pausingRigidbodies[i].Sleep();
        }


        /// MonoBehaviourの停止
        // 子要素から、activeかつこのインスタンスでないもの、IgnoreGameObjectsに含まれていないMonoBehaviourを抽出
        Predicate<MonoBehaviour> monoBehaviourPredicate =
            obj => obj.enabled &&
                   obj != this &&
                   Array.FindIndex(this.IgnoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        this.pausingMonoBehaviours = Array.FindAll(this.transform.GetComponentsInChildren<MonoBehaviour>(), monoBehaviourPredicate);
        //抽出した対象monoBehaviourを全て停止させる
        foreach (var monoBehaviour in pausingMonoBehaviours)
        {
            monoBehaviour.enabled = false;
        }
        ///Animatorの停止
        Predicate<Animator> animatorPredicate =
            animator => animator.enabled &&
                Array.FindIndex(this.IgnoreGameObjects, gameObject => gameObject == animator.gameObject) == -1;

        this.pausingAnimators = Array.FindAll(this.transform.GetComponentsInChildren<Animator>(), animatorPredicate);
        //抽出したAnimatorを停止させる
        foreach (var animator in this.pausingAnimators)
        {
            animator.enabled = false;
        }

    }

    /// <summary>
    /// ポーズ状態を解除
    /// </summary>
    void Resume()
    {
        // Rigidbodyの再開
        for (int i = 0; i < pausingRigidbodies.Length; i++)
        {
            pausingRigidbodies[i].WakeUp();
            pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
            pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
        }

        // MonoBehaviourの再開
        foreach (var monoBehaviour in pausingMonoBehaviours)
        {
            monoBehaviour.enabled = true;
        }

        // Animatorの再開をする
        foreach (var animator in this.pausingAnimators)
        {
            animator.enabled = true;
        }
    }
}