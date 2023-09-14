using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void StartGame(int numPlayers)
    {
        // Сохраняем количество игроков в PlayerPrefs
        PlayerPrefs.SetInt("NumPlayers", numPlayers);
       // PlayerPrefs.Save();
        
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.enabled = false;
        }
        
        // Переходим на сцену SampleScene
        SceneManager.LoadSceneAsync("SampleScene");
        
    }
}
