using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Text deaths;

    private void Start()
    {
        deaths.text = $"Deaths: {PlayerStats.Deaths}";
    }

    private void OnDestroy()
    {
        PlayerStats.Reset();
    }
}
