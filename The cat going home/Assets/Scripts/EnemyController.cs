using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rb;
    private bool isMovingRight = true;

    private Animator eAni;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boundary"))
        {
            isMovingRight = !isMovingRight;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnMove()
    {
        
    }
}
