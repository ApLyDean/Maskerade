using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float attack;
    AIManager aIManager;
    public GameObject enemyToDie;
    public Animator animator;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {     
            Debug.Log("Health is equal to zero");   
            animator = GetComponent<Animator>();
            animator.SetTrigger("Dead"); 
            StartCoroutine(Die());
        }
    }

    public void DealDamage(GameObject target)
    {
        var hm = target.GetComponent<HealthManager>();
        if (hm != null)
        {
            hm.TakeDamage(attack);
            enemyToDie = target;
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f); 
        gameObject.SetActive(false); 
    }
}
