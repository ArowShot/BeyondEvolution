using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour {
    public float Speed = 15;
    private Rigidbody2D _rb;
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.up * Speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
