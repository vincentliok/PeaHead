using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector levelSelector;

    private void Start()
    {
        if (levelSelector)
            Destroy(levelSelector);

        levelSelector = this;
    }

    public void SelectLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        SelectLevel(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        if (nextLevel > weaponUnlockData.level) weaponUnlockData.level++;

        SceneManager.LoadScene(nextLevel);
    }

    private void Update()
    {
        // Reload current scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectLevel("Menu");
        }

        if (Input.GetKeyDown(KeyCode.Period)){
            NextLevel();
        }
    }
}
