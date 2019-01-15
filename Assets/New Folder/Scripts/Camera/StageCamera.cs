using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCamera : MonoBehaviour {


    public Transform TrackingTargetObjectTransform;
    public bool IsTrackingSmooth;
    public Vector2 TrackingCenterScreenPosition;
    public float TrackingSpeed;
    public bool IsFrozenScrollX, IsFrozenScrollY;


    private new Camera camera;
    private Camera Camera
    {
        get
        {
            if (this.camera == null)
            {
                this.camera = this.GetComponent<Camera>();
            }
            return this.camera;
        }
    }


    private Vector3 targetPosition {
        get
        {
            Vector3 pos = (this.Camera.WorldToScreenPoint(this.TrackingTargetObjectTransform.position));
            return new Vector3(pos.x / Screen.width, pos.y / Screen.height);
        }
    }
    private Vector3 cameraPosition {
        set{
            if (this.IsFrozenScrollX) { value.x = this.cameraPosition.x; }
            if (this.IsFrozenScrollY) { value.y = this.cameraPosition.y; }
            this.transform.position = new Vector3(value.x, value.y, this.transform.position.z);
        }
        get => this.transform.position;
    }
    private Vector2 cameraSizeInWorld
    {
        get => new Vector2(this.Camera.orthographicSize * 2 * this.Camera.aspect, this.Camera.orthographicSize * 2);
    }



    private void Update()
    {
        if (this.TrackingTargetObjectTransform != null)
        {
            if (this.IsTrackingSmooth)
            {
                this.TrackObjectSmooth();
            }
            else
            {
                this.TrackObjectDiveded();
            }
        }
    }

    private void TrackObjectSmooth()
    {
        Vector2 pos = (this.TrackingCenterScreenPosition) * this.cameraSizeInWorld;
        this.cameraPosition = this.TrackingTargetObjectTransform.position - (Vector3)pos;
    }

    private void TrackObjectDiveded()
    {
        if (this.IsTargetObjectOutOfCamera) {
            if (this.targetPosition.x > 1) this.cameraPosition += new Vector3(this.cameraSizeInWorld.x, 0);
            if (this.targetPosition.x < 0) this.cameraPosition -= new Vector3(this.cameraSizeInWorld.x, 0);
            if (this.targetPosition.y > 1) this.cameraPosition += new Vector3(0, this.cameraSizeInWorld.y);
            if (this.targetPosition.y < 0) this.cameraPosition -= new Vector3(0, this.cameraSizeInWorld.y);

        }
    }


    private bool IsTargetObjectOutOfCamera
    {
        get
        {
            if (this.TrackingTargetObjectTransform == null)
                return false;
            else
                return (this.targetPosition.x < 0 || this.targetPosition.x > 1
                            || this.targetPosition.y > 1 || this.targetPosition.y < 0) ;
        }
    }


    private void OnDrawGizmos()
    {
        float cnt_x = 20;
        float cnt_y = 4;

        Gizmos.color = Color.cyan;
        for (int x=0; x<cnt_x; x++) {
            for (int y = 0; y < cnt_y; y++)
            {
                Vector2 pos = this.transform.position + new Vector3((x - cnt_x/2)*this.cameraSizeInWorld.x
                                                                ,(y - cnt_y/2) * this.cameraSizeInWorld.y);
                Gizmos.DrawWireCube(pos, this.cameraSizeInWorld);
            }
        }
    }
}