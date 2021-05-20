using UnityEngine;

public class SpitterController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyBullet;

    public float shotCooldown;
    public float lastShotTime;
    public float bulletSpeed;
    public float spitterRange;

    public Sprite idle;
    public Sprite spitting;

    private enum SpitterState { Idle, Spitting }

    private SpitterState currentState;
    private SpitterState prevState;
    private Quaternion destination;
    private float angleRange;

    void Start()
    {
        lastShotTime = Time.time;
        currentState = SpitterState.Idle;
        prevState = currentState;
        destination = transform.rotation;
        angleRange = 120f;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentState)
        {
            case SpitterState.Idle:
                Idle();
                break;
            case SpitterState.Spitting:          
                if (Time.time - lastShotTime > shotCooldown)
                {
                    LookAt2D(transform, player.transform);
                    spit();
                    lastShotTime = Time.time;
                }
                break;
        }

        // If the player is in range and in the line of sight, start spitting
        if (Vector2.Distance(player.transform.position, transform.position) < spitterRange)
        {
            LookAt2D(transform, player.transform);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1000, ~LayerMask.NameToLayer("Default"));
            prevState = currentState;
            currentState = hit.transform.name == "Player" ? SpitterState.Spitting : SpitterState.Idle;
        }
        // Otherwise remain idle
        else
        {
            prevState = currentState;
            currentState = SpitterState.Idle;
        }
        if (prevState != currentState)
        {
            Sprite sprite = currentState == SpitterState.Idle ? idle : spitting;
            gameObject.ChangeSprite(sprite);
        }
    }

    private void Idle()
    {
        if (Quaternion.Angle(transform.rotation, destination) < 0.01f)
        {
            float angle = Random.Range(-angleRange, angleRange);
            // Have the enemy rotate C or CC a random amount
            destination = transform.rotation * Quaternion.Euler(0, 0, angle);
        }
        // Smoothly interpolate the rotation to the new angle 
        transform.rotation = Quaternion.Lerp(transform.rotation, destination, Time.deltaTime);
    }

    private void spit(){
        Debug.Log("pew pew");
        GameObject bullet = Instantiate(enemyBullet, transform);
        Vector2 size = bullet.transform.localScale;

        Quaternion rot = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z);
        bullet.transform.rotation = rot;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);

        // Set position of bullet at mouth of gun
        bullet.transform.localPosition = Vector2.right * 1.5f;
        bullet.transform.parent = null;
        bullet.tag = "EnemyBullet";
        bullet.transform.localScale = size;
    }

    public void LookAt2D(Transform transform, Transform target)
    {
        transform.right = target.position - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            SoundManager.soundManager.PlaySound(SoundManager.Sfx.ZombieDeath);
            Destroy(gameObject);
        }
    }
}
