using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectCell : MonoBehaviour {

    private static bool IsAlreadyDecided;

    public bool IsSelected;
    public int GotoStageNumber;
    public GameObject DecisionEffect;

    private Animator animator;

    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
        IsAlreadyDecided = false;
    }

    private void Update()
    {
        if (this.IsSelected)
        {
            if (!IsAlreadyDecided && Input.GetButtonDown("Start"))
            {
                IsAlreadyDecided = true;
                Instantiate(DecisionEffect, this.transform.position, this.transform.rotation);
                this.animator.Play("Selected", 0);
                Invoke("GotoStage", 1f);
            }
        }

        this.animator.SetBool("IsHilighted", this.IsSelected && !IsAlreadyDecided);
    }


    private void GotoStage()
    {
        GameManager.GotoStage(this.GotoStageNumber);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IsSelected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsSelected = false;
        }
    }

}