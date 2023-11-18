using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 50;

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * (horizontalInput * rotationSpeed * Time.deltaTime));
    }
}