using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliFloat : MonoBehaviour
{
    public float amplitud = 0.1f;     
    public float velocitat = 1f;      

    private float posicioBaseY;

    void Start()
    {
        posicioBaseY = transform.localPosition.y;
    }

    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.y = posicioBaseY + Mathf.Sin(Time.time * velocitat) * amplitud;
        transform.localPosition = pos;
    }
}
