using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType soundType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>() != null && keysRequiredToTrigger == GameService.Instance.GetPlayerController().KeysEquipped)
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
            GetComponent<Collider>().enabled = false;
        }
    }
}
