using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 1.8f;
    public float jumpForce = 3f;
    public float dashForce = 3f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private float moveInput;

    public bool canMove = true;

    public AudioClip Item1Sound;
    private AudioSource audioSource;

    private bool canDash = true;
    private bool isDashing = false;

    private bool Mujuck = false;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); }


    private void Update()
    {
        if (isDashing || !canMove)
            return;

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        pAni.SetBool("Walk", moveInput != 0);

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

        }
    }



    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing)
            return;



        if (collision.CompareTag("Respawn"))
        {
            if(!Mujuck)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if(collision.CompareTag("Enemy22"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (collision.CompareTag("Item1"))
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Item1Sound;
            audioSource.PlayOneShot(Item1Sound);

            Mujuck = true;
            Invoke(nameof(ResetMujuck), 3f);
            Destroy(collision.gameObject);
        }
    }

    void ResetMujuck()
    {
        Mujuck = false;
    }

}
