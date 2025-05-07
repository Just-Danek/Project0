using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRGrenade : MonoBehaviour
{
    [Header("�����")]
    public float delay = 3f;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public int damage = 100;
    public GameObject explosionEffect;
    public AudioClip explosionSound; // ���� ������

    private bool hasExploded = false;
    private bool timerStarted = false;

    [SerializeField] private XRGrabInteractable grabInteractable;
    public InputActionProperty LeftTrigger;
    public InputActionProperty RightTrigger;
    public InputActionProperty LeftGrip;
    public InputActionProperty RightGrip;
    bool IsReady = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!timerStarted && IsReady)
        {
            timerStarted = true;
            Invoke(nameof(Explode), delay);
        }
    }
    private void Awake()
    {
        LeftGrip.action.Enable();
        RightGrip.action.Enable();
        LeftTrigger.action.Enable();
        RightTrigger.action.Enable();
    }
    private void Update()
    {
        if ((LeftGrip.action.ReadValue<float>() > 0.8f && LeftTrigger.action.ReadValue<float>() > 0.8f) || (RightGrip.action.ReadValue<float>() > 0.8f && RightTrigger.action.ReadValue<float>() > 0.8f))
        {
            IsReady = true;
        }
    }
    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        //  ��������������� �����
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //  ���������� ������
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            Destroy(explosion, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        //  ���� ������
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy")|| nearbyObject.CompareTag("Body"))
            {
                EnemyHeaths enemy = nearbyObject.GetComponentInParent<EnemyHeaths>();
                if (enemy != null)
                    Debug.Log("���� ������!");
                    enemy.TakeDamage(damage);
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(gameObject);
    }
}
