using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class EnemyDiagonal : MonoBehaviour
{
    public float targetZ;
    public float maxTargetX;
    public float minTargetX;
    public float minSpeed; // Задаем скорость корабля противника
    public float maxSpeed;

    public GameObject LazerGunEnemy; // Объявляем объект лазерное оружие
    public GameObject LazerShotEnemy; // Объявляем объект лазерный выстрел

    public float LazerDelay; // Задержка между выстрелами
    private float nextShot; // Время следующего выстрела

    public GameObject enemyExplotion; // Объект взрыва противника
    public GameObject playerExplotion; // Объект взрыва игрока

    GameObject player;

    Rigidbody EnemyShip;

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    private GameObject playerShip;
    public float enemyShotSpeed;
    public Vector3 direction;
    private GameObject tempShot;

    // Start is called before the first frame update
    void Start()
    { // Код при создании объекта
        // Скопировал логику из EmitterScript
        var positionX = Random.Range(minTargetX, maxTargetX);
        var newTargetPosition = new Vector3(positionX, transform.position.y, targetZ); // Сделали новую позицию

        EnemyShip = GetComponent<Rigidbody>(); // Задаем для корабля твердое тело
        EnemyShip.transform.LookAt(newTargetPosition);
        EnemyShip.velocity = transform.forward * Random.Range(minSpeed, maxSpeed);

        // Ищем игрока
        player = GameObject.FindGameObjectWithTag("Player");

        playerShip = GameObject.Find("Player");

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    { // Вызывается на каждый кадр
        if (player) // Если игрок жив
        {
            // Поворачиваемся в сторону игрока
            // Если вычесть из координат нужной точки координаты текущей точки, то мы получим направление из текущей точки
            EnemyShip.transform.LookAt(player.transform.position);
        }
        //Стреляем по КД
        if (Time.time > nextShot) // Кнопка нажата и время прошло больше, чем в переменной
        {      
            if (playerShip) // Если игрок жив стреляем в игрока
            {
                tempShot = Instantiate(LazerShotEnemy, LazerGunEnemy.transform.position, LazerGunEnemy.transform.rotation);
                direction = playerShip.transform.position - tempShot.transform.position;
                tempShot.gameObject.transform.rotation = Quaternion.LookRotation(direction);
                direction.Normalize();
                tempShot.GetComponent<Rigidbody>().velocity = direction * enemyShotSpeed;
            }
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
    }
}
