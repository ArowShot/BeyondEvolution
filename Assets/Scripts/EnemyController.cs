using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D _rb;
    private TraitManager _tm;
    public float MinDistance = 6f;
    public float Range;
    public Transform Target;
    public float Attack = 8;
    public float Speed = 2;
    public float Force = 250;
    public float Health = 50;
    public float Defense = 0;
    public float Thrust = 10.0f;
    public bool isPoisoned = false;
	// Use this for initialization
	void Start ()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tm = TraitManager.Instance;
        InvokeRepeating("CheckPoison", 0.0f, 1.5f);
        Target = FindObjectOfType<PlayerController>().transform;
    }
	
	// Update is called independent of framerate - puts physics logic in here
	void FixedUpdate ()
    {
        EnemyPathfinding();
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            _tm.EvolutionPoints++;
        }
	}

    void EnemyPathfinding()
    {
        // Enemy follows the player if within the required distance
        Range = Vector2.Distance(transform.position, Target.position);

        if (Range < MinDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            // better way below
            _rb.AddForce((Target.transform.position - transform.position).normalized * Speed);
        }
    }

    void OnCollisionEnter2D(Collision2D col2d)
    {
        // If player collides with an enemy, take damage equal to this enemies Attack power
        var player = col2d.gameObject.GetComponent<PlayerController>();
        if (player == null)
            return;
        if (_tm.UnlockedTraits.Exists(x => x.FullName == "ThornsTrait"))
        {
            player.Health -= (Attack - player.Defense);
            Health -= (Attack - Defense) * 0.5f;
        } else
        {
            player.Health -= (Attack - player.Defense);
        }
        player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * Force);
    }

    private void OnTriggerEnter2D(Collider2D col2d)
    {
        // If player collides with an enemy, take damage equal to this enemies Attack power
        var player = col2d.gameObject.GetComponent<PlayerController>();
        if (player == null)
            return;
        
        player.Health -= (Attack - player.Defense);
        player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * Force);
        /*if(_tm.UnlockedTraits.Exists(x => x.FullName == "PoisonTrait"))
        {
            isPoisoned = true;
            Health -= 5;
        }
        else
        {
            Health -= 5;
        }*/
    }

    private void CheckPoison()
    {
        if (isPoisoned == true)
        {
            GetComponent<ParticleSystem>().enableEmission = true;
            Health -= 2;
        }
        else
        {
            GetComponent<ParticleSystem>().enableEmission = false;
        }
    }
}
