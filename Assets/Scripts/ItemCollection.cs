using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int scoreValue = 1; // Количество очков, которые предмет добавляет

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // Получаем скрипт управления счетчиком игрока
            PlayerScoreController playerScoreController = collision.GetComponent<PlayerScoreController>();

            if (playerScoreController != null)
            {
                // Увеличиваем счетчик игрока
                playerScoreController.IncreaseScore(scoreValue);

                // Уничтожаем предмет после сбора
                Destroy(gameObject);
            }
        }
    }
}