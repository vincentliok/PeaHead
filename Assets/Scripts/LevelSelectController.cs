using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public float xSpacing = 100;
    public float ySpacing = 100;
    public int levelsPerRow = 5;

    private GameObject[] levels;

    private void Start()
    {
        levels = new GameObject[10];
        int levelOffset = 1;

        for (int i = 0; i < levels.Length / levelsPerRow; ++i)
        {
            for (int j = 0; j < levelsPerRow; ++j)
            {
                GameObject buttonObject = Instantiate(buttonPrefab, transform);

                float x = j * xSpacing;
                float y = i * -ySpacing;

                buttonObject.transform.localPosition = new Vector2(x, y);

                int level = levelsPerRow * i + j + 1;
                Text text = buttonObject.GetComponentInChildren<Text>();
                text.text = level.ToString();

                Button button = buttonObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    SoundManager.soundManager.PlaySound(SoundManager.Sfx.Click);
                    SceneManager.LoadScene(level + levelOffset);
                });

                if (level > weaponUnlockData.level)
                {
                    button.interactable = false;
                }
            }
        }

        // Center the grid
        float gridW = levelsPerRow * xSpacing;
        float gridH = levels.Length / levelsPerRow * ySpacing;
        transform.localPosition = new Vector2(-gridW / 2 + xSpacing / 2, gridH / 2 - ySpacing / 2);
    }
}
