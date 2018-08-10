using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3;
    public float stamina = 10;
    public float health = 50;
    public float defense = 0;
    public float xAxis;
    public float yAxis;
    private Rigidbody2D rb;

    // Use this for initialization

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called independent of framerate - physics code goes here
    void FixedUpdate()
    {
        playerMovement();
        checkDead();

        if(Input.GetKeyDown("p"))
        {
            FindObjectOfType<TraitManager>().Instance.ApplyTraits(this);
        }
    }

    void playerMovement()
    {
        xAxis = Input.GetAxisRaw("Horizontal") * speed;
        yAxis = Input.GetAxisRaw("Vertical") * speed;

        Vector2 velocityX = rb.velocity;
        velocityX.x = xAxis;
        rb.velocity = velocityX;

        Vector2 velocityY = rb.velocity;
        velocityY.y = yAxis;
        rb.velocity = velocityY;

        if (rb.velocity.magnitude > speed)
        {
            rb.velocity *= speed / rb.velocity.magnitude;
        }
    }

    void checkDead()
    {
        if (this.health <= 0)
        {
            Debug.Log("I'm Dead. Rip.");
        }
    }

    void calcDamage(int damage)
    {
        this.health -= (damage - defense);
    }
}
