using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerUp : PowerUp
{
    public MovimientoCapusla movimiento;

    public override void Start()
    {
        base.Start();
        movimiento = GameObject.FindObjectOfType<MovimientoCapusla>();
    }

    public override void ApplyPlayerEfect(GameObject targetObject, float duration)
    {
        movimiento.ActivateMagnet(duration);
    }

    public override void RevertirEfecto()
    {
        movimiento.DeactivateMagnet();
    }
}
