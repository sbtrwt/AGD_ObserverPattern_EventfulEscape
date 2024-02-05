using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate();
    public static event LightSwitchDelegate OnToggleLightSwitch;

    private void OnEnable()
    {
        OnToggleLightSwitch += OnLightSwtichToggled;
    }
    private void OnDisable()
    {
        OnToggleLightSwitch -= OnLightSwtichToggled;
    }
    private void Start() => currentState = SwitchState.Off;

    public void Interact()
    {
        OnToggleLightSwitch?.Invoke();
    }
    private void ToggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void OnLightSwtichToggled()
    {
        ToggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}
