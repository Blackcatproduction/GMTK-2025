using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Status")]
    [SerializeField]
    int health = 1;
    [SerializeField]
    int damage = 1;

    public int Damage { get => damage; set => damage = value; }

    PlayerController player;

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
    }

    public void TakeDamage(int damage, Vector2 direction) {
        //// set hurt animation
        //animator.SetTrigger("Hurt");

        // update health
        health -= damage;

        if (!IsAlive()) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }

    public bool IsAlive() {
        return health > 0;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Projectile")) {
            Projectile hitProjectile = collision.collider.GetComponent<Projectile>();

            TakeDamage(hitProjectile.Damage, transform.position - collision.transform.position);

            hitProjectile.Explode();
        }
    }
}
