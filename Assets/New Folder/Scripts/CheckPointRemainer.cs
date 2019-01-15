using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPointRemainer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetCheckPoint();
            Destroy(this.gameObject);
        }
    }

    private void GetCheckPoint()
    {
        PlayerStartPoint.SetCheckPoint(this.transform.position);
    }
}