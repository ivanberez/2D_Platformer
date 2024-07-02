using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputHandler : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode JumpKey = KeyCode.Space;

    private Rigidbody2D _rigidbody2D;    

    public event Action MouseDownEvent;
    public event Action JumpKeyDownEvent;

    public float HorizontalAxis { get; private set; }
    public float VerticalAxis => _rigidbody2D.velocity.y;    

    private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
        HorizontalAxis = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
            JumpKeyDownEvent?.Invoke();

        if (Input.GetMouseButtonDown(0))
            MouseDownEvent?.Invoke();
    }
}
