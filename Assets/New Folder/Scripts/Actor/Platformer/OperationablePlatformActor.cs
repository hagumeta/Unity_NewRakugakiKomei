using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class OperationablePlatformActor : PlatformActor {
    
    //メンバ変数<インスペクタ表示>
    public ButtonOperation Operation;
    public ActionOfMovementStatus ActionStatus;

    
    //入力を受けた結果，移動する方向(左：-1  右:1  動かない:0)
    private float moveHorizontalAxis;


    /// <summary>
    /// 毎フレームの更新
    /// 入力を受け付ける
    /// </summary>
    protected override void Update ()
    {
        base.Update();

        //横方向の入力から進むべき方向を取得する
        if (this.Operation.Horizontal.IsPressing)
        {   //入力アリなら入力方向に設定(1 or -1 or 0)
            this.moveHorizontalAxis = this.Operation.Horizontal.AxisRaw;
        }
        else
        {   //入力がないなら0(動かない)
            this.moveHorizontalAxis = 0f;
        }
        //CurrentStateの更新
        this.CurrentState.IsRunning = this.moveHorizontalAxis != 0;
        this.FacingDirectionHorizontal = (int)this.moveHorizontalAxis;

        //ジャンプに関してのみ，即座に反応させる為Updateに記述．
        if (this.Operation.Jump.IsPressed　&& this.CurrentState.IsLanding)
        {
            this.Jump();
        }
    }

    /// <summary>
    /// ジャンプする
    /// 上方向に自身のジャンプ速度を足す
    /// </summary>
    protected void Jump()
    {
        this.VerticalSpeed += this.ActionStatus.JumpSpeed;
    }


    /// <summary>
    /// 毎フレームの更新
    /// 受け付けた入力を物理エンジンに反映
    /// </summary>
    private void FixedUpdate()
    {
        if (!this.IsFrozen) {
            this.HorizontalSpeed = this.moveHorizontalAxis * this.ActionStatus.RunSpeed;

            if (this.CurrentState.IsJumping && !this.Operation.Jump.IsPressing)
            {
                this.VerticalSpeed *= 0.9f;
            }
        }
    }


    //===========================================================================================================


    [System.Serializable]
    public class ActionOfMovementStatus
    {
        public float JumpSpeed, RunSpeed;
    }

    //===========================================================================================================

    [System.Serializable]
    public class ButtonOperation
    {
        public InputButton Horizontal, Vertical, Jump;


        [System.Serializable]
        public struct InputButton
        {
            public string ButtonName;

            public bool IsPressed { get { return Input.GetButtonDown(this.ButtonName); } }
            public bool IsPressing { get { return Input.GetButton(this.ButtonName); } }
            public float AxisRaw { get { return Input.GetAxisRaw(this.ButtonName); } }
            public float Axis { get { return Input.GetAxis(this.ButtonName); } }
        }
    }


}