using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected int modelIndex = 0;
    [SerializeField] protected float jumpforce = 0f;
    [SerializeField] protected float dashspeed = 0f;
    [SerializeField] protected int heart = 0;
    [SerializeField] protected float dashCount = 0f;
    [SerializeField] protected float maxdashCount;
    [SerializeField] protected bool isGround = false;
    [SerializeField] protected bool isDash = false;
    [SerializeField] protected bool isSDash = false;
    [SerializeField] protected bool dashReset = false;
    [SerializeField] protected bool isInvincible = false;
    [SerializeField] protected GameObject drawpool;
    [SerializeField] protected Vector3 firstpos;
    [SerializeField] protected List<Vector3> dashRoot = new List<Vector3>();
    [SerializeField] protected ParticleSystem ghostEffect;
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected GameObject[] heartImage = new GameObject[3];
    [SerializeField] protected Slider dashBar;
    [SerializeField] protected Slider mdashBar;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected WaitForSeconds sdashdelay = new WaitForSeconds(3f);
    protected WaitForSeconds indivildelay = new WaitForSeconds(0.1f);
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
   
    private Color alpha0 = new Color(0, 0, 0, -1);
    private Color alpha100 = new Color(0, 0, 0, 1);

    private void Awake()
    {
        if (modelIndex == 2)
            _sprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        else
            _sprite = GetComponent<SpriteRenderer>();
    }

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

        dashCount = maxdashCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isGround)
        {
            if(isDash || dashReset)
                return;

            anim.SetTrigger(jump);

            if (dashCount < maxdashCount)
                dashCount++;
        }
        else if (Input.GetKeyDown(KeyCode.S) && dashCount == maxdashCount)
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

        DashBarUpdate();
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
        isSDash = true;

        while (isDash)
        {
            if (transform.position.x < dashRoot.Last().x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot.Last(), dashspeed * Time.deltaTime);

            if (transform.position == dashRoot.Last())
            {
                yield return sdashdelay;

                gm.worldSpeed -= 3f;
                isSDash = false;
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
    public void StateReset()
    {
        StopAllCoroutines();
        isDash = false;
        isSDash = false;
        dashReset = false;
        isInvincible = false;
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1f); 
        //_sprite.enabled = true;
    }

    public void SwitchInvincible()
    {
        StartCoroutine(Hit(2));
    }

    public IEnumerator Hit(int blinkCount)
    {
        if (isInvincible)
        {
            yield break;
        }

        Debug.Log("Hit");
        isInvincible = true;

        for (int i = 0; i < blinkCount; i++)
        {
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0f); 
            yield return indivildelay;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1f); 
            yield return indivildelay;
        }

        isInvincible = false;
    }

    private void Die()
    {
        for (int i = heartImage.Length - 1; i >= 0; i--)
        {
            if (heartImage[i].activeSelf)
            {
                heartImage[i].SetActive(false);
                break;
            }
        }

        if (gm.hp > 0)
        {
            gm.hp--;
            StartCoroutine(Hit(4));
        }
        else
            gm.GameOver();
    }

    public void DashBarUpdate()
    {
        dashBar.value = dashCount / maxdashCount;
        mdashBar.value = dashCount / maxdashCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(sdashItem))
        {
            StopAllCoroutines();

            if (dashReset || isDash)
            {
                dashReset = false;
                isDash = false;
            }
            if(isSDash)
            {
                isSDash = false;
                gm.worldSpeed -= 3f;
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
            {
                gm.hp++;

                for (int i = 0; i < heartImage.Length; i++)
                {
                    if (!heartImage[i].activeSelf)
                    {
                        heartImage[i].SetActive(true);
                        break;
                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag(obs))
        {
            if (dashReset || isInvincible)
                return;
            else if (!isDash)
                Die();
            else
            {
                collision.gameObject.SetActive(false);
                gm.gameScore += 20;
            }
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

    private void OnEnable()
    {
        StateReset();
        //StartCoroutine(Hit(2));
    }

    private void OnDisable()
    {
        StateReset();
    }

    protected abstract IEnumerator Dash();
    protected abstract IEnumerator DashReset();

}
