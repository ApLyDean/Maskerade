using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerAttributes : MonoBehaviour
{
    public HealthManager htm;
    public GameObject debugPopup;

    void Start()
    {
        debugPopup.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthManager>().TakeDamage(htm.attack);
            debugPopup.SetActive(true);
        }

    }
}
