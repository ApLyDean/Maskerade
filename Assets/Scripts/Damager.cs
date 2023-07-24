using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    //public HealthManager playerhm;
    public HealthManager enemyhm;

    private Animator anim;
    public HealthManager htm;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        #region DamageTesting
        if(Input.GetKeyDown(KeyCode.F11))
        {
            anim.SetTrigger("isAttacking");
            //playerhm.DealDamage(enemyhm.gameObject);
            
        }
        if(Input.GetKeyDown(KeyCode.F12))
        {
            //enemyhm.DealDamage(playerhm.gameObject);
        }        
        #endregion

    }

}
