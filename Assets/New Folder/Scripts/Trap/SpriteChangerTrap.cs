using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]

public class SpriteChangerTrap : MonoBehaviour
{
    public Sprite afterChangeSprite;    //変化後のSprite
    public string targetTag;            //衝突対象のタグ

    
    private SpriteRenderer spriteRenderer;  //自分のSpriteRenderer

    /// <summary>
    /// 変数の初期化
    /// </summary>
    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// 対象タグのオブジェクト(targetTag)と衝突したら自身のスプライトを変更する
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == this.targetTag)
        {
            this.spriteRenderer.sprite = this.afterChangeSprite;
            this.enabled = false;
        }
    }
}