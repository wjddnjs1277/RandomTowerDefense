using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();     
    }

    private void Update()
    {
        if (particle.isPlaying == false)
            EffectManager.Instance.ReturnPool(gameObject);
    }
}
