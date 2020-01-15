using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class BossScript : MonoBehaviour
{
    public float minSpeed; // Задаем скорость корабля противника
    public float maxSpeed;

    public GameObject LazerGunBoss1; // Объявляем объект лазерное оружие
    public GameObject LazerGunBoss2;
    public GameObject MissileGun; // Объявляем объект ракетная пушка
    public GameObject LeftSmallLazerGun; // Левая пушка
    public GameObject RightSmallLazerGun; // Правая пушка
    public GameObject LazerShotBoss; // Объявляем объект лазерный выстрел
    public GameObject MissileShotBoss; // Объявляем объект ракетный выстрел
    public GameObject LazerShotBossDiagonal; // Объявляем объект лазерный выстрел из левого бокового орудия
    public GameObject LazerShotBossDiagonal2; // Объявляем объект лазерный выстрел из правого бокового орудия
    public GameObject BossShield; // Объект щит босса

    public float smallLazerAngle; // Угол под которым будут стрелять маленькие лазеры

    public float LazerDelay; // Задержка между выстрелами
    public float MissileDelay; // Задержка между ракетными выстрелами
    private float nextShot; // Время следующего выстрела
    private float nextShotMissile; // Время следующего выстрела ракетой

    public GameObject BossExplotion; // Объект взрыва противника
    public GameObject playerExplotion; // Объект взрыва игрока
    public float xMin, xMax, zMin, zMax; // Ограничить движение

    GameObject player; // Создаем пустой объект для записи в него найденного объекта "Player", т.е. "Игрок"

    Rigidbody BossShip; // Задаем твердое тело для корабля босса

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script


    // Start is called before the first frame update
    void Start()
    { // Код при создании объекта
        // Скопировал логику из EmitterScript
        BossShip = GetComponent<Rigidbody>(); // Задаем для корабля твердое тело

        player = GameObject.FindGameObjectWithTag("Player"); // Ищем объект "Игрок"

        Instantiate(BossShield, BossShip.transform.position, Quaternion.identity); // Создаем щит над боссом

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    { // Вызывается на каждый кадр


        if (player.transform.position.x > BossShip.transform.position.x) // Если позиция игрока правее позиции босса
        {
            BossShip.velocity = -BossShip.transform.right * Random.Range(minSpeed, maxSpeed); // Двигаем босса в сторону игрока
        }
        if (player.transform.position.x <= BossShip.transform.position.x) // Если позиция игрока левее или равна позиции босса
        {
            BossShip.velocity = BossShip.transform.right * Random.Range(minSpeed, maxSpeed); // Двигаем босса в сторону игрока
        }

        // Сковываем движения
        var xPosition = Mathf.Clamp(BossShip.position.x, xMin, xMax); // Ограничивает величину (позицию корабля) между минимальным и максимальным значением
        var zPosition = Mathf.Clamp(BossShip.position.z, zMin, zMax);
        BossShip.position = new Vector3(xPosition, 0, zPosition);

        //Стреляем по КД
        if (Time.time > nextShot) // Кнопка нажата и время прошло больше, чем в переменной
        {
            Instantiate(LazerShotBoss, LazerGunBoss1.transform.position, LazerGunBoss1.transform.rotation); // Стреляем из основных орудий
            Instantiate(LazerShotBoss, LazerGunBoss2.transform.position, LazerGunBoss2.transform.rotation);

            Quaternion LeftLazerRotation = Quaternion.Euler(0, smallLazerAngle, 0); // Задаем угол для вспомогательных орудий
            Quaternion RightLazerRotation = Quaternion.Euler(0, -smallLazerAngle, 0);

            Instantiate(LazerShotBossDiagonal, LeftSmallLazerGun.transform.position, LeftLazerRotation); // Стреляем вспомогательными орудиями
            Instantiate(LazerShotBossDiagonal2, RightSmallLazerGun.transform.position, RightLazerRotation);

            nextShot = Time.time + LazerDelay;
        }

        if (Time.time > nextShotMissile) // Кнопка нажата и время прошло больше, чем в переменной
        {
            Instantiate(MissileShotBoss, MissileGun.transform.position, MissileGun.transform.rotation); // Стреляем из ракетного орудия

            nextShotMissile = Time.time + MissileDelay;
        }

    }

    // Будет вызван при столкновении с другим объектом
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "EnemyLazer" || other.tag == "Shield"
            || other.tag == "EnemyShip" || other.tag == "Asteroid" || other.tag == "BossShield"
            || other.tag == "Bomb" || other.tag == "MissileBoss")
        {
            return;
        }
 
        Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся вражеский корабль (другой игровой объект)

        if (other.tag == "PlayerLazer" || other.tag == "SmallPlayerLazer")
        {
            Destroy(gameObject); // Уничтожение текущего игрового объекта  
            Destroy(other.gameObject); // Уничтожение объекта с которым столкнулся босс

            Instantiate(BossExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции противника
            gameController2Script.increaseScore(50); //Начисляем очки за уничтожение объекта
        }

        if (other.tag == "Player")
        {
            // Взорвать игрока
            Destroy(gameObject); // Уничтожение текущего игрового объекта  
            Destroy(other.gameObject); // Уничтожение объекта с которым столкнулся босс

            Instantiate(BossExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции противника
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity);
        }          
    }
}
