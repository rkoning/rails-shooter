using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayFireMode : FireMode
{
    public float delay = 0.25f;

    public override void Activate()
    {
        if (next) {
            StartCoroutine(WaitThenActivateNext(delay));
        }
    }

    private IEnumerator WaitThenActivateNext(float duration) {
        yield return new WaitForSeconds(duration);
        next.Activate();
    }
}
