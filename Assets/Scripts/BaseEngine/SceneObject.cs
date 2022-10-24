using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MyGameObject
{
    #region Base
    /*
     * описание объекта
     */

    protected bool _visible = true;
    protected bool _lock = false;

    /*
     *
     * определение всех данных типов как опции для доступа и чтение
     *
     */

    public override void HardSet()
    {
        if (InitBased)
            return;
        InitBased = true;

        this._trans = this.transform;

        this._particles = new Dictionary<string, ParticleObject>();
        foreach(var particle in GetComponentsInChildren<ParticleObject>())
        {
            particle.Setup();
            this._particles.Add(particle.name, particle);
        }
    }


    public virtual bool Visible
    {
        get
        {
            return _visible;
        }
        set
        {
            if (value && this._lock == false && this.gameObject.activeSelf == false)
            {
                this._visible = value;
                this.gameObject.SetActive(true);
                return;
            }

            if (_visible == value)
                return;

            if (value && this._lock)
                return;

            _visible = value;
            this.gameObject.SetActive(value);
        }
    }

    public virtual bool Lock
    {
        get { return this._lock; }
        set
        {
            if (this._lock == value)
                return;

            if (value == false)
                this.gameObject.SetActive(value);

            this._lock = value;
        }
    }


    // функции для записи

    /**
     * запись Позиции
     */
    public override Vector3 position
    {
        get
        {
            if (_trans == null)
                _trans = this.transform;

            return _trans.position;
        }
        set
        {
            if (_trans == null)
                _trans = this.transform;

            _trans.position = new Vector3(value.x, value.y, _trans.position.z);
        }
    }

    public Quaternion Rotation
    {
        get
        {
            if (_trans == null)
                _trans = this.transform;

            return _trans.rotation;
        }
        set
        {
            if (_trans == null)
                _trans = this.transform;

            _trans.rotation = value;
        }
    }

    public Vector2 VectorPosition
    {
        get
        {
            return new Vector2(this.position.x, this.position.z);
        }
    }


    ////////////////////////////////
    // Activity part

    #endregion

    #region Partciles

    private Dictionary<string, ParticleObject> _particles;
    public void PlayParticle(string id)
    {
        if (_particles == null)
            _particles = new Dictionary<string, ParticleObject>();

        if (_particles.ContainsKey(id))
            _particles[id].Play();
        else
            Debug.LogError("No such particle " + id);
    }

    public void StopParticle(string id)
    {
        if (_particles == null)
            _particles = new Dictionary<string, ParticleObject>();

        if (_particles.ContainsKey(id))
            _particles[id].Stop();
        else
            Debug.LogError("No such particle " + id);
    }

    #endregion
}
