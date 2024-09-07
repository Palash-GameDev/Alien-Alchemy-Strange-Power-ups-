using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float _flypower = 1f;
    [SerializeField] float _drag = 12f;

    [SerializeField] private int jumpCount = 1;

    float horizontalmove;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool isHavigWingsPower = false;
    [SerializeField] bool isMidAir = false;
    [SerializeField] bool isAntiGeavity = false;
    [SerializeField] private Rigidbody2D playerRb2d;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _Wings;
    [SerializeField] private GameObject _Parachute;
    public UiController uiController;


    // Start is called before the first frame update
    void Start()
    {
        playerRb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Flip();
        PlayerJump();

    }

    void PlayerMovement()
    {
        horizontalmove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("run_speed", Mathf.Abs(horizontalmove));
        playerRb2d.velocity = new Vector2(horizontalmove, playerRb2d.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void Flip()
    {
        if (isFacingRight && horizontalmove < 0f || !isFacingRight && horizontalmove > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {

                jumpCount = 1;
                playerRb2d.velocity = new Vector2(playerRb2d.velocity.x, jumpForce);

            }
            else if (isMidAir == true && !IsGrounded() && jumpCount > 0)
            {

                jumpCount = 1;
                playerRb2d.velocity = new Vector2(playerRb2d.velocity.x, jumpForce);
                jumpCount--;
            }

        }
        if (Input.GetButtonUp("Jump") && playerRb2d.velocity.y > 0f)
        {
            playerRb2d.velocity = new Vector2(playerRb2d.velocity.x, jumpForce * 0.25f);
        }
    }
    void UseWingPower()
    {
        GameObject wings = Instantiate(_Wings, new Vector3(this.transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
        wings.transform.SetParent(this.transform);


        if (isHavigWingsPower)
        {
            playerRb2d.gravityScale = 0;
            playerRb2d.velocity = new Vector2(horizontalmove, _flypower);
            Debug.Log(playerRb2d.velocity);
        }
        else
        {
            playerRb2d.gravityScale = 2;

        }
    }
    void UseAntiGeavity()
    {
        if (isAntiGeavity)
        {

            playerRb2d.gravityScale *= -1f;
            playerRb2d.rotation = 180f;
            jumpForce *= -1.15f;
        }
        else if (!isAntiGeavity)
        {
            playerRb2d.gravityScale = 2f;
            playerRb2d.rotation = 0f;
            jumpForce = 9f;

        }

    }

    void UseParachute()
    {
        GameObject Parachute = Instantiate(_Parachute, new Vector3(this.transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
        Parachute.transform.SetParent(this.transform);
        playerRb2d.drag = _drag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            uiController.CoinCollected();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("WingPower"))
        {
            isHavigWingsPower = true;
            Destroy(other.gameObject);
            UseWingPower();

        }
        if (other.gameObject.CompareTag("AntiGeavity"))
        {
            jumpCount = 1;
            isAntiGeavity = true;
            Destroy(other.gameObject);
            UseAntiGeavity();

        }
        if (other.gameObject.CompareTag("Gravity"))
        {
            jumpCount = 1;
            isAntiGeavity = false;
            Destroy(other.gameObject);
            UseAntiGeavity();

        }
        if (other.gameObject.CompareTag("ParachutePower"))
        {

            Destroy(other.gameObject);
            UseParachute();

        }
        if (other.gameObject.CompareTag("MidAirJump"))
        {
            isMidAir = true;
            Destroy(other.gameObject);
            PlayerJump();

        }
        if (other.gameObject.CompareTag("Spikes"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.CompareTag("Flag"))
        {
            if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }

            UnlockNewLevel();
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}// class
