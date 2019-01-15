using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/// <summary>
/// ＜参照用：PlatfomActor＞
/// PlatformActorの足.
/// 自身が"Block"タグのオブジェクトと衝突している間，IsLandingをtrueにする
/// </summary>
public class FootOfPlatformActor : MonoBehaviour {

    public bool IsLanding = false;

    void Start () {
        foreach (var collider in this.GetComponents<Collider2D>())
        {
            collider.isTrigger = true;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Block") {
            this.IsLanding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Block"){
            this.IsLanding = false;
        }
    }
}
