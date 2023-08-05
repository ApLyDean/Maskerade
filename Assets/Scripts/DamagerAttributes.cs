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
        EnableWeaponCollider(0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthManager>().TakeDamage(htm.attack);
            debugPopup.SetActive(true);
        }
    }

    public GameObject weapon;

    public void EnableWeaponCollider(int isEnable)
    {
        var col = weapon.GetComponent<Collider>();
        if (isEnable == 1)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
}
