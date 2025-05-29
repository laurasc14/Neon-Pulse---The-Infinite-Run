using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollisionForPlayer : MonoBehaviour
{
    private Collider[] childColliders;
    private bool wasBelowThreshold = true;
    public float zThreshold = 5f;

    void Start()
    {
        // Buscar todos los colliders en hijos al iniciar
        childColliders = GetComponentsInChildren<Collider>(includeInactive: true);
    }

    void Update()
    {
        float parentZ = transform.position.z;

        // Verifica si la Z ha bajado del umbral
        if (parentZ < zThreshold && !wasBelowThreshold)
        {
            ActivateColliders();
            wasBelowThreshold = true;
        }
        // Verifica si la Z ha vuelto a subir al umbral o más
        else if (parentZ >= zThreshold && wasBelowThreshold)
        {
            ActivateColliders();
            wasBelowThreshold = false;
        }
    }

    void ActivateColliders()
    {
        foreach (var col in childColliders)
        {
            if (!col.enabled)
                col.enabled = true;
        }
    }
}
