using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMouse : MonoBehaviour // Привязываем данный скрипт к основной камере игрока
{
    protected GameController2Script gameController2Script; // Задаем переменную gameController2Script
    public GameObject menu;

    bool isLocked; // Создаем булевую переменную

    // Start is called before the first frame update
    void Start()
    {
        SetCursorLock (true); // По умолчанию делаем значение булевой переменной внутри функции isLocked = true для снятия фиксации курсора в центре экрана и его видимости

        gameController2Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController2Script>(); // Ищем по тегу "GameController" компонент GameController2Script объекта GameController2Script и записываем данный скрипт в переменную
    }

    void SetCursorLock(bool isLocked) // Создаем функцию для работы с курсором мыши
    {
        this.isLocked = isLocked; // Текущей переменной функции присвоим значение глобальной переменной isLocked, для того, чтобы мы могли менять её в зависимости от значения глобальной переменной
        Screen.lockCursor = !isLocked; // Снимаем фиксацию с курсора мыши в центре экрана
        Cursor.visible = isLocked; // По умолчанию курсор мыши видимый
    }


    // Update is called once per frame
    void Update()
    {
        if (menu.activeSelf == false) // Проверяем, если меню скрыто
        {
            SetCursorLock(false); //  Делаем курсор невидимым и фиксируем курсор мыши в видимой на камеру игрока части экрана по центру, когда курсор мыши скрыт
        }           
    }
}
