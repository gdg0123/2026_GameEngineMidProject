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


    float score;


    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private float moveInput;

    public bool canMove = true;

    public AudioClip Item1Sound;
    private AudioSource audioSource;
    public AudioClip Meow;

    private bool canDash = true;
    private bool isDashing = false;

    private bool Mujuck = false;
    private SpriteRenderer spriteRenderer;

    private bool Jumping = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        score = 0f;
    }


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

        if(Jumping)
        {
            jumpForce = 4.5f;
        }
        else
        {
            jumpForce = 3f;
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
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Meow;
            audioSource.PlayOneShot(Meow);

            DontDestroyOnLoad(Meow);

            HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);

            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy22"))
        {
            if (!Mujuck)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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

        if(collision.CompareTag("Item2"))
        {
            
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Item1Sound;
            audioSource.PlayOneShot(Item1Sound);

            moveSpeed = 3f;

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Item3"))
        {

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Item1Sound;
            audioSource.PlayOneShot(Item1Sound);

            Jumping = true;
            Invoke(nameof(ResetJump), 5f);

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ScoreItem"))
        {
            //아이템을 먹었을 때 점수를 올려줌
            score += 10f;

            Destroy(collision.gameObject);

        }
    }

    void ResetMujuck()
    {
        Mujuck = false;
    }

    void ResetJump()
    {
        Jumping = false;
    }

}
