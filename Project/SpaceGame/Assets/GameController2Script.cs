using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController2Script : MonoBehaviour
{
    public UnityEngine.UI.Text scoreTextElement; // Создаем поля текста, меню и кнопок

    public UnityEngine.UI.Text restartTextElement;

    public UnityEngine.UI.Text gameOverTextElement;

    public UnityEngine.UI.Button startButton;

    public UnityEngine.UI.Button exitButton;

    public GameObject menu;

    private bool isStarted = false; // По умолчанию задаем значение: "игра отключена"

    private int score = 0; // Задаем начальное значение очков

    GameObject Player; // Создаем локальный объект игрок

    public bool getIsStarted() // Возвращаем значение старта игры
    {
        return isStarted;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTextElement.text = "Score: 0"; // Создаем текстовый элемент для подсчета очков
        startButton.onClick.AddListener(delegate // Создаем действие при нажатии на кнопку
        {
            isStarted = true; // Запускаем игру
            menu.SetActive(false); // Скрываем меню
        });

            exitButton.onClick.AddListener(delegate // Создаем действие при нажатии на кнопку
        {
            Application.Quit(); // Выход из игры
        });

    }
    public int increaseScore(int increment) // Подсчитываем очки игрока
    {
        score += increment;
        scoreTextElement.text = "Score: " + score; // Записываем данные в игру в реальном времени
        return score;
    }

    public void Update()
    {
        if (Input.GetKey("escape"))
        { 
            Application.Quit(); 
        }

        Player = GameObject.FindGameObjectWithTag("Player"); // Ищем игрока по тэгу
        if (!Player) // Если игрок уничтожен
        {
                  isStarted = false; // Отключаем игру
                  restartTextElement.text = "Press 'R' for restart"; // Выводим текст на экран "Нажмите 'R' для рестарта"
                  gameOverTextElement.text = "Game Over"; // Выводим текст на экран "Игра окончена"

            if (Input.GetKeyDown(KeyCode.R)) // Если игрок уничтожен и нажата клавиша "R"
            {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single); // Загружаем сцену "SampleScene" заново. Single означает, что загружается только текущая сцена, все остальные будут закрыты. Если используется несколько сцен, между которыми нужно переключаться, то используем атрибут Additive
            }
        }
        
    }
}
