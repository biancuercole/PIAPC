using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Transform player;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>().transform;
        body = GetComponent<Rigidbody2D>();

        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        Vector2 dirToPlayer = (player.position - transform.position).normalized;
        body.velocity = dirToPlayer * speed;
        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        float destroyTime = 5f;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            StartCoroutine(playerHealth.GetDamage(damage));
        }

        Destroy(gameObject);
    }
}
