using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBossScript : MonoBehaviour
{
    public GameObject playerExplotion; // Объявляем игровой объект взрыв игрока
    public GameObject MissileExplotion;

    public float EnemyShotSpeed; // Скорость выстрела

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.back * EnemyShotSpeed; // запускаем выстрел
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "Shield" || other.tag == "BossShip" || other.tag == "BossShield")
        {
            return;
        }

        Destroy(gameObject); // Уничтожение текущего игрового объекта     
        Instantiate(MissileExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции ракеты

        if (other.tag == "Player")
        {
            // Взорвать игрока
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(MissileExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции ракеты
            Instantiate(playerExplotion, other.transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции игрока
        }

        if (other.tag == "PlayerLazer" || other.tag == "SmallPlayerLazer" || other.tag == "EnemyLazer")
        {
            // Взорвать большой лазер
            Destroy(other.gameObject); // Уничтожение игрового объекта, с которым столкнулся лазер (другой игровой объект)
            Instantiate(MissileExplotion, transform.position, Quaternion.identity); // Создаём взрыв в текущей позиции ракеты
        }
    }
}
