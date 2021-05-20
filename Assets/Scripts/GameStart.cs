using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Start()
    {
        PlayerStats.Reset();
    }

    public void SetDifficulty(int option)
    {
        PlayerStats.SetDifficulty((PlayerStats.PlayMode) option);
    }
}
