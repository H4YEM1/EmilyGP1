using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Move;
    public float speed;
    public float originalSpeed;
    public float jump;
    public bool isFacingRight;
    public Animator anim;
    public bool isDashing;

    public CoinManager cm;
    // Start is called before the first frame update

    void Start()
    {
        originalSpeed = speed;
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if (Move >= 0.1f || Move <= -0.1f )
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if(!isFacingRight && Move > 0f)
        {
            Flip();
        
        }
        else if (!isFacingRight && Move < 0f)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            StartCoroutine(dash());
        }
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", true);
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    IEnumerable dash()
    {
        speed = 100;
        yield return new WaitForSeconds(0.1f);
        speed = originalSpeed;
        yield return new WaitForSeconds(1);
        isDashing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            cm.coinCount++;
        }
    }
}
