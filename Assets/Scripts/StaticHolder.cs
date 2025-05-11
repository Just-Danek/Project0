using UnityEngine;
// ������ ������ �� ���� 0 - ���; 1 - �����. ��������; 2 - �����. ��������; 3 - ��������; 4 - �������; 5 - �������� P40; 6 - ��������; 7 - ����������� �������; 8 - ����������� ����
public static class StaticHolder
{
    //��� �������� � ���������� ����������
    public static float GunVolume = 0.6f;
    public static float EnvVolume = 0.6f;
    public static bool Difficulty = false;
    public static int countShots;
    public static int countHits;
    public static float Damage;
    public static bool Ciborg;
    public static bool DiedinCyberpunk;

    //��� ��������������� ��������� ������
    public static bool levelCheksComplete;
    public static bool ItemPickedUp = false;
    public static bool UpdateWasBought;
    public static int CurrentLevel =2;
    public static bool DieStation;

    //��� ��� ����� ��� ������ �����. ���� �� �� ���� �������� � ���� ������ ������ ������ ������
    public static int CurrentGun = 0;
    public static bool CurrentGrenade; //���� ��� ��� ���
    public static float CurrentGunFireRate;// ��� ����� ����� ��� ������, ��� ����� �������
    public static float CurrentGunDamage;
    public static int CurrentGunMaxAmmo;

    public static bool UpdateLevelEnd;



    public static bool BuffGrenade = false; //���� ��� ��� ���
    public static float BuffGunFireRate = 1f;// ��� ����� ����� ��� ������, ��� ����� �������
    public static float BuffGunDamage = 1f;
    public static float BuffGunMaxAmmo = 1f;

    public static int PlayerHPBuff = 0;
    public static float PlayerBasicSpeed = 3f;

    public static bool SpeedBuffAfterDamage = false;
    public static float SpeedAfterDamageValue = 1f;
    public static float SpeedTimeAfterDamage;
    public static bool PropitalHeal = false;
    public static bool PropitalHealActive = false;
    public static float PropitalHealValue;
    public static bool Sandevistan = false;
    public static bool SandevistanActive = false;
    public static int SandevistanTime;
    public static float SandevistanTimeSlower;
    public static bool Akimbo = false;
    public static bool AkimboWas = false;
    public static bool Katana = false;
    public static bool StrongArms = false;
    public static float StrongArmsKoef = 1f;
    public static bool StrongLegs = false;
    public static float StrongLegsKoef = 1f;

}
