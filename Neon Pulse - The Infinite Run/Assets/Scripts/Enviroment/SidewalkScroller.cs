using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewalkScroller : MonoBehaviour
{
    public float resetZ = -30f;

    private DecorationGenerator decorationGen;

    void Start()
    {
        decorationGen = FindObjectOfType<DecorationGenerator>();
    }

    void Update()
    {
        transform.position -= Vector3.forward * GameSpeedController.ScrollSpeed * Time.deltaTime;

        if (transform.position.z < resetZ)
        {
            if (decorationGen != null)
            {
                decorationGen.RemoveDecoration(gameObject);
            }
        }
    }
}
