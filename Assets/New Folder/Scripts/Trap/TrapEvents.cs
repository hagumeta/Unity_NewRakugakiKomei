using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapEvents : MonoBehaviour{

    /// <summary>
    /// 指定方向にこのスクリプトをつけたオブジェクトをぶっ飛ばす
    /// 物理法則完全無視
    /// </summary>
    /// <param name="flySpeed"></param>
    public void FlyAway(Vector2 flySpeed)
    {
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {   //rigidbodyがついていなかったら無理矢理つける
            rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        }

        //全摩擦を0にする
        rigidbody.angularDrag = 0;
        rigidbody.drag = 0;

        //速度を指定のものに，かつ重力を消してコミカルな動きに
        rigidbody.velocity = flySpeed;
        rigidbody.gravityScale = 0;
    }

    /// <summary>
    /// 指定方向にこのスクリプトをつけたオブジェクトをぐるぐる回転させながらぶっ飛ばす
    /// 物理法則完全無視
    /// </summary>
    /// <param name="flySpeed"></param>
    public void FlyAwayWidhSpinning(Vector2 flySpeed)
    {
        //とりあえず吹っ飛ばす
        this.FlyAway(flySpeed);
        

        //変態PhysicsMaterial2Dに置き換える
        Collider2D collider = this.GetComponent<Collider2D>();
        if (collider != null)
        {
            PhysicsMaterial2D material = new PhysicsMaterial2D();
            material.bounciness = 1.1f;
            material.friction = 0;

            collider.sharedMaterial = material;
        }

        //もの凄い勢いで回転させるスクリプトをつける
        ObjectSpinner spinner = this.gameObject.AddComponent<ObjectSpinner>();
        spinner.SpinSpeed = 60f;
    }



    /// <summary>
    /// このスクリプトをつけたオブジェクトのSpriteを指定のものに変更する
    /// </summary>
    /// <param name="sprite"></param>
    public void ChangeSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        Animator animator = this.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        spriteRenderer.sprite = sprite;
    }


    /// <summary>
    /// このスクリプトをつけたオブジェクトを指定サイズまで引き延ばす
    /// </summary>
    public void StrechScale(Vector2 scaleTo)
    {
        if (this.transform.localScale.x != 0) {
            scaleTo.x *= Mathf.Sign(this.transform.localScale.x);
        }
        if (this.transform.localScale.y != 0) {
            scaleTo.y *= Mathf.Sign(this.transform.localScale.y);
        }
        iTween.ScaleTo(this.gameObject, scaleTo, 0.1f);
    }
}