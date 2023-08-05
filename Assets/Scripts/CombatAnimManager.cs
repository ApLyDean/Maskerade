using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimManager : MonoBehaviour
{
    //public HealthManager playerhm;
    //public HealthManager enemyhm;

    private Animator anim;
    //public HealthManager htm;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        #region AtkDamage
        if(Input.GetKeyDown(KeyCode.F11))
        {
            anim.SetTrigger("isAttacking");   
        }    
        #endregion

    }

}
