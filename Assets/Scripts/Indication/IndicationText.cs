using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IndicationText : IndicationElement
{        
    [SerializeField] private bool _isPercentages;

    private TextMeshProUGUI _textMeshPro;
    private float _tempValue;

    private void OnValidate()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    protected override void ShowInstant(IDataIndication dataIndication)
    {
        ChangeText(_tempValue = dataIndication.Curent, dataIndication.Max);
    }

    protected override IEnumerator SmoothRoutine(IDataIndication dataIndication)
    {
        float targetValue = dataIndication.Curent;
        float speedSmooth = Mathf.Abs(targetValue - _tempValue);               

        while (_tempValue !=  targetValue) 
        {
            _tempValue = Mathf.MoveTowards(_tempValue, targetValue, speedSmooth * Time.deltaTime);
            ChangeText(_tempValue, dataIndication.Max);
            yield return null;
        }        
    }

    private void ChangeText(float curent, float max)
    {        
        _textMeshPro.text = _isPercentages ? string.Format("{0:P0}", curent / max) : string.Format($"{curent:n0} / {max:n0}");
    }
}
