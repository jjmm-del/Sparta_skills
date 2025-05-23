using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    void Activate();
    void Deactivate();
}
public class PowerUpManager : MonoBehaviour
{
    private IPowerUp currentPowerUp;
    private Coroutine powerUpRoutine;

    public void ApplyPowerUp(IPowerUp powerUp, float duration)
    {
        if (powerUpRoutine != null)
        {
            StopCoroutine(powerUpRoutine);
        }

        if (currentPowerUp != null)
        {
            currentPowerUp.Deactivate();
        }
        currentPowerUp = powerUp;
        currentPowerUp.Activate();
        powerUpRoutine = StartCoroutine(PowerUpDuration(duration));
    }

    private IEnumerator PowerUpDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentPowerUp.Deactivate();
        currentPowerUp = null;
    }
    
}
