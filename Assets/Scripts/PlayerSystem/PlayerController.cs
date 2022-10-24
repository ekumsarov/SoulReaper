using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _parent;
    private StateMachine _stateMachine;
    private PlayerInputHandler _inputHandler;
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSense { get; private set; }

    public void Initialize(Player parent)
    {
        this._parent = parent;
        this._transform = this._parent.transform;
        this._stateMachine = new StateMachine();
        this.Movement = GetComponentInChildren<Movement>();
        this.Movement.Setup();
        this.CollisionSense = GetComponentInChildren<CollisionSenses>();
        this._inputHandler = GetComponentInChildren<PlayerInputHandler>();
        InputSetup();
        StateInitialize();

        this._stateMachine.Initialize(this.InAirState);
    }

    public void PlayAnimation(string animation, int playTime = -1)
    {
        this._parent.Animation.Play(animation, playTime);
    }

    public void StopAnimation()
    {
        this._parent.Animation.Stop();
    }

    public bool IsAnimationComplete => this._parent.Animation.isCompleted;

    private void Update()
    {
        this.Movement.LogicUpdate();
        this._stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        this._stateMachine.CurrentState.PhysicsUpdate();
    }

    #region States

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public InAirState InAirState { get; private set; }
    public JumpState JumpState { get; private set; }
    public LandState LandState { get; private set; }
    public AttackState AttackState { get; private set; }

    private void StateInitialize()
    {
        this.IdleState = new IdleState();
        this.IdleState.Initialize(this, this._stateMachine, this._parent.PlayerData);

        this.MoveState = new MoveState();
        this.MoveState.Initialize(this, this._stateMachine, this._parent.PlayerData);

        this.InAirState = new InAirState();
        this.InAirState.Initialize(this, this._stateMachine, this._parent.PlayerData);

        this.JumpState = new JumpState();
        this.JumpState.Initialize(this, this._stateMachine, this._parent.PlayerData);

        this.LandState = new LandState();
        this.LandState.Initialize(this, this._stateMachine, this._parent.PlayerData);

        this.AttackState = new AttackState();
        this.AttackState.Initialize(this, this._stateMachine, this._parent.PlayerData);
    }

    #endregion

    #region Movement Properties

    private Transform _transform;
    public Vector2 Position => _transform.position;
    public void InputSetup()
    {
        _inputHandler.xInputChange += SetXInput;
        _inputHandler.JumpInputChange += SetJumpInput;
        _inputHandler.JumpStopInputChange += SetJumpInputStop;
        _inputHandler.AddAttckChange += AddWeakAttack;
    }

    private int _facingDirection = 1;
    private int _xInput = 0;
    private void SetXInput(int xInput)
    {
        _xInput = xInput;
    }
    public int xInput => _xInput;
    public int FacingDirection => _facingDirection;


    private bool _jumpInput;
    private bool _jumpInputStop;

    public bool JumpInput => _jumpInput;
    public bool JumpInputStop => _jumpInputStop;
    private void SetJumpInput(bool input)
    {
        _jumpInput = input;
    }
    private void SetJumpInputStop(bool input)
    {
        _jumpInputStop = input;
    }

    public void UseJumpInput()
    {
        _jumpInput = false;
    }

    private int _attackCounter;
    public int AttackCounter => _attackCounter;

    private void AddWeakAttack()
    {
        if (_attackCounter >= this._parent.PlayerData.AttackCombo || this._stateMachine.CurrentState == this.InAirState)
            return;

        _attackCounter += 1;
    }

    public void ResetAttack()
    {
        _attackCounter = 0;
    }

    public void CheckIfShouldFlip()
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        _facingDirection *= -1;
        this._parent.Flip = _facingDirection != 1;
    }

    #endregion

    #region Particles

    public void PlayParticle(string id)
    {
        _parent.PlayParticle(id);
    }

    public void StopParticle(string id)
    {
        _parent.StopParticle(id);
    }

    #endregion
}
