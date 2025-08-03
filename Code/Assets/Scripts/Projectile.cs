using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField]
    int baseDamage = 1;
    [SerializeField] 
    int damage = 1;

    public int Damage { get => damage; set { 
            damage = value;
            transform.localScale *= Mathf.Sqrt(damage);
        } 
    }

    public void Explode() {
        // TODO Show special effects
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Wall")) {
            // Change layer to be able to hit player
            gameObject.layer = LayerMask.NameToLayer("Projectiles");
        }
    }

    private void OnDisable() {
        damage = baseDamage;
    }
}
