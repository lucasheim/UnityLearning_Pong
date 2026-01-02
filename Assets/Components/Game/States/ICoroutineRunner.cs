using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine StartManagedCoroutine(IEnumerator routine);

    void StopManagedCoroutine(Coroutine routine);
}
