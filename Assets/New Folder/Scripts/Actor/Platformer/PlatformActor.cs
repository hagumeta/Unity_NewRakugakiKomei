﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlatformActor : MonoBehaviour {
    public bool IsFrozen = false;


    //メンバ変数
    protected Animator Animator;                    
    protected Rigidbody2D Rigidbody;                
    protected CurrentStateManager CurrentState;
    protected AnimationManager Animation;
    protected FootOfPlatformActor ActorFoot;
    /// <summary>
    /// ActorFootについて
    /// このスクリプトをつけたGameObject下に"Foot"という名のGmaeObjectがあり，
    /// "Foot"についているFootOfActorから参照している
    /// </summary> 


    //自身のRigidbodyの速度プロパティ
    public float HorizontalSpeed
    {   //水平方向(x)
        set { this.Rigidbody.velocity = new Vector2(value, this.VerticalSpeed); }
        get { return this.Rigidbody.velocity.x; }
    }   
    public float VerticalSpeed
    {   //垂直方向(y)
        set { this.Rigidbody.velocity = new Vector2(this.HorizontalSpeed, value); }
        get { return this.Rigidbody.velocity.y; }
    }
    
    //水平方向について，Actorが向いている方向　のプロパティ(1:右，-1:左)
    public int FacingDirectionHorizontal
    {
        get { if (this.transform.localScale.x > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        set
        {
            int axis = Mathf.Clamp(value, -1, 1);
            if (axis != 0)
            {
                this.transform.localScale = new Vector3(axis * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            }
        }
    }


    /// <summary>
    /// 初期化
    /// 自身のパラメータをそれぞれ取得/初期化
    /// </summary>
    protected virtual void Start()
    {
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
        this.Animator = this.GetComponent<Animator>();

        //自身のgameObjectの子からFootOfActorを取得する
        this.ActorFoot = this.GetComponentInChildren<FootOfPlatformActor>();

        this.Animation = new AnimationManager(this);
        this.CurrentState = new CurrentStateManager(this);
    }




    /// <summary>
    /// 各フレーム更新
    /// AnimationとCurrentStateを更新する
    /// </summary>
    protected virtual void Update()
    {
        if (this.IsFrozen)
        {   //frozen中なら動作停止
            this.Rigidbody.velocity = Vector2.zero;
            return;
        }
        else {
            this.CurrentState.UpdateState();
            this.Animation.UpdateAnimation();
        }
    }



    //===========================================================================================================


    /// <summary>
    /// 対象Actorの状態管理を行うクラス
    /// 簡単に言えば「地面に足が付いているか」，「空中にいるか」等の状態を保存している
    /// </summary>
    public class CurrentStateManager
    {
        //対象となるActor
        protected PlatformActor Actor;

        protected FootOfPlatformActor ActorFoot { get { return this.Actor.ActorFoot; } }

        /// <summary>
        /// コンストラクタ
        /// 対象Actorをセット
        /// </summary>
        /// <param name="actor"></param>
        public CurrentStateManager(PlatformActor actor){
            this.Actor = actor;
        }


        public bool IsLanding { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsFalling { get; private set; }
        public bool IsRunning { get; set; }   //走っているかどうかは知らんので外部からアクセスして更新できる
        

        /// <summary>
        /// 現在の状態を更新する
        /// </summary>
        public virtual void UpdateState()
        {
            if (this.ActorFoot != null)
            {
                this.IsLanding = this.ActorFoot.IsLanding;
            }
            else
            {
                this.IsLanding = false;
            }


            if (!this.IsLanding)
            {
                if (this.Actor.VerticalSpeed > 0)
                {   //上昇中
                    this.IsFalling = false;
                    this.IsJumping = true;
                }
                else
                {   //下降中
                    this.IsFalling = true;
                    this.IsJumping = false;
                }
            }
        }
    }



    //===========================================================================================================

    /// <summary>
    /// 対象ActorのAnimation管理を行うクラス
    /// とは言っているもののAnimatorを参照する
    /// 現在のActorの状態CurrentStateに合わせてAnimatorに反映させる
    /// </summary>
    public class AnimationManager
    {
        //対象となるActor
        protected PlatformActor Actor;
        
        //それぞれ対象Actorからパラメータ取得するプロパティ
        protected Animator Animator { get { return this.Actor.Animator; } }
        protected CurrentStateManager CurrentState { get { return this.Actor.CurrentState; } }
        
        /// <summary>
        /// コンストラクタ
        /// 対象Actorをセット
        /// </summary>
        /// <param name="actor"></param>
        public AnimationManager(PlatformActor actor)
        {
            this.Actor = actor;
        }

        /// <summary>
        /// 対象ActorのAnimatorに現在のActorの状態を反映させる
        /// </summary>
        public virtual void UpdateAnimation()
        {
            if (this.Animator != null) {
                this.Animator.SetBool("IsOnGround", this.CurrentState.IsLanding);
                this.Animator.SetBool("IsRunning", this.CurrentState.IsRunning);
                this.Animator.SetBool("IsJumping", this.CurrentState.IsJumping);
                this.Animator.SetBool("IsFalling", this.CurrentState.IsFalling);
            }
        }
    }
}