using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject Asteroid, Asteroid2, Asteroid3;
    public GameObject Enemy, Enemy2, Enemy3;

    public float minDelay, maxDelay; // Задаем минимальную и максимальную задержку запуска объектов

    private float nextLaunch; // Время следующего запуска астероида

    // Start is called before the first frame update
    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    void Start()
    {
        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Update is called once per frame
    void Update()
    {
        if (! gameController2Script.getIsStarted()) // Проверяем, если игра не начата
        {
            return; // Не запускаем астероиды и противников
        }

        if (Time.time > nextLaunch)
        {
            // ПОРА!
            nextLaunch = Time.time + Random.Range(minDelay, maxDelay);

        var halfWidth = transform.localScale.x / 2; // Находим границы появления астероидов на оси X
        var positionX = Random.Range(-halfWidth, halfWidth);    // Выбираем случайную позицию появления астероида от половины размера нашей границы

        var newAsteroidPosition = new Vector3(positionX, transform.position.y, transform.position.z); // Задаём новую позицию астероида

            // Создаем случайные астероиды
            int random = Random.Range(1, 7); // Левая граница включается, правая исключается. Так работает Random.Range(). Проверка от 1 до 6
            switch (random)
            {
                case 1:
                    Instantiate(Asteroid, newAsteroidPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Asteroid2, newAsteroidPosition, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Asteroid3, newAsteroidPosition, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(Enemy, newAsteroidPosition, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(Enemy2, newAsteroidPosition, Quaternion.Euler(0, 180, 0));
                    break;
                case 6:
                    Instantiate(Enemy3, newAsteroidPosition, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }
}