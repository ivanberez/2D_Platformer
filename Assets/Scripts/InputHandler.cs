using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private bool _isSpaceDown;

    public event Action MouseDownEvent;
    public float HorizontalAxis { get; private set; }

    public bool IsSpaceDown
    {
        get
        {
            bool result = _isSpaceDown;
            _isSpaceDown = false;
            return result;
        }
    }    

    private void Update()
    {
        HorizontalAxis = Input.GetAxis(Horizontal);        
       
        if(Input.GetKeyDown(KeyCode.Space)) 
            _isSpaceDown = true;

        if (Input.GetMouseButtonDown(0))
            MouseDownEvent?.Invoke();
    }
}
