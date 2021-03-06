﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazerScript : MonoBehaviour
{
    public GameObject playerExplotion; // Объявляем игровой объект взрыв игрока
    public GameObject lazerExplotion;
    public GameObject lazerExplotionSmall;

    public float EnemyShotSpeed; // Скорость выстрела

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.back * EnemyShotSpeed; // запускаем выстрел
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "EnemyShip" || other.tag == "Shield" || other.tag == "BossShip" || other.tag == "BossShield")
        {
            return;
        }

        Destroy(gameObject); // Уничтожение текущего игрового объекта     

        if (other.tag == "Player")
        {
            // Взорвать игрока
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции игрока
        }

        if (other.tag == "PlayerLazer")
        {
            // Взорвать большой лазер
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(lazerExplotion, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции большого лазера
        }

        if (other.tag == "SmallPlayerLazer")
        {
            // Взорвать маленький лазер
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(lazerExplotionSmall, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции маленького лазера
        }

        if (other.tag == "EnemyLazer")
        {
            // Взорвать большой лазер
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(lazerExplotion, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции вражеского лазера
        }
    }
}
