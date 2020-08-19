
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject LazerGun; // Объявляем объект лазерное оружие
    public GameObject LazerGun2;
    public GameObject LazerGunSmall;
    public GameObject LazerGunSmall2;
    public GameObject LazerShotGreen; // Объявляем объект лазерный выстрел
    public GameObject LazerShotBlue;
    public GameObject LazerShotBlue2;
    public float speed; // Публичная переменная (Отображается в Unity)
    public float tilt; // Наклон
    public float xMin, xMax, zMin, zMax; // Ограничить движение

    public float LazerDelay; // Задержка между выстрелами
    private float nextShot; // Время следующего выстрела

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    Rigidbody ship;

    // Start is called before the first frame update
    void Start()
    {
        // Кусок кода вызывается при создании объекта
        ship = GetComponent<Rigidbody>(); // Задаем для корабля твердое тело

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    {
        // Вызывается на каждый кадр
        if (!gameController2Script.getIsStarted()) // Проверяем, если игра не начата
        {
            return; // Не появляется игрок
        }

        // 1. Узнать куда хочет лететь игрок
        var moveHorizontal = Input.GetAxis("Horizontal"); // Куда игрок хочет лететь по горизонтали, -1 ... +1
        var moveVertical = Input.GetAxis("Vertical");

        // 2. Полететь туда
        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        // 3. Наклон корабля
        ship.rotation = Quaternion.Euler(moveVertical * tilt, 0, -moveHorizontal * tilt);

        // 4. Сковываем движения

        var xPosition = Mathf.Clamp(ship.position.x, xMin, xMax); // Ограничивает величину (позицию корабля) между минимальным и максимальным значением
        var zPosition = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(xPosition, 0, zPosition);

        // Выстрел из основных орудий корабля
        if (Input.GetButton("Fire1") && Time.time > nextShot) // Проверяем, прошла ли в данный момент задержка перед выстрелом
        {
            Instantiate(LazerShotGreen, LazerGun.transform.position, Quaternion.identity); // Создание игрового объекта LazerShot в позиции LazerGun с нулевым углом поворота
            Instantiate(LazerShotGreen, LazerGun2.transform.position, Quaternion.identity);
            nextShot = Time.time + LazerDelay; // Время следующего выстрела (текущее время + величина задержки для выстрела)
        }

        // Выстрел из вспомогательных орудий корабля
        if (Input.GetButton("Fire2") && Time.time > nextShot)
        {
            Instantiate(LazerShotBlue, LazerGunSmall.transform.position, Quaternion.Euler(0, 45, 0)); // Задаем угол поворота для левого и правого орудия корабля
            Instantiate(LazerShotBlue2, LazerGunSmall2.transform.position, Quaternion.Euler(0, -45, 0));
            nextShot = Time.time + LazerDelay / 2; // Время следующего выстрела в 2 раза чаще
        }
    }
}
