using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class EventButtonToggle : MonoBehaviour
{
    public string labelPrimary = "button off";
    public string labelSecondary = "button on";

    public Image showWhenOn;
    [SerializeField]
    private bool _state;

void Reset()
{
	text=GetComponentInChildren<Text>();
	if (text!=null) labelPrimary=text.text;
}
    public bool state
    {
        get
        {
            return _state;
        }
        set
        {
			
			if (_state == value) return;
            _state = value;
            if (text != null)
                text.text = (_state ? labelSecondary : labelPrimary);
            if (_state) onToggleOn.Invoke(); else onToggleOff.Invoke();
            onStateChange.Invoke(_state);
            if (showWhenOn != null) showWhenOn.enabled = _state;
        }
    }
    [Header("Bind to state changes")]

    public UnityEvent onToggleOn;
    public UnityEvent onToggleOff;
    public BoolEvent onStateChange;
    [SerializeField]
    [HideInInspector]
    Text text;
    [SerializeField]
    [HideInInspector]
    Button b;

    protected virtual void OnValidate()
    {
        if (text == null) text = GetComponentInChildren<Text>();
        if (b == null) b = GetComponent<Button>();
		
        state = _state;

    }
    public void ToggleState()
    {
        state = !state;
    }
    void Start()
    {
        if (b != null)
        {
            b.onClick.AddListener(ToggleState);
        }
    }






}
