using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public float speed; // Задаем скорость бэкграунда

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Запоминаем стартовую позицию бэкграунда
    }

    // Update is called once per frame
    void Update()
    {
        var shift = Mathf.Repeat(Time.time * speed, 200); // Зацикливаем значение движения бэкграунда с определенной скоростью до достижения максимального значения (0...100)
        transform.position = startPosition + Vector3.back * shift;
    }
}
