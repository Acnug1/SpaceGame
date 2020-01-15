using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotation;
    public float minSpeed, maxSpeed; // Значения скорости полета астероида

    public float ShieldDropChance; // Переменная шанса выпадения щита

    public GameObject asteroidExplosion; // Объявляем игровой объект взрыв астероида
    public GameObject playerExplosion; // Объявляем игровой объект взрыв игрока

    public GameObject Shield; // Объект щита, который дропается

    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotation; // Возвращает случайный угол вращения для угловой скорости астероида

        asteroid.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed); // Задаем полет астероида вниз со случайной скоростью

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    // Будет вызван при столкновении с другим объектом (other), у которого тоже есть коллайдер
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "Asteroid" || other.tag == "Shield" || other.tag == "BossShip" || other.tag == "BossShield")
        {
            return;
        }

        Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся астероид (другой игровой объект)
        Destroy(gameObject); // Уничтожение самого астероида (текущий игровой объект)

        Instantiate(asteroidExplosion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции астероида

        if (other.tag == "Player")
        {
            // Взорвать игрока
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }
        else
        {
            gameController2Script.increaseScore(10); //Начисляем очки за уничтожение объекта
        }

        if (Random.Range(0, 100) <= ShieldDropChance)
        {
            Instantiate(Shield, transform.position, Quaternion.identity); // Создаем щит со случайной вероятностью от 1 до 100 из астероида
        }
    }
}
