using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3;
    public float Stamina = 10;
    public float MaxStamina = 10;
    public float Health = 50;
    public float MaxHealth = 100;
    public float Defense = 0;
    public float Attack = 0;
    public float XAxis;
    public float YAxis;
    private Rigidbody2D Rb;

    // Use this for initialization

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called independent of framerate - physics code goes here
    private void FixedUpdate()
    {
        PlayerMovement();
        CheckDead();

        var tm = FindObjectOfType<TraitManager>().Instance;
        
        if(Input.GetKeyDown("p"))
        {
            tm.ApplyTraits(this);
        }

        if (Input.GetKeyDown("1"))
        {
            tm.ActivateAbility(this, 1);
        }
        if (Input.GetKeyDown("2"))
        {
            tm.ActivateAbility(this, 2);
        }
        if (Input.GetKeyDown("3"))
        {
            tm.ActivateAbility(this, 3);
        }
        if (Input.GetKeyDown("4"))
        {
            tm.ActivateAbility(this, 4);
        }
    }

    private void PlayerMovement()
    {
        XAxis = Input.GetAxisRaw("Horizontal") * Speed;
        YAxis = Input.GetAxisRaw("Vertical") * Speed;

        var velocityX = Rb.velocity;
        velocityX.x = XAxis;
        Rb.velocity = velocityX;

        var velocityY = Rb.velocity;
        velocityY.y = YAxis;
        Rb.velocity = velocityY;

        if (Rb.velocity.magnitude > Speed)
        {
            Rb.velocity *= Speed / Rb.velocity.magnitude;
        }
    }

    private void CheckDead()
    {
        if (Health <= 0)
        {
            Debug.Log("I'm Dead. Rip.");
        }
    }

    private void CalcDamage(int damage)
    {
        Health -= (damage - Defense);
    }
}
