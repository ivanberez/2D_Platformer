using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public event Action SpaceDown;

    public float HorizontalInput { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxis(Horizontal);        

        if (Input.GetKeyDown(KeyCode.Space))
            SpaceDown?.Invoke();
    }
}
