using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlanesEmitter : MonoBehaviour
{
    public GameObject Enemy; // Задаем объект "Противник"

    public float Delay; // Задаем минимальную и максимальную задержку запуска объектов из эмиттера

    private float nextLaunch; // Время следующего запуска противника
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
            return; // Не запускаем противников
        }

        score = gameController2Script.increaseScore(increment); // Присваиваем в переменную количество очков игрока
        if ((score % 750 == 0 || score % 760 == 0 || score % 770 == 0) && score > 0 && Time.time > nextLaunch) // Если количество очков игрока кратно 1000, больше 0 и прошло достаточно времени после спавна
        {
            nextLaunch = Time.time + Delay; // Задаем минимальное время между спавнами противников
            var halfWidth = transform.localScale.x / 2; // Находим границы появления противников на оси Z
            var positionX = Random.Range(-halfWidth, halfWidth);    // Выбираем случайную позицию появления противников от половины размера нашей границы

            var newEnemyPosition = new Vector3(positionX, transform.position.y, transform.position.z); // Задаём новую позицию противников

            Instantiate(Enemy, newEnemyPosition, Quaternion.identity); // Создаем корабли противников
        }
    }
}
