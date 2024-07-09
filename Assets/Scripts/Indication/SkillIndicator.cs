using UnityEngine;

public class SkillIndicator : Indicator<TimerSkill>
{
    [SerializeField] private IndicationElement _textIndication;

    public override void OnEnable()
    {
        base.OnEnable();
        DataIndication.ActionFinishing += ShowText;
        DataIndication.Ending += HideText;
        DataIndication.Disabling += Disable;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        DataIndication.ActionFinishing -= ShowText;
        DataIndication.Ending -= HideText;
        DataIndication.Disabling -= Disable;
    }

    public override void Start()
    {
        base.Start();        
        HideText();
    }

    private void ShowText() => _textIndication.gameObject.SetActive(true);

    private void HideText() => _textIndication.gameObject.SetActive(false);

    private void Disable() => gameObject.SetActive(false);
}
