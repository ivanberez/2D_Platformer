using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public event Action SpaceDown;

    public float HorizontalAxis { get; private set; }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis(Horizontal);        
     
        if (Input.GetKeyDown(KeyCode.Space))
            SpaceDown?.Invoke();
    }
}
