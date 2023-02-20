using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueUnitScript : UnitScript
{
    public ParticleSystem hitParticle;
    public Transform splitEnemies;

    //We overwrite the original damage function so it spawns particles now
    public override void Damage(int dmg)
    {
        Debug.Log("Blue damage");
        health -= dmg;

        Instantiate(hitParticle, transform.position, Quaternion.identity);
    }

    //This overrides the Die function from the unity scrript
    protected override void Die()
    {
        //Instantiate two split enemies in the same position
        Instantiate(splitEnemies, transform.position, Quaternion.identity);
        Instantiate(splitEnemies, transform.position, Quaternion.identity);

        //Destroy the original
        Destroy(this.gameObject);
    }
}
