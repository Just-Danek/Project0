using UnityEngine;

public class BeltFollower : MonoBehaviour
{
    public Transform headTransform; // Main Camera
    public Vector3 localOffset = new Vector3(0, -0.5f, 0.3f); // �������� "�����" �� ������, ��� ���� �� ������ ��� ��������

    void LateUpdate()
    {
        // �������� ������� ����������� ������� (������ ������ �� Y=0)
        Vector3 flatForward = Vector3.ProjectOnPlane(headTransform.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.Cross(Vector3.up, flatForward).normalized;

        // �������� ������� ��������������� ��������
        Quaternion flatRotation = Quaternion.LookRotation(flatForward, Vector3.up);

        // ������� � ��� ���� �� offset ���������� � ���� ���������
        Vector3 worldOffset = flatRotation * localOffset;
        transform.position = headTransform.position + worldOffset;

        // �������� � ������ �� Y
        transform.rotation = flatRotation;
    }
}
