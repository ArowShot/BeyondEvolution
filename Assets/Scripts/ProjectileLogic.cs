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

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;
        _rb.AddForce(mouseDir * Speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
