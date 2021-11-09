using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 1) * speed; // Выстрел в правый верхний угол под углом 45 градусов
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
