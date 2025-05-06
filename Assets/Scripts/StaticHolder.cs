using UnityEngine;
// ������ ������ �� ���� 0 - ���; 1 - �����. ��������; 2 - �����. ��������; 3 - ��������; 4 - �������; 5 - �������� P40; 6 - ��������; 7 - ����������� �������; 8 - ����������� ����
public static class StaticHolder
{
    //��� �������� � ���������� ����������
    public static float GunVolume = 0.6f;
    public static float EnvVolume = 0.6f;
    public static bool Difficulty = true;
    public static int countShots;
    public static int countHits;
    public static float Damage;

    //��� ��������������� ��������� ������
    public static bool levelCheksComplete;
    public static bool UpdateWasBought;
    public static int CurrentLevel =2;
    public static bool DieStation;

    //��� ��� ����� ��� ������ �����. ���� �� �� ���� �������� � ���� ������ ������ ������ ������
    public static int CurrentGun;
    public static bool CurrentGrenade; //���� ��� ��� ���
    public static float CurrentGunFireRate;// ��� ����� ����� ��� ������, ��� ����� �������
    public static float CurrentGunDamage;
    public static int CurrentGunMaxAmmo;

    public static int BuffGun;
    public static bool BuffGrenade; //���� ��� ��� ���
    public static float BuffGunFireRate = 1f;// ��� ����� ����� ��� ������, ��� ����� �������
    public static float BuffGunDamage = 1f;
    public static float BuffGunMaxAmmo = 1f;

    public static int PlayerHP = 100;
    public static float PlayerSpeed = 100f;

    public static bool SpeedBuffAfterDamage;
    public static float SpeedAfterDamageValue;
    public static bool PropitalHeal;
    public static bool Sandevistan;
    public static bool Akimbo = false;
    public static bool AkimboWas = false;
    public static bool Katana;
    public static bool StrongArms;
    public static float StrongArmsKoef;
    public static bool StrongLegs;
    public static float StrongLegsKoef;

}
