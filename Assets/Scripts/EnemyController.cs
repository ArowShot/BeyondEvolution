using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D _rb;
    private TraitManager _tm;
    public float MinDistance = 6f;
    public float Range;
    public Transform Target;
    public float Attack = 2;
    public float Speed = 2;
    public float Force = 150;
    public float Health = 50;
    public float Defense = 0;
    public float Thrust = 10.0f;
	// Use this for initialization
	void Start ()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tm = FindObjectOfType<TraitManager>().Instance;
	}
	
	// Update is called independent of framerate - puts physics logic in here
	void FixedUpdate ()
    {
        EnemyPathfinding();
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
        
        player.Health -= (Attack - player.Defense);
        _rb.AddForce((player.transform.position - transform.position).normalized * -Force);
    }

    private void OnTriggerEnter2D(Collider2D col2d)
    {
        Debug.Log(_tm.UnlockedTraits.Exists(x => x.FullName == "PoisonTrait"));
    }
}
