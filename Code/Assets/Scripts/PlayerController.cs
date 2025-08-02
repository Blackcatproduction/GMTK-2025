using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] 
    int maxHealth;
    [SerializeField] 
    int health;

    //[SerializeField]
    //Image healthBar;

    [Header("Components")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Header("Combat")]
    //[SerializeField] float knockbackForce;
    //[SerializeField] float knockbackDuration;
    [SerializeField] CameraShake shakeEffect;
    [SerializeField] 
    int damageMultiplier = 1;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    bool isAbsorbing = false;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    public Vector2 MoveDirection { get => moveDirection; set {
            moveDirection = value;

            // Updates facing direction if changing movement direction not to stop
            if (moveDirection.x != 0 || moveDirection.y != 0) {
                facingDirection = moveDirection;
            }
        }
    }

    Vector2 facingDirection;
    //[SerializeField]
    //bool isInHitstun = false;
    float initialSpeed = 1f;

    //public bool facingLeft = true;
    //bool flipping = false;

    //[Header("Dash")]
    //[SerializeField]
    //float dashSpeed;
    //[SerializeField]
    //float dashLength = .5f;
    //[SerializeField]
    //float dashCooldown = 1f;
    //private float dashCounter, dashCoolCounter;

    public CameraShake ShakeEffect { get => shakeEffect; set => shakeEffect = value; }

    private void Start() {
        // Load data from game controller
        LoadPlayerAttributes(GameController.controller.PlayerData);
    }

    void LoadPlayerAttributes(PlayerDataSO data) {
        maxHealth = data.maxHealth;
        health = data.health;
    }

    void UnLoadPlayerAttributes(PlayerDataSO data) {
        Debug.Log("Unloaded");
        data.maxHealth = maxHealth;
        data.health = health;
    }

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        Run();

        //if (dashCounter > 0) {
        //    dashCounter -= Time.deltaTime;

        //    if (dashCounter < 0) {
        //        moveSpeed = initialSpeed;
        //        dashCoolCounter = dashCooldown;
        //    }
        //}

        //if (dashCoolCounter > 0) {
        //    dashCoolCounter -= Time.deltaTime;
        //}
    }

    void OnMove(InputValue value) {
        Debug.Log(value);
        if (/*gameStateManager.IsPaused() ||*/ !IsAlive()) {
            return;
        }

        MoveDirection = value.Get<Vector2>();
    }

    void OnFire(InputValue value) {
        if (/*gameStateManager.IsPaused() ||*/ !IsAlive() || isAbsorbing) {
            return;
        }

        // Create new projectile and fire to player's direction
        GameObject newProjectile = Instantiate(projectile, transform.parent);
        newProjectile.transform.position = (Vector2)transform.position + facingDirection * 1f;

        newProjectile.GetComponent<Rigidbody2D>().velocity = facingDirection * 10f;
        newProjectile.GetComponent<Projectile>().Damage = newProjectile.GetComponent<Projectile>().Damage * damageMultiplier;
    }

    void OnChangeAbsorb(InputValue value) {
        if (/*gameStateManager.IsPaused() ||*/ !IsAlive()) {
            return;
        }

        isAbsorbing = !isAbsorbing;
    }

    //void OnDash(InputValue value) {
    //    if (gameStateManager.IsPaused() || !IsAlive()) {
    //        return;
    //    }

    //    if (dashCoolCounter <= 0 && dashCounter <= 0) {
    //        StartCoroutine(Invulnerability());
    //        AkSoundEngine.PostEvent("playerDash", this.gameObject);
    //        moveSpeed = dashSpeed;
    //        dashCounter = dashLength;
    //    }
    //}

    //void OnPause(InputValue value) {
    //    gameStateManager.ProcessPause();
    //}

    void Run() {
        rb.velocity = moveSpeed * MoveDirection;

        //animator.SetBool("IsMoving", rb.velocity != Vector2.zero);
    }

    //public void FlipSprite() {
    //    //bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
    //    if (flipping) return;

    //    StartCoroutine(DoFlipSprite(spriteRenderer.transform));

    //    //if ((rb.velocity.x > 0.1f && facingLeft) || (rb.velocity.x < -0.1f && !facingLeft)) {
    //    //transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    //    //spriteRenderer.flipX = rb.velocity.x >= 0;
    //    //StartCoroutine(DoFlipSprite(spriteRenderer.transform));
    //    //}
    //}

    //IEnumerator DoFlipSprite(Transform sprite) {
    //    flipping = true; // semaphore

    //    // calc the direction of flip
    //    float currentScale = sprite.localScale.x;
    //    float goalScale = currentScale * -1;
    //    float increment = 0f;
    //    if (currentScale > 0) increment = -0.2f;
    //    else if (currentScale < 0) increment = 0.2f;

    //    // do the flip
    //    for (int i = 0; i < 10; i++) {
    //        sprite.localScale = new Vector3(sprite.localScale.x + increment, 1, 1);
    //        yield return new WaitForSeconds(0.02f);
    //    }

    //    // update direction
    //    facingLeft = !facingLeft;

    //    flipping = false;
    //}


    public bool IsAlive() {
        return health > 0;
    }

    public void TakeDamage(int damage, Vector2 direction) {
        // Can't take damage after winning
        if (EnemyController.controller.EnemyCount() == 0) {
            return;
        }

        //// set hurt animation
        //animator.SetTrigger("Hurt");

        // update health
        health -= damage;

        // update UI
        //healthBar.fillAmount = ((float)health / maxHealth);

        if (!IsAlive()) {
            Die();
        }
        else {
            // Shake camera
            shakeEffect.Shake(damage * 2, 0.5f);

            //// knockback
            //StartCoroutine(SufferHitStun());
            //rb.velocity = Vector2.zero;
            //rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }

    void Die() {
        // Disappear the player shadow and character
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
            sr.color = Color.clear;
        }

        // Disable script
        enabled = false;

        // Disable collider and velocity
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;

        GameController.controller.CallGameOverMenu();
    }

    public void EnableInput() {
        GetComponent<PlayerInput>().ActivateInput();
    }

    public void DisableInput() {
        GetComponent<PlayerInput>().DeactivateInput();
    }

    //IEnumerator SufferHitStun() {
    //    isInHitstun = true;

    //    blinkEffect.StartBlinking();

    //    yield return new WaitForSeconds(knockbackDuration);

    //    blinkEffect.StopBlinking();

    //    isInHitstun = false;
    //    rb.velocity = Vector2.zero;
    //}

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.IsAlive()) {
                // Get knockback direction
                Vector2 knockbackDirection = transform.position - collision.transform.position;
                TakeDamage(enemy.Damage, knockbackDirection.normalized);
            }

        }
        else if (collision.collider.CompareTag("Projectile")) {
            Projectile hitProjectile = collision.collider.GetComponent<Projectile>();
            // TODO Check if the projectile is enemies'
            if (isAbsorbing) {
                damageMultiplier += 1;
            } else {
                TakeDamage(hitProjectile.Damage, transform.position - collision.transform.position);

                // Reset damage multiplier
                damageMultiplier = 1;
            }

            hitProjectile.Explode();
        }
    }

    // Avoids player dying after wave over
    public void ActivateWaveOverInvincibility() {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnDestroy() {
        // Save player data to game controller, if player didn't die
        if (IsAlive()) {
            UnLoadPlayerAttributes(GameController.controller.PlayerData);
        }
    }
}
