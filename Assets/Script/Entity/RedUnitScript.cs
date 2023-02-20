using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedUnitScript : UnitScript
{
    //The particles that will be created when this object is spawned
    public ParticleSystem spawnParticles;

    // Start is called before the first frame update
    new void Start()
    {
        health = MaxHP;

        Instantiate(spawnParticles, transform.position, Quaternion.identity);
    }
}
