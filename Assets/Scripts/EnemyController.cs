using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D _rb;
    public float MinDistance = 4f;
    public float Range;
    public Transform Target;
    public float Speed = 1;
    public float Health = 50;
    public float Defense = 0;
	// Use this for initialization
	void Start ()
    {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called independent of framerate - puts physics logic in here
	void FixedUpdate ()
    {
        EnemyPathfinding();
	}

    void EnemyPathfinding()
    {
        Range = Vector2.Distance(transform.position, Target.position);

        if (Range < MinDistance)
        {
            Debug.Log(Range);
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
    }
}
