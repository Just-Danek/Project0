using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandBasedGrabPoint : XRGrabInteractable
{
    [SerializeField] private Transform rightHandAttachTransform;
    [SerializeField] private Transform leftHandAttachTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // ����� attachTransform �� ������� ������ �������
        if (args.interactorObject != null)
        {
            var interactorTransform = args.interactorObject.transform;

            if (interactorTransform.CompareTag("RightHand") && rightHandAttachTransform != null)
            {
                attachTransform = rightHandAttachTransform;
            }
            else if (interactorTransform.CompareTag("LeftHand") && leftHandAttachTransform != null)
            {
                attachTransform = leftHandAttachTransform;
            }
        }

        // �����: ������� ������� ����������, ����� ������ ������������� ����������
        base.OnSelectEntering(args);
    }
}

