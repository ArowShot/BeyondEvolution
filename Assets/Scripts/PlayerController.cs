using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 80;
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
    private GameManager _gm;

    // Use this for initialization

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
        Animator = GetComponent<Animator>();
        InvokeRepeating("RegenStamina", 5.0f, 4.0f);
    }

    // Update is called independent of framerate - physics code goes here
    private void FixedUpdate()
    {
        PlayerMovement();
        CheckDead();

        var tm = TraitManager.Instance;
        
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
    
        _rb.AddForce(new Vector2(XAxis, YAxis));
        
        /*var velocityX = _rb.velocity;
        velocityX.x = XAxis;
        _rb.velocity = velocityX;

        var velocityY = _rb.velocity;
        velocityY.y = YAxis;
        _rb.velocity = velocityY;

        if (_rb.velocity.magnitude > Speed)
        {
            _rb.velocity *= Speed / _rb.velocity.magnitude;
        }*/
        
        
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
                
                Animator.SetBool("Walking", Math.Abs(XAxis) + Math.Abs(YAxis) > .2);
            }
        }

    }

    private void CheckDead()
    {
        if (Health <= 0)
        {
            _gm.OnDead();
        }
    }

    private void PlayerAttack()
    {
        Instantiate(Laser, transform.position, Quaternion.identity);
    }

    private void RegenStamina()
    {
        if (Stamina < MaxStamina)
        {
            Stamina++;
        }
    }
}
