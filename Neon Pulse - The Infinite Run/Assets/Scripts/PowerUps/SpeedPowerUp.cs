using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public float speedMultiplier = 0.75f;   // Multiplicador de velocitat

    public MovimientoCapusla movimiento;

    public override void Start()
    {
        base.Start();
        movimiento = GameObject.FindObjectOfType<MovimientoCapusla>();
    }

    public override void ApplyPlayerEfect(GameObject targetObject, float duration)
    {
        movimiento.ActivateSpeedBoost(speedMultiplier, duration);
    }

    public override void RevertirEfecto()
    {
        movimiento.DeactivateSpeedBoost();
    }
}
