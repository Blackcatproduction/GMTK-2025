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
            transform.localScale *= damage;
        } 
    }

    public void Explode() {
        // TODO Show special effects
        Destroy(gameObject);
    }

    private void OnDisable() {
        damage = baseDamage;
    }
}
