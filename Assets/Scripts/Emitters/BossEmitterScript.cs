using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEmitterScript : MonoBehaviour
{
    public GameObject Boss; // Задаем объект "Босс"

    public float Delay; // Задаем минимальную и максимальную задержку запуска объектов из эмиттера

    private float nextLaunch; // Время следующего запуска босса
    private int increment; // Подставим данную переменную в функцию для записи количества очков
    private int score; // Запишем в данную переменную количество очков игрока

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    // Start is called before the first frame update
    void Start()
    {
        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController2Script.getIsStarted()) // Проверяем, если игра не начата
        {
            return; // Не запускаем босса
        }
           
              score = gameController2Script.increaseScore(increment); // Присваиваем в переменную количество очков игрока
              if ((score % 500 == 0 || score % 510 == 0 || score % 520 == 0) && score > 0 && Time.time > nextLaunch) // Если количество очков игрока кратно 1000, больше 0 и прошло достаточно времени после спавна
              {
                  nextLaunch = Time.time + Delay; // Задаем минимальное время между спавнами босса
            var halfHeight = transform.localScale.z / 2; // Находим границы появления босса на оси Z
            var positionZ = halfHeight;    // Выбираем позицию появления босса от половины размера нашей границы

            var newBossPosition = new Vector3(transform.position.x, transform.position.y, positionZ); // Задаём новую позицию босса

            Instantiate(Boss, newBossPosition, Quaternion.Euler(0, 180, 0)); // Поворачиваем корабль босса по оси Y
              }
    }
}
