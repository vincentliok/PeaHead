using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public int numPoints;
    public bool reverseLoop;
    public Vector2 a;
    public Vector2 b;
    public Vector2 c;
    public Vector2 d;

    public Sprite idle;
    public Sprite patrolling;

    private Vector2[] positions;
    private int index = 0;
    private int step = 1;
    private bool spriteSet;

    private void Awake()
    {
        positions = new Vector2[4] { a, b, c, d };
        transform.position = positions[0];
        spriteSet = false;
    }

    private void Update()
    {
        // Set sprite depending on idle or patrolling
        if (!spriteSet)
        {
            Sprite sprite = numPoints == 1 ? idle : patrolling;
            gameObject.ChangeSprite(sprite);
            spriteSet = true;
        }

        if (numPoints < 2) return;

        Vector3 goal = positions[(index + step) % numPoints];
        // Make enemy face direction of movement
        transform.right = goal - transform.position;
        transform.position = Vector2.Lerp(transform.position, goal, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, goal) < 0.01)
        {
            transform.position = goal;
            index += step;

            if (!reverseLoop)
            {
                if (index > numPoints - 1)
                {
                    index %= numPoints;
                }
            }
            else
            {
                if (index == numPoints - 1 || index == 0)
                {
                    step *= -1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            SoundManager.soundManager.PlaySound(SoundManager.Sfx.ZombieDeath);
            Destroy(gameObject);
        }
    }
}
