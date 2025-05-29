using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{

    public MovimientoCapusla movimiento;

    public override void Start()
    {
        base.Start();
        movimiento = GameObject.FindObjectOfType<MovimientoCapusla>();
    }

    public override void ApplyPlayerEfect(GameObject targetObject, float duration)
    {
        movimiento.ActivateShield(duration);
    }

    public override void RevertirEfecto()
    {
        movimiento.DeactivateShield();
        Destroy(this.gameObject);
    }
}
