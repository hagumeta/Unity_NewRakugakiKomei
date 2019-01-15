using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class TrapEventTrigger : MonoBehaviour{

    //シリアライズ：イベントフィールド
    [SerializeField]
    private ExtendedEvent Events;

    virtual protected ExtendedEvent TrapEvents {
        get => this.Events;
        set => this.Events = value;
    }

    [SerializeField]
    protected bool IsOnlyOnce;

    private bool isInvoked;

    virtual protected void Start()
    {
        //this.TrapEvents.Listeners.Add(new ExtendedEvent.GameObjectContainer(this.transform.parent.gameObject));
        //this.TrapEvents(this.CallBackTrapEvent);
    }

    
    virtual protected void InvokeTrapEvent()
    {
        if (this.TrapEvents != null)
        {
            this.TrapEvents.Invoke();
            if (this.IsOnlyOnce)
            {
                this.TrapEvents = null;
            }
        }
        
    }

   
}