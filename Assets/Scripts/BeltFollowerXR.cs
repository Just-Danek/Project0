using UnityEngine;

public class BeltFollower : MonoBehaviour
{
    public Transform headTransform; // ���� ������������� Main Camera
    void LateUpdate()
    {
        // �������� ������� ������ ������ �� Y
        Vector3 forward = Vector3.ProjectOnPlane(headTransform.forward, Vector3.up).normalized;
        if (forward.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}

