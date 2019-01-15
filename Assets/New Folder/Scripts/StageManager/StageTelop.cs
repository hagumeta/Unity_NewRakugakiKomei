using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageTelop : MonoBehaviour {


    public PauseManager StagePauseManager;

    protected abstract float TelopTime { get; }

    //trueでTelop表示を行うようにする
    public static bool IsTelopEnabled = true;

    private void Start()
    {
        if(IsTelopEnabled) {
            this.StagePauseManager.IsPausing = true;
            StartCoroutine(this.IETelop());
            IsTelopEnabled = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator IETelop()
    {
        StartCoroutine(this.IETelopEffect());
        yield return new WaitForSeconds(this.TelopTime);

        this.StagePauseManager.IsPausing = false;
        Destroy(this.gameObject);
    }


    abstract protected IEnumerator IETelopEffect();
}
