using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private GameObject bulletPrefabs;
    private float fireTimeDelay = 1f;
    private float bulletAngleRange = 180f;

    private Rigidbody2D _rb;
    private Vector2 movement;

    private AudioManager audioManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _flySpeed = 10f;
        StartCoroutine(ShootRoutine());
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        _rb.velocity = movement * _flySpeed;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
        float _angle = Random.Range(-bulletAngleRange / 2f, bulletAngleRange / 2f);
        Vector2 _forward = transform.up;
        Vector2 _rotateDir = Quaternion.Euler(0, 0, _angle) * _forward;

        BulletController _bulletController = bullet.GetComponent<BulletController>();
        _bulletController.SetDirection(_rotateDir);

        Collider2D _playerCollider = GetComponent<Collider2D>();
        _bulletController.IgnoreCollisionOnce(_playerCollider);
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTimeDelay);
            Shoot();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("va cham");
            audioManager.PlaySFX(audioManager.die);
            FindObjectOfType<GameManager>().ShowEndGame();
        }
    }
}
