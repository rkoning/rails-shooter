using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireMode : FireMode
{
    public int numberOfShots;
    public float duration;

    public override void Activate()
    {
        StartCoroutine(ActivateOverDuration(duration));    
    }

    private IEnumerator ActivateOverDuration(float duration) {
        float betweenShots = duration / (float) numberOfShots;

        for (int i = 0 ;i < numberOfShots; i++) {
            FireOrNext();
            yield return new WaitForSeconds(betweenShots);
        }
    }

    private void FireOrNext() {
        if (next) {
            next.Activate();
        } else {
            foreach(var we in weapon.effects) {
                we.Activate();
            }
        }
    }
}
