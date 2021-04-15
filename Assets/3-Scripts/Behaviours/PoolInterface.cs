using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PoolEvent(IPoolable sender);

public interface IPoolable
{
    event PoolEvent OnEntityAwoken;
    event PoolEvent OnEntitySlept;

    GameObject gameObject { get; }

    void Awaken();
    void Sleep();
}
