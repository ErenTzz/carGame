using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSettingsClicked()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
            
            // Pause if panel was inactive (now active), Resume if panel was active (now inactive)
            Time.timeScale = !isActive ? 0 : 1;
        }
        else
        {
            Debug.LogWarning("Settings Panel reference is missing!");
        }
    }
}
