using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyState : MonoBehaviour
{

    public float min_difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (min_difficulty > DifficultyController.instance.GetDificultad()) {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
