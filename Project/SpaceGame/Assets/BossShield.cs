using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private GameObject Boss; // Запишем в новый объект корабль босса

    public GameObject playerExplotion; // Объект взрыва игрока

    public int shieldHealth; // Переменная здоровье щита

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("BossShip"); // Ищем объект "Босс"
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Boss.transform.position; // Текущее положения щита равно текущему положению босса
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary") || other.CompareTag("Shield") 
            || other.CompareTag("Asteroid") || other.CompareTag("EnemyLazer")
            || other.CompareTag("EnemyShip") || other.CompareTag("Bomb") || other.CompareTag("MissileBoss"))  // Щит не должен пересекаться с границей игровой зоны и другими вражескими объектами
        {
            return;
        }

        if (other.tag == "PlayerLazer" || other.tag == "SmallPlayerLazer")
        {
            Destroy(other.gameObject); // Уничтожение объекта с которым столкнулся щит
            shieldHealth -= 1; // Уменьшаем здоровье щита на единицу
            if (shieldHealth == 0) // Если здоровье щита равно 0
            {
                Destroy(gameObject); // Уничтожаем щит
            }
        }

        if (other.tag == "Player")
        {
            // Взорвать игрока
            shieldHealth -= 1; // Уменьшаем здоровье щита на единицу
            if (shieldHealth == 0) // Если здоровье щита равно 0
            {
                Destroy(gameObject); // Уничтожаем щит
            }

            Destroy(other.gameObject); // Уничтожение объекта с которым столкнулся босс
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции игрока
        }          
    }
}
