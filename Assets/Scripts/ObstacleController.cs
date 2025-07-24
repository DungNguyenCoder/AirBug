using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float _rotationSpeed = 180f;

    private void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }
    private void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            float _waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(_waitTime);

            _rotationSpeed *= -1;
        }
    }
}
