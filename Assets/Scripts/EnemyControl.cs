using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float m_speed = 8;
    public bool m_grounded = false;
    Rigidbody2D rb;
    Animator m_animator;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (m_grounded)
        {
            rb.velocity = transform.right * -m_speed;
            m_animator.SetInteger("AnimState", 2);
        }
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_grounded = other.tag == "Ground" ? true : false;
    }

}
