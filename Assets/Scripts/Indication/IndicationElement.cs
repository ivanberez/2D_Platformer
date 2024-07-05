using System.Collections;
using UnityEngine;

public abstract class IndicationElement : MonoBehaviour
{
    private Coroutine _coroutine;

    public void Refresh(IDataIndication dataIndication, bool isSmoother)
    {
        if (isSmoother)
            ShowSmooth(dataIndication);
        else
            ShowInstant(dataIndication);
    }

    protected abstract void ShowInstant(IDataIndication dataIndication);
    protected abstract IEnumerator SmoothRoutine(IDataIndication dataIndication);
    
    private void ShowSmooth(IDataIndication dataIndication)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothRoutine(dataIndication));        
    }
}