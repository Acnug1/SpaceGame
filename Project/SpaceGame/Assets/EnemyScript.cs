using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class EnemyScript : MonoBehaviour
{
    public GameObject LazerGunEnemy; // Объявляем объект лазерное оружие
    public GameObject LazerShotEnemy; // Объявляем объект лазерный выстрел

    public float minSpeed; // Задаем скорость корабля противника
    public float maxSpeed;
    public float BoomBonusDropChance; // Переменная шанса выпадения бомбы

    public float LazerDelay; // Задержка между выстрелами
    private float nextShot; // Время следующего выстрела

    public GameObject enemyExplotion; // Объект взрыва противника
    public GameObject playerExplotion; // Объект взрыва игрока
    public GameObject BoomBonus; // Объект бомбы, который дропается

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody EnemyShip = GetComponent<Rigidbody>(); // Задаем для корабля твердое тело
        EnemyShip.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed); // Задаем направление и скорость корабля

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    {
        // Вызывается на каждый кадр

        // 1. Стреляем ко КД
        if (Time.time > nextShot) // Кнопка нажата и время прошло больше, чем в переменной
        {
            Instantiate(LazerShotEnemy, LazerGunEnemy.transform.position, LazerGunEnemy.transform.rotation);
            nextShot = Time.time + LazerDelay;
        }
    }
    // Будет вызван при столкновении с другим объектом
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "EnemyLazer" || other.tag == "Shield" || other.tag == "BossShip" || other.tag == "BossShield")
        {
            return;
        }

        Destroy(gameObject); // Уничтожение текущего игрового объекта   
        Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся вражеский корабль (другой игровой объект)

        Instantiate(enemyExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции противника

        if (other.tag == "Player")
        {
            // Взорвать игрока
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity);
        }
        else
        {
            gameController2Script.increaseScore(10); //Начисляем очки за уничтожение объекта
        }

        if (Random.Range(0, 100) <= BoomBonusDropChance)
        {
            Instantiate(BoomBonus, transform.position, Quaternion.identity); // Создаем бомбу со случайной вероятностью от 1 до 100 из астероида
        }
    }  
}
