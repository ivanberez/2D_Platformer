using UnityEngine;

public class Indicator<T> : MonoBehaviour where T : IDataIndication
{
    [ContextMenuItem("Try define indications in childrens", "TryDefineIndicationsInChildrens")]
    [SerializeField] protected IndicationElement[] IndicationElements;
    [SerializeField] protected T DataIndication;

    [SerializeField] private bool _isSmotherImage;
    [SerializeField] private bool _isSmotherText;
    [SerializeField] private bool _isDestroyAtTheEnd;
    
    public void TryDefineIndicationsInChildrens()
    {        
        IndicationElements = transform.GetComponentsInChildren<IndicationElement>();

        if (IndicationElements.Length == 0)
            Debug.Log("No indication elements were found for child objects");
    }

    public virtual void OnEnable()
    {
        DataIndication.Changed += Refresh;
        
        if (_isDestroyAtTheEnd)
            DataIndication.Ending += Destroy;        
    }    

    public virtual void OnDisable()
    {
        DataIndication.Changed -= Refresh;
        DataIndication.Ending -= Destroy;
    }

    public virtual void Start() => Refresh();

    private void Refresh()
    {
        foreach (var indication in IndicationElements)
            indication.Refresh(DataIndication, GetIsSmoother(indication));
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