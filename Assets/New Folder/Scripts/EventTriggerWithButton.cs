using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerWithButton : TrapEventTrigger {

    public string ButtonName;



    private void Update()
    {
        if (Input.GetButtonDown(this.ButtonName))
        {
            this.InvokeTrapEvent();
        }
    }


}