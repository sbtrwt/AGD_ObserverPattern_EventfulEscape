using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void OnEnable()
    {
        EventService.Instance.OnLightSwitchToggled.AddListener(OnLightSwtichToggled);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(OnLightTurnOffByGhost);
    }
    private void OnDisable()
    {
        EventService.Instance.OnLightSwitchToggled.RemoveListener(OnLightSwtichToggled);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(OnLightTurnOffByGhost);
    }
    private void Start() => currentState = SwitchState.Off;

    public void Interact()
    {
        EventService.Instance.OnLightSwitchToggled.InvokeEvent();
    }
    private void toggleLights()
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
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
    private void SetLights(bool light)
    {
        foreach(Light lightSource in lightsources)
        {
            lightSource.enabled = light;
        }
        if (light) currentState = SwitchState.On;
        else currentState = SwitchState.Off;
    }
    private void OnLightTurnOffByGhost()
    {
        SetLights(false);
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.LightsOff);
    }
}
