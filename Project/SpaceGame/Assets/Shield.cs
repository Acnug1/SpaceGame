using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject PlayerShield;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.back * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary") || other.CompareTag("BossShip") || other.CompareTag("BossShield")) // Иконка щита не должна пересекаться с границей игровой зоны или кораблем босса
        {
            return;
        }

        if (other.CompareTag("Player")) // Если игрок подбирает иконку щита
        {
            Destroy(gameObject); // Уничтожаем иконку щита
            Instantiate(PlayerShield, other.transform.position, Quaternion.identity); // Создаем щит над игроком
        }
    }
}
