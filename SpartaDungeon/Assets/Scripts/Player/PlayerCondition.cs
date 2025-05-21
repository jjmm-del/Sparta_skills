using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damageAmount);
}
public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;
    
    Condition health {get {return uiCondition.health;}}
    //other condition later

    public event Action onTakeDamge;
    
    
    // Update is called once per frame
    /*void Update()
    {
        
        if (health.curValue <= 0)
        {
            Die();
        }
    }*/

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamge?.Invoke();
    }
}
