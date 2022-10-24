using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public delegate void xInputChangeDelegate(int xInput);
    public xInputChangeDelegate xInputChange;

    public delegate void BoolInputChangeDelegate(bool Input);
    public delegate void VoidInputChangeDelegate();

    public BoolInputChangeDelegate JumpInputChange;
    public BoolInputChangeDelegate JumpStopInputChange;

    public VoidInputChangeDelegate AddAttckChange;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private Vector2 _rawMoveInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        _rawMoveInput = context.ReadValue<Vector2>();

        xInputChange(Mathf.RoundToInt(_rawMoveInput.x));
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInputChange(true);
            JumpStopInputChange(false);
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputChange(false);
            JumpStopInputChange(true);
        }
    }

    public void OnWeakAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            AddAttckChange();
        }
    }
}
