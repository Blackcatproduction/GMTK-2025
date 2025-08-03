using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField]
    int baseDamage = 1;
    [SerializeField] 
    int damage = 1;


    [Header("Appearance")]
    [SerializeField]
    Sprite past;
    [SerializeField]
    Sprite present;
    [SerializeField]
    Sprite future;
    SpriteRenderer spriteRenderer;

    int bounces = 0;
    bool canHitPlayer = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int currentPeriod = GameController.controller.PlayerData.loopIndex % 3;

        switch (currentPeriod) {
            case 0:
                spriteRenderer.sprite = past;
                break;

            case 1:
                spriteRenderer.sprite = present;
                break;

            case 2:
                spriteRenderer.sprite = future;
                break;
        }
    }

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
            bounces += 1;

            if (bounces >= 3 && !canHitPlayer) {
                // Change layer to be able to hit player
                gameObject.layer = LayerMask.NameToLayer("Projectiles");
                canHitPlayer = true;

            }
        }
    }

    private void OnDisable() {
        damage = baseDamage;
    }
}
