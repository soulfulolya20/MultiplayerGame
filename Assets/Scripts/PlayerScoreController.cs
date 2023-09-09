using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreController : MonoBehaviour
{
    public int playerID; // Уникальный идентификатор игрока
    public Text scoreText; // Ссылка на текстовое поле для отображения счета

    private int score = 0;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        // Ищем текстовое поле для отображения счета
        if (scoreText == null)
        {
            Debug.LogError("Не установлено текстовое поле для отображения счета!");
        }
    }

    public void IncreaseScore(int amount)
    {
        // Увеличиваем счет игрока на указанное количество
        score += amount;

        collectionSoundEffect.Play();
        // Обновляем отображение счета в текстовом поле
        UpdateScoreText();
    }

    private void UpdateScoreText() =>
        // Обновляем текстовое поле для отображения текущего счета
        scoreText.text = "Score: " + score;
}