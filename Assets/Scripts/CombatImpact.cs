using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatImpact : MonoBehaviour
{
    public HealthManager htm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthManager>().TakeDamage(htm.attack);
        }
    }
}
