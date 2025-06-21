using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;     // Degrees per second
    [SerializeField] private float bobSpeed = 1f;           // Speed of the sine wave
    [SerializeField] private float bobHeight = 0.2f;        // Amplitude of the bob
    private float initialY;               // Starting Y position

    void Start()
    {
        // Save the initial Y position to offset the sine movement
        initialY = transform.position.y;
    }

    void Update()
    {
        // Rotate around the Y axis
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);

        // Calculate the new Y position using sine wave
        float newY = initialY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Apply the new Y position while keeping X and Z the same
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;
    }
}
