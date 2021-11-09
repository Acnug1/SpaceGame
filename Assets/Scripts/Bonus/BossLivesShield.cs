using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLivesShield : MonoBehaviour
{
    public GameObject BossShield; // Объект щит босса

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary")) // Иконка щита не должна пересекаться с границей игровой зоны
        {
            return;
        }

        if (other.CompareTag("BossShip")) // Если босс подбирает иконку щита
        {
            Destroy(gameObject); // Уничтожаем иконку щита
            Instantiate(BossShield, other.transform.position, Quaternion.identity); // Создаем щит над боссом
        }
    }
}
