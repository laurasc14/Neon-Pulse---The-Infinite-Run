using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LucesPolicia : MonoBehaviour
{
    public GameObject rojo;
    public GameObject azul;
    private float tiempo_cambio;

    // Start is called before the first frame update
    void Start()
    {
        tiempo_cambio = Time.time + 1;
        rojo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempo_cambio < Time.time)
        {
            tiempo_cambio = Time.time + 0.5f;
            rojo.SetActive(!rojo.activeSelf);
            azul.SetActive(!rojo.activeSelf);
        }
    }
}
