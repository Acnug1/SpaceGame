using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    // Срабатывает при выходе объекта (other) за границы коллайдера
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
