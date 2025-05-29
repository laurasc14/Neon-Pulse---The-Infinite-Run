using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private DecorationGenerator decorationGen;

    private void Start()
    {
        if (decorationGen == null)
            decorationGen = FindObjectOfType<DecorationGenerator>();
    }

    void Update()
    {
        transform.position -= Vector3.forward * GameSpeedController.ScrollSpeed * Time.deltaTime;

        if (transform.position.z < -30f)
        {
            var envGen = FindObjectOfType<EnviromentGenerator>();
            if (envGen != null)
            {
                envGen.RemoveRoad(gameObject);
            }
        }
    }
}
