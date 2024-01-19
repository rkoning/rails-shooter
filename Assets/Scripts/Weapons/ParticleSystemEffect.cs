using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleSystemEffectActivationType {
    Emit,
    Play
}

public class ParticleSystemEffect : WeaponEffect
{
    public ParticleSystemEffectActivationType type;
    public int emissionCount = 1;

    private ParticleSystem particleSystem;
    
    private void Start() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public override void Activate()
    {
        switch (type) {
            case ParticleSystemEffectActivationType.Emit:
                particleSystem.Emit(emissionCount);
                break;
            case ParticleSystemEffectActivationType.Play:
                if (particleSystem.isPlaying == false)
                    particleSystem.Play();
                break;
        }
    }

    private void OnParticleCollision(GameObject other) {
        if (other.TryGetComponent(out Health health)) {
            weapon.DealDamage(health);
        }
    }
}
