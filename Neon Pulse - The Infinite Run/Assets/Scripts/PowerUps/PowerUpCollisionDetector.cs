using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollisionDetector : MonoBehaviour
{
    float stopTime;

    // Start is called before the first frame update
    void Start()
    {
        stopTime = Time.time + 1;
        /**GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(1f, 0.4f, 5f);
        cube.transform.parent = transform;
        cube.transform.localPosition = Vector3.zero;*/
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool contacto = Physics.BoxCast(transform.position, new Vector3(1f, 0.4f, 5f), Vector3.up, out hit, transform.rotation, 5, 1 << 0, QueryTriggerInteraction.Collide);
        //Debug.Log(hit.collider != null ? hit.collider.name : "No toque nada");
        if (contacto)
        {
            Destroy(gameObject);
        }
        if(stopTime < Time.time)
        {
            Destroy(this);
        }
    }
}
