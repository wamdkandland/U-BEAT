using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelAudioController : MonoBehaviour
{
    [System.Serializable]
    public class PanelSlot
    {
        public RectTransform panel;
        public AudioSource audioSource;
    }

    public List<PanelSlot> panelSlots;

    private const float Z_THRESHOLD = 100f; // �߽����� �ν��� Z ��

    public void CheckAndPlayAudio()
    {
        foreach (PanelSlot slot in panelSlots)
        {
            float z = slot.panel.anchoredPosition3D.z;

            if (Mathf.Approximately(z, Z_THRESHOLD))
            {
                if (!slot.audioSource.isPlaying)
                    slot.audioSource.Play();
            }
            else
            {
                if (slot.audioSource.isPlaying)
                    slot.audioSource.Stop();
            }
        }
    }
}
