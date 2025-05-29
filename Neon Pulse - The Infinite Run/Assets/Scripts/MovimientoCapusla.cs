using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovimientoCapusla : MonoBehaviour
{
    public InputActionReference moveAction;
    private Rigidbody rb;

    public float velocity = 5;
    private float baseVelocity = 5;
    public float maxVelocity = 10;
    public float jumpForce = 30;

    public GameObject pies;
    public LayerMask suelo;

    float puntuacion = 0;

    private float jumpPowerDuration = 0;
    private bool jumpPowerActive = false;
    private bool estaSobreJumpPlatform = false;

    private bool isShieldActive = false;
    private float shieldDuration = 0f;

    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 0f;
    private float originalSpeed;

    private bool isMagnetActive = false;
    private float magnetDuration = 0f;
    public float magnetRange = 5f;
    public float magnetSpeed = 10f;

    [Header("Poli")]
    public GameObject policeCar;
    public float safeDistanceFromPolice = 10f;

    private bool isVulnerable = false;
    private bool isSlowedByObstacle = false;
    private bool isCaught = false;

    [Header("Vides")]
    public GameObject vida1UI;
    public GameObject vida2UI;

    private int videsRestants = 2;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = velocity;
        DifficultyController.OnDifficultyChanged += AdjustSpeedWithDifficulty;
        GameSpeedController.ScrollSpeed = 5f;
    }

    void Update()
    {
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>();
        moveDirection.y = 0;
        rb.velocity = moveDirection * velocity + new Vector3(0, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !estaSobreJumpPlatform)
        {
            rb.AddForce(Vector3.up * jumpForce * (jumpPowerActive ? 1.25f : 1f), ForceMode.Impulse);
        }

        puntuacion += 10 * Time.deltaTime;
        if (puntuacion > 0)
        {
            int puntuacionEntera = Mathf.FloorToInt(puntuacion);
            puntuacion -= puntuacionEntera;
            ScoreManager.instance.AddPoints(puntuacionEntera);
        }

        if (jumpPowerDuration < Time.time)
        {
            jumpPowerActive = false;
            jumpPowerDuration = 0;
        }

        if (isShieldActive && Time.time > shieldDuration)
            DeactivateShield();

        if (isSpeedBoostActive && Time.time > speedBoostDuration)
            DeactivateSpeedBoost();

        if (isMagnetActive && Time.time > magnetDuration)
            DeactivateMagnet();

        if (isMagnetActive)
            AttractCoins();

        // Si s’allunya molt, deixa de ser vulnerable
        if (policeCar != null && Vector3.Distance(transform.position, policeCar.transform.position) > safeDistanceFromPolice)
        {
            isVulnerable = false;
        }
    }

    private void OnDestroy()
    {
        DifficultyController.OnDifficultyChanged -= AdjustSpeedWithDifficulty;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTRA A OnTriggerEnter");
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            if (isShieldActive)
            {
                Destroy(other.gameObject);
            }
            else
            {
                if (!isSlowedByObstacle)
                {
                    videsRestants--;
                    GameSpeedController.ScrollSpeed *= 0.75f;
                    StartCoroutine(spawnPoli());
                    UpdateVidesUI();

                    if (videsRestants <= 0)
                    {
                        // Mor immediatament si no queden vides
                        FindObjectOfType<FlashScreen>().FlashAndRestart();
                        GameSpeedController.ScrollSpeed = 0;
                        return;
                    }

                    StartCoroutine(HandleObstacleCollision());
                }
                else
                {

                    FindObjectOfType<FlashScreen>().FlashAndRestart(); // Si torna a xocar ràpid, també mor

                    GameSpeedController.ScrollSpeed = 0;
                }
            }
        }

        if (other.gameObject.CompareTag("JumpPlatform"))
        {
            estaSobreJumpPlatform = true;
            rb.AddForce(Vector3.up * jumpForce * 1.2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPlatform"))
        {
            estaSobreJumpPlatform = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ENTRA A OnCollisionEnter");
        if (isCaught) return;

        if (collision.gameObject.CompareTag("PoliceCar"))
        {
            if (isVulnerable)
            {
                isCaught = true;
                StartCoroutine(HandleCaughtByPolice());
            }
            else
            {
                Debug.Log("El poli t'ha tocat però no estaves vulnerable.");
            }
        }

        
    }

    IEnumerator HandleObstacleCollision()
    {
        isVulnerable = true;
        isSlowedByObstacle = true;

        float original = velocity;
        velocity *= 0.5f;

        Vector3 knockback = -transform.forward * 5f;
        rb.AddForce(knockback, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        velocity = original;

        isVulnerable = (videsRestants == 1);

        isSlowedByObstacle = false;
    }

    IEnumerator HandleCaughtByPolice()
    {
        Debug.Log("El poli t'ha atrapat!");

        rb.velocity = Vector3.zero;
        velocity = 0;

        FindObjectOfType<FlashScreen>().FlashAndRestart();

        yield return null; // ja no cal esperar amb yield si el flaix ho fa
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f, suelo);
    }

    public void ActivatePowerJump(float duration)
    {
        jumpPowerDuration = Time.time + duration;
        jumpPowerActive = true;
    }

    public void DesactivatePowerJump()
    {
        jumpPowerActive = false;
    }

    public void ActivateShield(float duration)
    {
        isShieldActive = true;
        shieldDuration = Time.time + duration;
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
    }

    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            velocity *= multiplier;
            speedBoostDuration = Time.time + duration;
        }
    }

    public void DeactivateSpeedBoost()
    {
        isSpeedBoostActive = false;
        velocity = originalSpeed;
    }

    public void ActivateMagnet(float duration)
    {
        isMagnetActive = true;
        magnetDuration = Time.time + duration;
    }

    public void DeactivateMagnet()
    {
        isMagnetActive = false;
    }

    private void AttractCoins()
    {
        Collider[] coins = Physics.OverlapSphere(transform.position, magnetRange, LayerMask.GetMask("Coin"));
        foreach (Collider coin in coins)
        {
            if (coin.gameObject.CompareTag("Coin"))
            {
                Vector3 direction = (transform.position - coin.transform.position).normalized;
                coin.transform.position += direction * magnetSpeed * Time.deltaTime;
            }
        }
    }

    void AdjustSpeedWithDifficulty(float newDifficulty)
    {
        velocity = Mathf.Lerp(baseVelocity, maxVelocity, newDifficulty);
    }

    void UpdateVidesUI()
{
        Debug.Log("Vides restants: " + videsRestants);

        videsRestants = Mathf.Clamp(videsRestants, 0, 2);

        if (videsRestants == 1)
            vida2UI.SetActive(false);
        else if (videsRestants == 0)
            vida1UI.SetActive(false);

    }

    IEnumerator spawnPoli() {

        while(policeCar.transform.localPosition.z < 5.75)
        {
            policeCar.transform.localPosition += new Vector3(0, 0, 7)*Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}