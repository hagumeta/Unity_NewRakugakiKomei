using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu_Action : MonoBehaviour {

    public Button FirstButton;

    private void Start()
    {
        this.FirstButton.Select();    
    }


}