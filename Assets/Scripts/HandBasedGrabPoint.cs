using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class HandBasedGrabPoint : XRGrabInteractable
{
    [SerializeField] private Transform rightHandAttachTransform;
    [SerializeField] private Transform leftHandAttachTransform;

    private FixedJoint joint;
    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
        movementType = MovementType.VelocityTracking;
        trackPosition = true;
        trackRotation = true;


    }
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // ���� ��� ���-�� ������ � �� ������ ��������� ��� ���
        if (isSelected && !IsInteractorAlreadySelected(args.interactorObject))
        {
            // ��������� ������ ������
            return;
        }

        var interactorRb = (args.interactorObject as MonoBehaviour)?.GetComponent<Rigidbody>();
        var thisRb = GetComponent<Rigidbody>();
        if (interactorRb != null && thisRb != null)
        {
            thisRb.isKinematic = false;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = interactorRb;
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
        }

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

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
    private bool IsInteractorAlreadySelected(IXRSelectInteractor interactor)
    {
        foreach (var i in interactorsSelecting)
        {
            if (i == interactor)
                return true;
        }
        return false;
    }
}

