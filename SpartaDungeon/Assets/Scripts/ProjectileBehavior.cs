using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ProjectileBehaviour : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakePhysicalDamage(Damage);
            Destroy(gameObject);
        }
    }

}
