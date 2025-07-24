using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _bulletSpeed = 15f;
    private int _bulletBounciness = 0;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private AudioManager audioManager;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        _rb.velocity = _direction * _bulletSpeed;

    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }
    public void IgnoreCollisionOnce(Collider2D other)
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        if (myCollider != null && other != null)
        {
            Physics2D.IgnoreCollision(myCollider, other, true);
            StartCoroutine(EnableCollitionAfferDelay(myCollider, other, 0.5f));
        }
    }
    private IEnumerator EnableCollitionAfferDelay(Collider2D myCollider, Collider2D other, float delay)
    {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(myCollider, other, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlaySFX(audioManager.bounce);
        ++_bulletBounciness;
        if (_bulletBounciness == 10)
        {
            Destroy(gameObject);
        }
    }
}
