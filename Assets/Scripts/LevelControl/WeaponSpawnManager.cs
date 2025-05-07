using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{
    [Header("���������")]
    [Tooltip("0 - ���, 1 - �������� ��������, 2 - �������� ��������, 3 - ��������; 4 - �������; 5 - �������� P40; 6 - ��������; 7 - ����������� �������; 8 - ����������� ����")]
    public int weaponIndex;

    [Header("����� ������")]
    public Transform weaponSpawnPoint;
    public Transform weaponSpawnPointAkimbo;
    public Transform ammoSpawnPoint1;
    public Transform ammoSpawnPoint2;
    public Transform ammoSpawnPoint3;
    public Transform ammoSpawnPoint4;
    public Transform ammoSpawnPoint5;
    public Transform ammoSpawnPoint6;
    public Transform ammoSpawnPoint7;
    public Transform ammoSpawnPoint8;
    public Transform ammoSpawnPoint9;

    [Header("������� ������")]
    public GameObject AKM;
    public GameObject LaserPistol;
    public GameObject LaserRifle;
    public GameObject Pistol;
    public GameObject P40;
    public GameObject Shotgun;
    public GameObject CopBaton;
    public GameObject Beat;

    [Header("������� �����������")]
    public GameObject pistolAmmoPrefab;
    public GameObject AKMAmmoPrefab;
    public GameObject shotgunAmmoPrefab;
    public GameObject P40AmmoPrefab;

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        GameObject weaponToSpawn = null;
        GameObject ammoToSpawn = null;

        switch (StaticHolder.CurrentGun)
        {
            case 0:
                weaponToSpawn = AKM;
                ammoToSpawn = AKMAmmoPrefab;
                break;
            case 1:
                weaponToSpawn = LaserPistol;
                break;
            case 2:
                weaponToSpawn = LaserRifle;
                break;
            case 3:
                weaponToSpawn = Pistol;
                ammoToSpawn = pistolAmmoPrefab;
                break;
            case 5:
                weaponToSpawn = P40;
                ammoToSpawn = P40AmmoPrefab;
                break;
            case 6:
                weaponToSpawn = Shotgun;
                ammoToSpawn = shotgunAmmoPrefab;
                break;
            default:
                Debug.LogWarning("�������� ������ ������!");
                return;
        }

        if (weaponToSpawn != null && weaponSpawnPoint != null)
            Instantiate(weaponToSpawn, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
        if (StaticHolder.Akimbo)
        {
            Instantiate(weaponToSpawn, weaponSpawnPointAkimbo.position, weaponSpawnPointAkimbo.rotation);
        }
        if (ammoToSpawn != null && ammoSpawnPoint1 != null)
        {
            Instantiate(ammoToSpawn, ammoSpawnPoint1.position, ammoSpawnPoint1.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint2.position, ammoSpawnPoint2.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint3.position, ammoSpawnPoint3.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint4.position, ammoSpawnPoint4.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint5.position, ammoSpawnPoint5.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint6.position, ammoSpawnPoint6.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint7.position, ammoSpawnPoint7.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint8.position, ammoSpawnPoint8.rotation);
            Instantiate(ammoToSpawn, ammoSpawnPoint9.position, ammoSpawnPoint9.rotation);

        }
    }
}
