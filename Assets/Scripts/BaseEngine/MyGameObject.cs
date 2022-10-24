using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

public class MyGameObject : MonoBehaviour, iObjectID
{
    #region Base
    /*
     * описание объекта
     */
    string ObjectID = "nil";

    public string ID
    {
        get { return ObjectID; }
        set { this.ObjectID = value; }
    }

    public void SafeCall(string methodName, int select = -1)
    {
        if (string.IsNullOrEmpty(methodName)) return;
        MethodInfo mi = this.GetType().GetMethod(methodName);
        if (mi != null)
        {
            if (select == -1)
                mi.Invoke(this, null);
            else
                mi.Invoke(this, new object[] { select });
        }
        else
            Debug.LogError(ID + ": Can't find method " + methodName);
    }

    public void SafeCall(string methodName, Action del = null)
    {
        if (string.IsNullOrEmpty(methodName)) return;
        MethodInfo mi = this.GetType().GetMethod(methodName);
        if (mi != null)
        {
            if (del == null)
                mi.Invoke(this, null);
            else
                mi.Invoke(this, new object[] { del });
        }
        else
            Debug.LogError(ID + ": Can't find method " + methodName);
    }

    protected Transform _trans;

    ////////////////////////////////
    // Activity part

    /**
     * Initialize LDObject
     */
    protected bool InitBased = false;
    public bool Initialized
    {
        get { return InitBased; }
    }

    public virtual void HardSet()
    {
        if (InitBased)
            return;
        InitBased = true;
    }
    #endregion

    #region Positioning

    protected Vector3 Target;
    protected float targetVelocity;
    protected Action _del;

    public Vector3 target
    {
        get { return Target; }
        set { Target = value; }
    }

    public virtual Transform GetTransform
    {
        get
        {
            if (_trans == null)
                _trans = gameObject.transform;

            return _trans;
        }
    }

    public virtual Vector3 position
    {
        get
        {
            if (_trans == null)
                _trans = this.transform;

            return new Vector3(_trans.position.x, _trans.position.y, _trans.position.z);
        }
        set
        {
            if (_trans == null)
                _trans = this.transform;

            _trans.position = new Vector3(value.x, value.y, _trans.position.z);
        }
    }

    public virtual Vector3 ScreePoint
    {
        get
        {
            if (_trans == null)
                _trans = this.gameObject.transform;

            return _trans.position;
        }
    }

    public virtual void PlaceObject(Vector3 point, bool fast = true, Action deli = null, float time = 2f)
    {
        if (deli != null)
        {
            deli();
            deli = null;
        }
    }

    public virtual void PlaceObject(MyGameObject point, bool fast = true, Action deli = null, float time = 2f)
    {
        if (deli != null)
        {
            deli();
            deli = null;
        }
    }

    public virtual IEnumerator moveToCoroutine()
    {
        yield return null;
    }

    public virtual IEnumerator moveToPosition()
    {
        yield return null;
    }

    #endregion
}
