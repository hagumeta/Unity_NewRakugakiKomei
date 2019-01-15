using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    [SerializeField]
    private GameObject PlayerObject;

    private static bool IsCheckPointEnabled;
    private static Vector2 CheckPointPosition;

    void Start () {
        if (IsCheckPointEnabled)
        {
            this.transform.position = CheckPointPosition;
        }
        this.CreatePlayer();

        Destroy(this.gameObject);
	}
	
    private void CreatePlayer()
    {
        Instantiate(this.PlayerObject, this.transform.position, this.transform.rotation, this.transform.parent);
    }


    public static void SetCheckPoint(Vector2 position)
    {
        IsCheckPointEnabled = true;
        CheckPointPosition = position;
    }
    public static void ReSetCheckPoint()
    {
        IsCheckPointEnabled = false;
    }

}