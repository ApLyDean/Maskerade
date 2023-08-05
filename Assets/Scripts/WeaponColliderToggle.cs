using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderToggle : MonoBehaviour
{
    public GameObject weapon;
    void Start()
    {
        EnableWeaponCollider(0);
    }

    public void EnableWeaponCollider(int isEnable)
    {
        var col = weapon.GetComponent<Collider>();
        if (isEnable == 1)
        {
            Debug.Log("Fist collider activated");
            col.enabled = true;
        }
        else
        {
            Debug.Log("Fist collider deactivated");
            col.enabled = false;
        }
    }
}
