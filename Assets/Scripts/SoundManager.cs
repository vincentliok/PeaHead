using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sfx
    {
        Win,
        HitWall,
        Shoot,
        Click,
        Hit,
        Score,
        Keypickup,
        DoorUnlock,
        ZombieDeath
    }

    public static SoundManager soundManager;

    public AudioClip score;
    public AudioClip win;
    public AudioClip hitWall;
    public AudioClip shoot;
    public AudioClip click;
    public AudioClip hit;
    public AudioClip keypickup;
    public AudioClip doorUnlock;
    public AudioClip zombieDeath;

    private AudioSource src;

    private void Start()
    {
        if (soundManager)
            Destroy(soundManager);

        soundManager = this;

        src = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        if (Enum.TryParse(sound, out Sfx result))
        {
            PlaySound(result);
        }
    }

    public void PlaySound(Sfx sound)
    {
        switch (sound)
        {
            case Sfx.Win:
                src.PlayOneShot(win);
                break;
            case Sfx.HitWall:
                src.PlayOneShot(hitWall);
                break;
            case Sfx.Shoot:
                src.PlayOneShot(shoot);
                break;
            case Sfx.Click:
                src.PlayOneShot(click);
                break;
            case Sfx.Hit:
                src.PlayOneShot(hit);
                break;
            case Sfx.Score:
                src.PlayOneShot(score);
                break;
            case Sfx.Keypickup:
                src.PlayOneShot(keypickup);
                break;
            case Sfx.DoorUnlock:
                src.PlayOneShot(doorUnlock);
                break;
            case Sfx.ZombieDeath:
                src.PlayOneShot(zombieDeath);
                break;
            default:
                break;
        }
    }

    public void PlaySoundWait(Sfx sound, Action onFinish)
    {
        StartCoroutine(Play(sound, onFinish));
    }

    private IEnumerator Play(Sfx sound, Action onFinish)
    {
        PlaySound(sound);
        yield return new WaitWhile(() => src.isPlaying);
        onFinish();
    }

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        src = GetComponent<AudioSource>();
    }
}
