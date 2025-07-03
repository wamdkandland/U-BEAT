using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VRSlideHandler : MonoBehaviour
{
    public Transform[] panelSlots;
    private Vector3 initialPosition;
    private XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        initialPosition = transform.position;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        // ���� ������ �� ���� ����� �������� ����
        Transform nearest = panelSlots[0];
        float minDist = Vector3.Distance(transform.position, nearest.position);
        foreach (var slot in panelSlots)
        {
            float dist = Vector3.Distance(transform.position, slot.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = slot;
            }
        }

        // �ε巴�� �̵�
        StartCoroutine(SmoothMove(nearest.position));
    }

    IEnumerator SmoothMove(Vector3 target)
    {
        float time = 0f;
        Vector3 start = transform.position;
        while (time < 0.3f)
        {
            transform.position = Vector3.Lerp(start, target, time / 0.3f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
