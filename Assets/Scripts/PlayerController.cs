using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3;
    public float Stamina = 10;
    public float MaxStamina = 10;
    public float Health = 100;
    public float MaxHealth = 100;
    public float Defense = 1;
    public float Attack = 0;
    public float XAxis;
    public float YAxis;
    public GameObject Laser;
    private Rigidbody2D _rb;
    public Animator Animator;

    // Use this for initialization

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
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

        if (Input.GetMouseButtonDown(0))
        {
            tm.ActivateAbility(this, 1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            tm.ActivateAbility(this, 2);
        }
        if (Input.GetKeyDown("q"))
        {
            tm.ActivateAbility(this, 3);
        }
        if (Input.GetKeyDown("e"))
        {
            tm.ActivateAbility(this, 4);
        }
    }

    private void PlayerMovement()
    {
        XAxis = Input.GetAxisRaw("Horizontal") * Speed;
        YAxis = Input.GetAxisRaw("Vertical") * Speed;

        var velocityX = _rb.velocity;
        velocityX.x = XAxis;
        _rb.velocity = velocityX;

        var velocityY = _rb.velocity;
        velocityY.y = YAxis;
        _rb.velocity = velocityY;

        if (_rb.velocity.magnitude > Speed)
        {
            _rb.velocity *= Speed / _rb.velocity.magnitude;
        }
        
        
        if (Animator != null)
        {
            if(Animator.runtimeAnimatorController!=null)
            {
                Animator.SetFloat("Up", Input.GetAxisRaw("Vertical"));
                Animator.SetFloat("Right", Input.GetAxisRaw("Horizontal"));
                /*if (Input.GetAxis("Vertical") > 0)
                {
                    Animator.SetFloat("Up", 1);
                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    Animator.SetFloat("Up", -1);
                }
                if (Input.GetAxis("Vertical") == 0)
                {
                    Animator.SetFloat("Up", 0);
                }
                if (Input.GetAxis("Horizontal") > 0)
                {
                    Animator.SetFloat("Right", 1);
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    Animator.SetFloat("Right", -1);
                }
                if (Input.GetAxis("Horizontal") == 0)
                {
                    Animator.SetFloat("Right", 0);
                }*/
                
                Animator.SetBool("Walking", Math.Abs(velocityX.x) + Math.Abs(velocityY.y) > .2);
            }
        }

    }

    private void CheckDead()
    {
        if (Health <= 0)
        {
            Debug.Log("I'm Dead. Rip.");
        }
    }

    private void PlayerAttack()
    {
        Instantiate(Laser, transform.position, Quaternion.identity);
    }
}
