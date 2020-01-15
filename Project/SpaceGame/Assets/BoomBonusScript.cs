using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBonusScript : MonoBehaviour
{
    public float speed; // Задаем скорость полета бомбы
    public float rotation; // Задаем вращение для бомбы

    public GameObject playerExplotion; // Объект взрыва игрока
    public GameObject BombExplotion; // Объект взрыва бомбы

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody BoomBonus = GetComponent<Rigidbody>(); // Задаем твердое тело для бомбы
        BoomBonus.angularVelocity = Random.insideUnitSphere * rotation; // Возвращает случайный угол вращения для угловой скорости бомбы

        BoomBonus.velocity = Vector3.back * speed; // Задаем полет бомбы вниз
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary") || other.CompareTag("BossShip") || other.CompareTag("BossShield")) // Иконка бомбы не должна пересекаться с границей игровой зоны или кораблем босса
        {
            return;
        }

        Destroy(gameObject); // Уничтожаем бомбу и объекты, которые к ней прикоснутся
        Destroy(other.gameObject);

        Instantiate(BombExplotion, transform.position, Quaternion.identity);
        
        if (other.CompareTag("Player")) // Если игрок задевает бомбу
        {
            Destroy(gameObject); // Уничтожаем иконку бомбы

            // Взорвать игрока
            Destroy(other.gameObject);
            Instantiate(BombExplotion, transform.position, Quaternion.identity);
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity);
        }
    }
}
