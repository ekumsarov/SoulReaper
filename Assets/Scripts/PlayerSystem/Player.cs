using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Player : SceneObject
{
    // TODO: Remove this after
    void Start()
    {
        HardSet();
    }


    public override void HardSet()
    {
        base.HardSet();

        if (Animation == null)
        {
            UnityArmatureComponent component = gameObject.GetComponent<UnityArmatureComponent>();
            if (component == null)
            {
                throw new System.Exception();
            }

            this.Animation = component.animation;
            this._armature = component.armature;
        }

        this.GetComponent<Rigidbody2D>().gravityScale = this._playerData.Gravity;

        this.Animation.Stop();

        if (this._controller == null)
            this._controller = gameObject.GetComponent<PlayerController>();

        if(this._controller == null)
            this.gameObject.AddComponent<PlayerController>();

        this._controller.Initialize(this);
    }

    #region Base Properties

    [SerializeField] private PlayerController _controller;
    [SerializeField] private PlayerData _playerData;
    public PlayerData PlayerData => _playerData;
    public DragonBones.Animation Animation { get; private set; }
    private Armature _armature;
    public bool Flip
    {
        set
        {
            this._armature.flipX = value;
        }
    }

    #endregion
}
