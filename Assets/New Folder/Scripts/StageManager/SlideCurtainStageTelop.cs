using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCurtainStageTelop : StageTelop{
    public float WaitTime;
    public float CurtainOpenTime;

    protected override float TelopTime
    {
        get => this.WaitTime + this.CurtainOpenTime;
    }


    protected override IEnumerator IETelopEffect()
    {
        yield return new WaitForSeconds(this.WaitTime);
        Debug.Log(this.TelopTime);
        iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0, "time", this.CurtainOpenTime, "easetype", iTween.EaseType.easeInOutSine));
        yield return new WaitForSeconds(this.CurtainOpenTime);
    }
}