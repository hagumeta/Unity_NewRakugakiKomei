using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Object_ScrollerInMyChildren : MonoBehaviour {

    public enum Axis{
        x, y, z
    }
    public Axis ScrollAxis;

    public float ScrollSpeed;
    public float ScrollSpan_distance;


    private Transform[] ScrollObjectTransforms;


    private float TailPositionOfScrollObjects
    {
        get
        {
            return -Mathf.Sign(this.ScrollSpeed) * this.ScrollObjectTransforms.Length / 2 * this.ScrollSpan_distance;
        }
    }
    private float HeadPositionOfScrollObjects
    {
        get
        {
            return Mathf.Sign(this.ScrollSpeed) * this.ScrollObjectTransforms.Length / 2 * this.ScrollSpan_distance;
        }
    }


    private void Start()
    {
        this.ScrollObjectTransforms = this.GetComponentsInChildren<Transform>().Where(t => t != this.transform && t.parent == this.transform).ToArray();
    }

    void Update ()
    {
        foreach (Transform trans in this.ScrollObjectTransforms) {

            Vector3 nextPos = trans.localPosition;

            switch (this.ScrollAxis) {

                case Axis.x:
                    nextPos = new Vector3(this.scrollOneAxisPosion(nextPos.x), nextPos.y, nextPos.z);
                    break;
                case Axis.y:
                    nextPos = new Vector3(nextPos.x, this.scrollOneAxisPosion(nextPos.y), nextPos.z);
                    break;
                case Axis.z:
                    nextPos = new Vector3(nextPos.x, nextPos.y, this.scrollOneAxisPosion(nextPos.z));
                    break;
            }

            trans.localPosition = nextPos;
        }

	}


    private float scrollOneAxisPosion(float axisPosition)
    {
        axisPosition += this.ScrollSpeed;
        if (Mathf.Sign(this.ScrollSpeed) * axisPosition > Mathf.Sign(this.ScrollSpeed) * this.HeadPositionOfScrollObjects)
        {
            axisPosition = this.TailPositionOfScrollObjects + this.ScrollSpeed;
        }

        return axisPosition;
    }


}
