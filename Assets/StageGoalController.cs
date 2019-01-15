﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StageGoalController : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.NowMyPlayer.IsFrozen = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.enabled = false;
        }
    }
}