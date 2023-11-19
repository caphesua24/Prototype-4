using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private const float RotationSpeed = 100;

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * (horizontalInput * RotationSpeed * Time.deltaTime));
    }
}