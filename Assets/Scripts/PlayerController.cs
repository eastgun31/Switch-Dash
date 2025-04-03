using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected float jumpforce = 0f;
    [SerializeField] protected float dashspeed = 0f;
    [SerializeField] protected int heart = 0;
    [SerializeField] protected float dashCount = 0f;
    [SerializeField] protected bool isGround = false;
    [SerializeField] protected bool isDash = false;
    [SerializeField] protected bool dashReset = false;
    [SerializeField] protected GameObject drawpool;
    [SerializeField] protected Vector3 firstpos;
    [SerializeField] protected List<Vector3> dashRoot = new List<Vector3>();
    [SerializeField] protected ParticleSystem ghostEffect;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected WaitForSeconds sdashdelay = new WaitForSeconds(3f);
    protected string ground = "Ground";
    protected string run = "Run";
    protected string jump = "Jump";
    protected string dash = "Dash";
    protected string sdash = "SDash";
    protected string sdashItem = "SDashItem";
    protected string hItem = "HItem";
    protected string obs = "Obstacle";
    protected DrawPooling drawPooling;
    protected GameManager gm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        drawPooling = drawpool.GetComponent<DrawPooling>();
        gm = GameManager.instance;

        Color colorWithAlpha = gm.colors[gm.modelIndex];
        colorWithAlpha.a = 0.3f; 
        ParticleSystem.MainModule main = ghostEffect.main;
        main.startColor = new ParticleSystem.MinMaxGradient(colorWithAlpha);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && isGround)
        {
            if(isDash || dashReset)
                return;

            anim.SetTrigger(jump);

            if (dashCount > 0)
                dashCount--;
        }
        else if (Input.GetKeyDown(KeyCode.S) && dashCount == 0)
        {
            if(isDash || dashReset)
                return;

            isDash = true;
            gm.nowDash = true;
            anim.SetBool(run, false);
            anim.SetTrigger(dash);
            GhostEffectOn();

            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;

            StartCoroutine(Dash());
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        isGround = false;
        GhostEffectOn();

        drawPooling.MoveDraw();
    }

    private IEnumerator SuperDash()
    {
        gm.worldSpeed += 3f;
        gm.nowDash = true;

        while (isDash)
        {
            if (transform.position.x < dashRoot.Last().x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot.Last(), dashspeed * Time.deltaTime);

            if (transform.position == dashRoot.Last())
            {
                yield return sdashdelay;

                gm.worldSpeed -= 3f;
                isDash = false;
                StartCoroutine(DashReset());
            }

            yield return null;
        }
    }

    public void GhostEffectOn()
    {
        ghostEffect.Play();
    }
    public void GhostEffectOff()
    {
        ghostEffect.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(sdashItem))
        {
            if(dashReset)
            {
                dashReset = false;
                StopAllCoroutines();
            }

            isDash = true;
            anim.SetBool(run, false);
            anim.SetTrigger(sdash);
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            GhostEffectOff();

            Destroy(collision.gameObject);
            StartCoroutine(SuperDash());
        }
        else if (collision.gameObject.CompareTag(hItem))
        {
            Destroy(collision.gameObject);

            if (gm.hp < 3)
                gm.hp++;
        }
        else if (collision.gameObject.CompareTag(obs))
        {
            if (!isDash && !dashReset)
                gm.hp--;
            else
                collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ground) && !isDash && !dashReset)
        {
            isGround = true;
            anim.SetBool(run, true);
            GhostEffectOff();
            drawPooling.SetDraw();
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
    protected abstract IEnumerator DashReset();
    protected abstract void Die();

}
