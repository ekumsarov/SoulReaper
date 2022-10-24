using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;

    [SerializeField] private float _gravity;
    public float Gravity => _gravity;

    [SerializeField] private float _jumpVelocity;
    public float JumpVelocity => _jumpVelocity;

    [SerializeField] private float _variableJumpHeightMultiplier;
    public float VariableJumpHeightMultiplier => _variableJumpHeightMultiplier;

    [SerializeField] private float _coyoteTime;
    public float CoyoteTime => _coyoteTime;

    [SerializeField] private int _amountOfJumps;
    public int AmountOfJumps => _amountOfJumps;

    [SerializeField] private bool _activateVariableJump;
    public bool ActivateVariableJump => _activateVariableJump;

    [SerializeField] private int _attackCombo;
    public int AttackCombo => _attackCombo;

    #region animation name
    private const string _idle = "idle";
    public string Idle => _idle;

    private const string _walk = "walk";
    public string Walk => _walk;

    private const string _jump = "JumpPush";
    public string Jump => _jump;

    private const string _flyingUp = "JumpFlyingUp";
    public string FlyingUp => _flyingUp;

    private const string _flyingDown = "JumpFlyingDown";
    public string FlyingDown => _flyingDown;

    private const string _landing = "JumpFlyingLanding";
    public string Landing => _landing;

    private const string _weakAttack = "fight";
    public string WeakAttack => _weakAttack;
    #endregion
}
