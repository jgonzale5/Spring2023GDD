using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    //The starting health for this unit
    public int MaxHP = 100;

    //A unit will always have health
    //_health stores the actual value
    //while health allows us to modify _health through its methods
    private int _health;
    public int health
    {
        set
        {
            _health = value;

            //If the health is equal or less than 0, die
            if (_health <= 0)
                Die();
        }

        get
        {
            return _health;
        }
    }

    //Set the health to its initial, maximum value, at the beginning of the game
    protected void Start()
    {
        Debug.Log(transform.name);
        health = MaxHP;
    }

    //The function called when this unit dies
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    //The function that tells this unit to receive damage
    public virtual void Damage(int dmg)
    {
        health -= dmg;
    }
}
