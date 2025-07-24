using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private float _coinSize = 1f;
    private int _pointValue = 1;
    private float minX = -20f, maxX = 20f;
    private float minY = -10f, maxY = 10f;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.point);
            GameManager.Instance.AddScore(_pointValue);
            transform.position = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }
}
