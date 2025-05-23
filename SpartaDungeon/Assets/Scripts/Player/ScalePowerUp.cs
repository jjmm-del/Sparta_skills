using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePowerUp : IPowerUp
{
    private Transform target;
    private Vector3 originalScale;
    private float scaleFactor;

    public ScalePowerUp(Transform target, float scaleFactor)
    {
        this.target = target;
        this.originalScale = target.localScale;
        this.scaleFactor = scaleFactor;
    }
    public void Activate()
    {
        target.localScale = originalScale * scaleFactor;
    }

    public void Deactivate()
    {
        target.localScale = originalScale;
    }
}
