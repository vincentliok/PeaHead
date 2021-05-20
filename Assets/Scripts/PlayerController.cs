using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject pistolBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject sniperBulletPrefab;

    public float pistolBulletSpeed;
    public float pistolRecoil;

    public float shotgunBulletSpeed;
    public float shotgunRecoil;
    public int numPellets;
    public float coneAngle;

    public float sniperBulletSpeed;
    public float sniperRecoil;

    public bool won;
    public bool dead;

    public bool hasKey;

    public string gun;

    private Rigidbody2D rb;
    private Vector2 goal;

    private bool startTimer;
    private float timePassed;

    public Animator animator;

    private const int STATE_PEA = 0;
    private const int STATE_GRAPE = 1;
    private const int STATE_PEANUT = 2;

    private int currentState;

    public bool shootDisabled;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        won = false;
        dead = false;
        gun = "Pistol";
        hasKey = false;
        startTimer = false;
        currentState = STATE_PEA;
        shootDisabled = false;
    }

    private void Update()
    {
        if (dead) return;

        if (won)
        {
            transform.position = Vector2.Lerp(transform.position, goal, Time.deltaTime);
            return;
        }

        // Rotate player to face mouse
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootGun();
        }

        if (Input.GetMouseButtonDown(0) && !shootDisabled)
        {
            ShootGun();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun = "Pistol";
            ChangeState(STATE_PEA);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponUnlockData.shotgunUnlocked)
        {
            gun = "Shotgun";
            ChangeState(STATE_GRAPE);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponUnlockData.sniperUnlocked)
        {
            gun = "Sniper";
            ChangeState(STATE_PEANUT);
        }

        if (startTimer)
        {
            if (timePassed > 0.1f)
            {
                Text weaponUnlockText = GameObject.Find("Canvas").transform.Find("weaponUnlockText").GetComponent<Text>();
                weaponUnlockText.color -= new Color(0f, 0f, 0f, 0.1f);
                timePassed = 0f;
            }
            timePassed += Time.deltaTime;
        }
    }

    public void ChangeState(int state)
    {
        if (currentState == state) return;
        animator.SetInteger("weapon", state);
        currentState = state;
    }

    private void ShootGun()
    {
        // If the animator is currently playing a shooting state, do nothing
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("shooting")) return;
        switch (gun)
        {
            case "Pistol":
                ShootPistol();
                break;
            case "Shotgun":
                ShootShotgun();
                break;
            case "Sniper":
                ShootSniper();
                break;
        }
    }

    private void ShootPistol()
    {
        GameObject bullet = Instantiate(pistolBulletPrefab, transform);

        Quaternion rot = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z);
        bullet.transform.rotation = rot;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * pistolBulletSpeed);

        // Set position of bullet at mouth of gun
        bullet.transform.localPosition = Vector2.right * 0.55f;
        bullet.transform.parent = null;
        bullet.tag = "PlayerBullet";

        // Move the player backwards due to recoil
        rb.AddForce(-transform.right * pistolRecoil);

        SoundManager.soundManager.PlaySound(SoundManager.Sfx.Shoot);
        // Play shooting animation
        animator.SetTrigger("shoot_pea");
    }

    private void ShootShotgun()
    {
        float deviation = -(coneAngle / 2.0f);
        for (int i = 0; i < numPellets; i++)
        {
            float direction = transform.eulerAngles.z;
            direction += deviation;
            GameObject bullet = Instantiate(shotgunBulletPrefab, transform);

            Quaternion rot = Quaternion.Euler(0.0f, 0.0f, direction);
            bullet.transform.rotation = rot;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * shotgunBulletSpeed);

            // Set position of bullet at mouth of gun
            bullet.transform.localPosition = Vector2.right * 0.4f;
            bullet.transform.parent = null;
            bullet.tag = "PlayerBullet";

            deviation += coneAngle / (numPellets - 1);
        }
        rb.AddForce(-transform.right * shotgunRecoil);
        SoundManager.soundManager.PlaySound(SoundManager.Sfx.Shoot);

        // Play shooting animation
        animator.SetTrigger("shoot_grape");
    }

    private void ShootSniper()
    {
        GameObject bullet = Instantiate(sniperBulletPrefab, transform);

        Quaternion rot = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z);
        bullet.transform.rotation = rot;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * sniperBulletSpeed);

        // Set position of bullet at mouth of gun
        bullet.transform.localPosition = Vector2.right * 0.55f;
        bullet.transform.parent = null;
        bullet.tag = "PlayerBullet";

        // Move the player backwards due to recoil
        rb.AddForce(-transform.right * sniperRecoil);

        SoundManager.soundManager.PlaySound(SoundManager.Sfx.Shoot);

        // Play shooting animation
        animator.SetTrigger("shoot_peanut");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (won || dead) return;

        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.name.Contains("Hazards") || 
            collision.gameObject.tag == "EnemyBullet")
        {
            Die();
        }

        else if (collision.gameObject.name.Contains("World"))
        {
            SoundManager.soundManager.PlaySound(SoundManager.Sfx.HitWall);
        }

        else if (collision.gameObject.name.Contains("Key"))
        {
            SoundManager.soundManager.PlaySound(SoundManager.Sfx.Keypickup);
            hasKey = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("ShotgunPowerup"))
        {
            weaponUnlockData.shotgunUnlocked = true;
            Text weaponUnlockText = GameObject.Find("Canvas").transform.Find("weaponUnlockText").GetComponent<Text>();
            weaponUnlockText.enabled = true;
            weaponUnlockText.color = new Color(1f, 1f, 1f, 3f);
            weaponUnlockText.text = "Grapeshot Unlocked! Press 2 to equip!\n Press 1 to equip Peashooter!";
            startTimer = true;
            Destroy(collision.gameObject);

            WeaponDisplayController.weaponDisplay.ShowPea();
            WeaponDisplayController.weaponDisplay.ShowGrape();
        }
        else if (collision.gameObject.name.Contains("SniperPowerup"))
        {
            weaponUnlockData.sniperUnlocked = true;
            Text weaponUnlockText = GameObject.Find("Canvas").transform.Find("weaponUnlockText").GetComponent<Text>();
            weaponUnlockText.enabled = true;
            weaponUnlockText.color = new Color(1f, 1f, 1f, 3f);
            weaponUnlockText.text = "Peanutrator Unlocked! Press 3 to equip!";
            startTimer = true;
            Destroy(collision.gameObject);

            WeaponDisplayController.weaponDisplay.ShowPeanut();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Hazards"))
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerStats.Deaths++;
        dead = true;
        // Make player red
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = Color.red;
        }

        SoundManager.soundManager.PlaySoundWait(SoundManager.Sfx.Hit,
            () => LevelSelector.levelSelector.RestartLevel());
    }

    public void Win(Vector2 goalPos)
    {
        rb.velocity = Vector2.zero;
        won = true;
        goal = goalPos;

        SoundManager.soundManager.PlaySoundWait(SoundManager.Sfx.Score, () =>
        {
            LevelSelector.levelSelector.NextLevel();
        });
    }
}
