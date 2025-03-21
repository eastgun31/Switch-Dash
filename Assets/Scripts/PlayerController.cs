using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected float jumpforce = 0f;
    [SerializeField] protected float dashspeed = 0f;
    [SerializeField] protected int heart = 0;
    [SerializeField] protected float dashCount = 0f;
    [SerializeField] protected bool isGround = false;
    [SerializeField] protected bool isDash = false;
    [SerializeField] protected GameObject drawpool;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected string ground = "Ground";
    protected string run = "Run";
    protected string jump = "Jump";
    protected DrawPooling drawPooling;
    GameManager gm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        drawPooling = drawpool.GetComponent<DrawPooling>();
        gm = GameManager.instance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && isGround)
        {
            anim.SetTrigger(jump);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            isDash = true;
            anim.SetBool(run, false);
            transform.position = new Vector3(-3, 0.003f, 0);
            rb.gravityScale = 0;

            StartCoroutine(Dash());
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        isGround = false;

        drawPooling.MoveDraw();
    }

    private void Draw()
    {
        drawPooling.SetDraw();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ground) && !isDash)
        {
            isGround = true;
            anim.SetBool(run, true);
            Draw();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ground))
        {
            isGround = false;
            anim.SetBool(run, false);

            if (drawPooling != null)
                drawPooling.MoveDraw();
            else
                Debug.LogWarning("DrawPooling component is missing.");
        }
            
    }

    protected abstract IEnumerator Dash();
    protected abstract void Die();

}
