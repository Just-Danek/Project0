using UnityEngine;

public class XRBodyColliderSync : MonoBehaviour
{
    [Header("XR Camera (������: XR Origin/Main Camera)")]
    [SerializeField] private Transform cameraTransform;

    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController; // ���������� CharacterController

    [Header("��������� Capsule")]
    [SerializeField] private float skinWidth = 0.05f; // ������ �� ����
    [SerializeField] public GameObject PosBelt;

    private void Reset()
    {
        cameraTransform = Camera.main?.transform;
        characterController = GetComponent<CharacterController>(); // �������� CharacterController
    }

    private void LateUpdate()
    {
        if (cameraTransform == null || characterController == null)
        {
            Debug.LogWarning("CameraTransform ��� CharacterController �� ���������.");
            return;
        }

        // ��� ���������� ������� ������ � �������
        Debug.Log($"[DEBUG] cameraTransform.position = {cameraTransform.position}");
        Debug.Log($"[DEBUG] XR Origin (this.transform.position) = {transform.position}");

        // �������� ��������� ������� ������ ������������ XR Origin
        Vector3 localHeadPos = transform.InverseTransformPoint(cameraTransform.position);
        Debug.Log($"[DEBUG] localHeadPos = {localHeadPos}");

        // ��������� ������ CharacterController (� ������ skin width)
        float newHeight = Mathf.Clamp(localHeadPos.y, 0.5f, 3.0f);
        characterController.height = newHeight;
        characterController.center = new Vector3(localHeadPos.x, newHeight / 2f + skinWidth, localHeadPos.z);
        //PosBelt.transform.position = new Vector3(0, 0.5f, 0);
        Debug.Log($"[DEBUG] CharacterController height = {characterController.height}, center = {characterController.center}");
    }
}
