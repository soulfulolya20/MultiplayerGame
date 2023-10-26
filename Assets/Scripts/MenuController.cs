using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button oldGameButton;
    public Button localButton;
    public GameObject playerButtonsGroup; // Содержит кнопки 1player, 2player, 3player, 4player
   

    private void Start()
    {
        // При старте, активируем кнопки old game и local, остальные группы скрываем
        oldGameButton.interactable = true;
        localButton.interactable = true;
        playerButtonsGroup.SetActive(false);
    }

    public void OnClickOldGame()
    {
        // По нажатию кнопки "Old Game" скрываем кнопки их объектами
        oldGameButton.gameObject.SetActive(false);
        localButton.gameObject.SetActive(false);

        // Показываем группу с кнопками выбора игроков
        playerButtonsGroup.SetActive(true);
    }

    public void OnClickLocal()
    {
        // По нажатию кнопки "Local" скрываем кнопки их объектами
        localButton.gameObject.SetActive(false);
        oldGameButton.gameObject.SetActive(false);

        // Показываем группу с кнопками server, host, client
        SceneManager.LoadScene("LocalGameScene");
    }

}