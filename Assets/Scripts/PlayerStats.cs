using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum PlayMode
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    };

    public static int Deaths { get; set; } = 0;

    public static float TimeSpent { get; set; } = 0.0f;

    public static float RespawnRate { get; set; } = 2.0f;

    public static float EnemySpeed { get; set; } = 3.0f;

    public static void Reset()
    {
        Deaths = 0;
        TimeSpent = 0;
    }

    public static void SetDifficulty(PlayMode difficulty)
    {
        switch (difficulty)
        {
            case PlayMode.Easy:
                RespawnRate = 2.0f;
                EnemySpeed = 3.0f;
                break;
            case PlayMode.Medium:
                RespawnRate = 1.0f;
                EnemySpeed = 4.0f;
                break;
            case PlayMode.Hard:
                RespawnRate = 0.8f;
                EnemySpeed = 6.0f;
                break;
            default:
                break;
        }
    }
}
