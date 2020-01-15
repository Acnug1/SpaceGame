using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private GameObject PlayerShip; // Запишем в новый объект корабль игрока
        
    private int shieldHealth; // Переменная здоровье щита

    // Start is called before the first frame update
    void Start()
    {
        PlayerShip = GameObject.Find("Player"); // Ищем объект "Игрок"
        shieldHealth = 1; // Здоровье щита равно 1
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerShip.transform.position; // Текущее положения щита равно текущему положению игрока
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary")) // Щит не должен пересекаться с границей игровой зоны
        {
            return;
        }

        if (other.CompareTag("Shield")) // Если мы сталкиваемся с иконкой щита
        {
            Destroy(other.gameObject); // Уничтожаем иконку щита
            if (shieldHealth != 3) // Если здоровье щита не больше 3
            { 
                shieldHealth += 1; // Увеличиваем количество жизней, но не больше четырёх(одно здоровье корабля +3 щита)
            }
        }

        if (other.CompareTag("Asteroid") || other.CompareTag("EnemyLazer") || other.CompareTag("EnemyShip")
            || other.tag == ("BossShip") || other.tag == ("BossShield") || other.tag == ("Bomb") || other.tag == ("MissileBoss")) // Если сталкиваемся с враждебными объектами
        {
            shieldHealth -= 1; // Уменьшаем здоровье щита на единицу
            if (shieldHealth == 0) // Если здоровье щита равно 0
            { 
                Destroy(gameObject); // Уничтожаем щит
            }
        }
    }
}
