using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCityScroller : MonoBehaviour
{
    public float loopDistance = 100f;
    public float despawnZ = -30f;

    void Update()
    {
        float speed = GameSpeedController.ScrollSpeed;
        transform.position -= Vector3.forward * speed * Time.deltaTime;

        if (transform.position.z < despawnZ)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + loopDistance
            );
        }
    }
}
