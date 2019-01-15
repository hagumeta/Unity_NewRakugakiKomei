using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxEvents : MonoBehaviour {

    [SerializeField]
    private GameObject ItemObjectPrefab;
    private Transform ItemObjectTransform;

    private bool IsOpen;

    private void Start()
    {
        this.ItemObjectTransform = Instantiate(this.ItemObjectPrefab, this.transform).transform;
        this.ItemObjectTransform.gameObject.SetActive(false);
    }

    public void PopItem()
    {
        if (this.IsOpen) return;
        this.ItemObjectTransform.gameObject.SetActive(true);
        this.ItemObjectTransform.position = this.transform.position + new Vector3(0, this.transform.localScale.y);

        var rigid = this.ItemObjectTransform.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = new Vector2(0, 2);
        }
    }


    public void ToBeVisible()
    {
        var sprite = this.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.enabled = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Gizmos.DrawCube(this.transform.position, this.transform.localScale/2);
    }
}