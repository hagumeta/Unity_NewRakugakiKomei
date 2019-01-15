using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalColliderTrapEventTrigger : ColliderTrapEventTrigger{

    public bool IsFromRight, IsFromLeft, IsFromTop, IsFromButtom;
    protected override void CallTrapEvent(GameObject other)
    {
        var rigid = other.GetComponent<Rigidbody2D>();
        bool isTrigger = false;
        if (rigid != null)
        {
            if (IsFromRight && rigid.velocity.x < 0) isTrigger = true;
            if (IsFromLeft && rigid.velocity.x > 0) isTrigger = true;
            if (IsFromTop && rigid.velocity.y < 0) isTrigger = true;
            if (IsFromButtom && rigid.velocity.y > 0) isTrigger = true;
            if (isTrigger) {
                base.CallTrapEvent(other);
            }
        }
    }
}