using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float pistolRange;
    private float rangeTracker; //this is used to keep track of time

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        //these conditionals might need to be changed when converting to tilemap
        if (collision.gameObject.name.Contains("World") ||
            collision.gameObject.name.Contains("Player") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.name.Contains("World"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rangeTracker = 0;

    }

    private void Update()
    {
        if (rangeTracker > pistolRange)
        {
            Destroy(gameObject);
        }
        rangeTracker += Time.deltaTime;
    }
}

