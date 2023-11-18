using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;
    private GameObject _player;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(lookDirection * speed);
    }
}