using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NPCCondition : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    public void Awake()
    {
        currentHealth = maxHealth;
    }
   

    public void TakePhysicalDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount}damage, Remains: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died");
        Destroy(gameObject);
    }
    
}
