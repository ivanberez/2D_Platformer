using UnityEngine;

public class Indicator<T> : MonoBehaviour where T : IDataIndication
{
    [ContextMenuItem("Try define indications in childrens", "TryDefineIndicationsInChildrens")]
    [SerializeField] private IndicationElement[] _indications;

    [SerializeField] private bool _isSmotherImage;
    [SerializeField] private bool _isSmotherText;
    [SerializeField] private bool _isDestroyAtTheEnd;

    [SerializeField] private T _dataIndication;

    public void TryDefineIndicationsInChildrens()
    {        
        _indications = transform.GetComponentsInChildren<IndicationElement>();

        if (_indications.Length == 0)
            Debug.Log("No indication elements were found for child objects");
    }

    private void Awake()
    {
        _dataIndication.Changed += Refresh;
        
        if (_isDestroyAtTheEnd)
            _dataIndication.Ending += Destroy;

        Refresh();
    }

    private void OnDisable()
    {
        _dataIndication.Changed -= Refresh;
        _dataIndication.Ending -= Destroy;
    }

    private void Refresh()
    {
        foreach (var indication in _indications)
            indication.Refresh(_dataIndication, GetIsSmoother(indication));
    }

    private bool GetIsSmoother(IndicationElement indication)
    {
        switch (indication)
        {
            case IndicationText: return _isSmotherText;
            case IndicationBar: return _isSmotherImage;
            default: return false;
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}