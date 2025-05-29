using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateIfBehindPlayer : MonoBehaviour
{
    public float deactivateDistance = 30f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null && (transform.position.z < player.position.z - deactivateDistance))
        {
            gameObject.SetActive(false);
        }
    }
}
