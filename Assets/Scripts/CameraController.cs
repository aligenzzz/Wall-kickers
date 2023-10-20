using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 position;
    private float maxY = 0f;

    private void Awake() { if (!player) player = FindObjectOfType<Hero>().transform; }

    private void Update()
    {
        position = player.position;
        position.z = -10f;

        position.y = Math.Max(maxY, position.y + 7.82f);
        maxY = position.y;

        position.x += 2.37f;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    }
}
