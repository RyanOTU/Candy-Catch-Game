using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    private Vector2 _moveDir;
    void Start()
    {
        Controls.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (_moveDir.x * Time.deltaTime * _moveSpeed), Space.Self);
    }
    public void SetMovementDirection(Vector3 direction)
    {
        _moveDir = direction;
    }
}
