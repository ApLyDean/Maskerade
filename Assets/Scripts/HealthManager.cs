using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float attack;

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void DealDamage(GameObject target)
    {
        var hm = target.GetComponent<HealthManager>();
        if (hm != null)
        {
            hm.TakeDamage(attack);
        }
    }
}
