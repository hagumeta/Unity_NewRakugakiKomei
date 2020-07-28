using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject.FindObjectOfType<TitleController>().GetComponent<TitleController>().ShowMenu();
            this.gameObject.SetActive(false);
        }
    }
}
