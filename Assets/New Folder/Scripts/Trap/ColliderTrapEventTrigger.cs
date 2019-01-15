using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderTrapEventTrigger : TrapEventTrigger{

    //対象オブジェクトのタグ
    public string TargetObjectTag;
    public bool IsColliderUsingTrigger;

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.IsColliderUsingTrigger) {
            if (collision.tag == this.TargetObjectTag)
            {
                this.CallTrapEvent(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.IsColliderUsingTrigger)
        {
            if (collision.gameObject.tag == this.TargetObjectTag)
            {
                this.CallTrapEvent(collision.gameObject);
            }
        }
    }


    virtual protected void CallTrapEvent(GameObject other)
    {
        this.InvokeTrapEvent();
    }
}