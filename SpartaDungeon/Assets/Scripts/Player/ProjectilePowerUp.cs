using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerUp : IPowerUp
{
    private PlayerController controller;
    private GameObject projectilePrefab;
    private float projectileSpeed;
    private float projectileLifeTime;

    public ProjectilePowerUp(PlayerController controller, GameObject prefab, float speed, float lifetime)
    {
        this.controller = controller;
        this.projectilePrefab = prefab;
        this.projectileSpeed = speed;
        this.projectileLifeTime = lifetime;
    }
    public void Activate()
    {
        controller.canFire = true;
        controller.fireProjectilePrefab = projectilePrefab;
        controller.projectileSpeed = projectileSpeed;
        controller.projectileLifeTime = projectileLifeTime;
    }

    public void Deactivate()
    {
        controller.canFire = false;
    }
}
