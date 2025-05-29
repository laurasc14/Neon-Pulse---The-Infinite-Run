using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController instance;

    private int botellas_total_generadas = 0;
    private int botellas_total_recogidas = 0;

    private float dificultat_actual;

    public delegate void DifficultyChanged(float newDifficulty);
    public static event DifficultyChanged OnDifficultyChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dificultat_actual = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (botellas_total_generadas > 0) {
            float proporcion_recogidas = (botellas_total_recogidas * 1f / botellas_total_generadas)*100;
            if (proporcion_recogidas >= 70) {
                dificultat_actual += Time.deltaTime;
                if (dificultat_actual > 1) dificultat_actual = 1;
                //Debug.Log("Dificultad "+dificultat_actual+ " " + proporcion_recogidas + " " + botellas_total_generadas + " " + botellas_total_recogidas);
            }
            if (proporcion_recogidas > 30 && proporcion_recogidas < 70)
            {
                if (dificultat_actual < 0.5f)
                {
                    dificultat_actual += Time.deltaTime;
                    if (dificultat_actual > 0.5f) dificultat_actual = 0.5f;
                }
                else if (dificultat_actual > 0.5f) {
                    dificultat_actual -= Time.deltaTime;
                    if (dificultat_actual < 0.5f) dificultat_actual = 0.5f;
                }
                //Debug.Log("Dificultad " + dificultat_actual + " " + proporcion_recogidas + " " + botellas_total_generadas + " " + botellas_total_recogidas);
            }
            if (proporcion_recogidas <= 30){
                dificultat_actual -= Time.deltaTime;
                if (dificultat_actual < 0) dificultat_actual = 0;
                //Debug.Log("Dificultad " + dificultat_actual + " " + proporcion_recogidas + " " + botellas_total_generadas + " " + botellas_total_recogidas);
            }
        }
    }

    public float GetDificultad()
    {
        return dificultat_actual;
    }

    public void  AddBottellaTotal() {
        botellas_total_generadas++;
        StartCoroutine(restar_bottella_total());
    }
    public void AddBottellaRecogida() {   
        botellas_total_recogidas++;
        StartCoroutine(restar_bottella_recogida());
    }

    void RestarBottellaTotal() {
        botellas_total_generadas--;
    }

    void RestarBottellaRecogida() {
        botellas_total_recogidas--;
    }

    IEnumerator restar_bottella_total() {
        yield return new WaitForSeconds(20);
        RestarBottellaTotal();

    }

    IEnumerator restar_bottella_recogida() {
        yield return new WaitForSeconds(30);
        RestarBottellaRecogida();
    }
}
