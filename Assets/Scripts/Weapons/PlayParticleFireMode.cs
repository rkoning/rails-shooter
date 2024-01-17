using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleFireMode : FireMode
{
    public ParticleSystem particle;

    public override void Activate()
    {
        if (!particle.isPlaying)
            particle.Play();

        if (next)
            next.Activate();
    }
}
