using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask timerWallLayer;

    private Rigidbody2D body;
    public static Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioSource sound;
    private BoxCollider2D box_collider;

    private bool doubleJump = true;
    private bool wasInAir = false;
    public bool hitLeaper = false;

    private int maxScoreValue = 0;
    private float timeLeft = 3;

    private IEnumerator coroutine;

    public states state
    {
        get { return (states)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (onTimerWall())
            {
                if (sprite.flipX)
                {
                    transform.position = new Vector2(transform.position.x - 0.8f, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + 0.8f, transform.position.y);
                }

                sprite.flipX = !sprite.flipX;
            }
            timeLeft = 3;
        }

        if (hitLeaper)
        {
            doubleJump = true;
            Jump();
            hitLeaper = false;
        }

        checkScore();

        if (!checkHero())
        {
            state = states.death;
            Debug.Log("Death!!!!!!!!!!!!!!");

            coroutine = ExecuteAfterTime(1.5f);
            StartCoroutine(coroutine);
        }

        if (state == states.death || state == states.sit) return; // ??

        if (onGround()) state = states.idle;
        else if (onWall() || onTimerWall()) state = states.on_wall;
        else state = states.jump;

        if (!onWall() && !onTimerWall()) Physics2D.gravity = new Vector2(0f, -9.81f); 

        if (!onGround() && !onWall() && !onTimerWall()) wasInAir = true;

        if (onGround() || onWall() || onTimerWall()) doubleJump = true; 

        if (onWall() && wasInAir)
        {                               
            body.velocity = Vector2.zero;
            wasInAir = false;
            Physics2D.gravity = new Vector2(0f, -0.1f);
        }

        if (onTimerWall() && wasInAir)
        {
            body.velocity = Vector2.zero;
            wasInAir = false;
            Physics2D.gravity = Vector2.zero;
        }

        if (onGround() && Input.GetButton("Horizontal")) Run();
        if (Input.GetKeyDown(KeyCode.Space) && (doubleJump))
        {
            if (!onGround() && !onWall() && !onTimerWall()) doubleJump = false; 
            sound.Play();
            Jump();
        }
    }

    private bool checkHero()
    {
        float difference = Camera.main.transform.position.y - (body.position.y + 7.81f);
        return ((int)difference < 6);
    }

    private void checkScore()
    {
        int cScore = (int)body.position.y + 10;
        cScore /= 2;
        maxScoreValue = Math.Max(maxScoreValue, cScore);
        ScoreSystem.scoreValue = maxScoreValue;
    }

    private void Run()
    {
        state = states.run;

        Vector3 distance = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + distance, speed * Time.deltaTime);

        sprite.flipX = distance.x < 0.0f;
    }

    public void Jump() 
    {
        if (onGround())
        {
            if (sprite.flipX) body.velocity = new Vector2(-0.5f * jumpForce, jumpForce);
            else body.velocity = new Vector2(0.5f * jumpForce, jumpForce);
        }
        else if (onWall())
        {
            if (sprite.flipX) body.velocity = new Vector2(0.5f * jumpForce, jumpForce);
            else body.velocity = new Vector2(-0.5f * jumpForce, jumpForce);
            sprite.flipX = !sprite.flipX;
        }
        else
        {
            if (!sprite.flipX) body.velocity = new Vector2(-0.5f * jumpForce, jumpForce);
            else body.velocity = new Vector2(0.5f * jumpForce, jumpForce);
            sprite.flipX = !sprite.flipX;
        }
    }

    private bool onGround()
    {
        RaycastHit2D raycast_hit = Physics2D.BoxCast(box_collider.bounds.center, box_collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycast_hit2 = Physics2D.BoxCast(box_collider.bounds.center, box_collider.bounds.size, 0, Vector2.down, 0.1f, wallLayer);

        return (raycast_hit.collider != null || raycast_hit2.collider != null);
    }

    private bool onWall()
    {
        RaycastHit2D raycast_hit1 = Physics2D.BoxCast(box_collider.bounds.center, new Vector2(0.8425932f, 0.1f), 0, Vector2.right, 0.1f, wallLayer);
        RaycastHit2D raycast_hit2 = Physics2D.BoxCast(box_collider.bounds.center, new Vector2(0.8425932f, 0.1f), 0, Vector2.left, 0.1f, wallLayer);

        return (raycast_hit1.collider != null) || (raycast_hit2.collider != null);
    }
    private bool onTimerWall()
    {
        RaycastHit2D raycast_hit1 = Physics2D.BoxCast(box_collider.bounds.center, new Vector2(0.8425932f, 0.1f), 0, Vector2.right, 0.1f, timerWallLayer);
        RaycastHit2D raycast_hit2 = Physics2D.BoxCast(box_collider.bounds.center, new Vector2(0.8425932f, 0.1f), 0, Vector2.left, 0.1f, timerWallLayer);

        return (raycast_hit1.collider != null) || (raycast_hit2.collider != null);
    }


    IEnumerator ExecuteAfterTime(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);

        GameObject obj = GameObject.FindGameObjectWithTag("music");

        if (obj)
        {
            Destroy(obj);
        }

        SceneManager.LoadScene(0);
    }
}

public enum states { idle, run, jump, on_wall, death, sit }