using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript2 : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed; // Короткая запись Vector3(0, 0, 1)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
