using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IndicationBar : IndicationElement
{
    [SerializeField] private Image.FillMethod _fillMethod;
    [SerializeField] private Image _imagePreview;
    
    private Image _imageIndication;

    private void OnValidate()
    {        
        _imageIndication = GetComponent<Image>();
        _imageIndication.type = Image.Type.Filled;
        _imageIndication.fillMethod = _fillMethod;

        if(_imagePreview)
        {
            _imagePreview.type = Image.Type.Filled;
            _imagePreview.fillMethod = _fillMethod;
        }
    }

    protected override void ShowInstant(IDataIndication dataIndication)
    {
        _imageIndication.fillAmount = dataIndication.Curent / dataIndication.Max;
    }

    protected override IEnumerator SmoothRoutine(IDataIndication dataIndication)
    {
        float targetFillAmount = dataIndication.Curent / dataIndication.Max;
        float speedSmooth = Mathf.Abs(targetFillAmount - _imageIndication.fillAmount);

        if (_imagePreview != null)
            _imagePreview.fillAmount = targetFillAmount;

        while (_imageIndication.fillAmount != targetFillAmount)
        {
            _imageIndication.fillAmount = Mathf.MoveTowards(_imageIndication.fillAmount, targetFillAmount, speedSmooth * Time.deltaTime);
            yield return null;
        }
    }
}