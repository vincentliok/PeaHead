using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource src;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        src = GetComponent<AudioSource>();
    }
}
