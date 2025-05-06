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

    //��� ��������������� ��������� ������
    public static bool levelCheksComplete;
    public static bool UpdateWasBought;
    public static int CurrentLevel =2;
    public static bool DieStation;

    //��� ��� ����� ��� ������ �����. ���� �� �� ���� �������� � ���� ������ ������ ������ ������
    public static int CurrentGun;
    public static bool CurrentGrenade; //���� ��� ��� ���
    public static float CurrentGunFireRate = 100f;// ��� ����� ����� ��� ������, ��� ����� �������
    public static float CurrentGunDamage = 100f;
    public static int CurrentGunMaxAmmo = 100;

    public static int PlayerHP = 100;
    public static float PlayerSpeed = 100f;

    public static bool SpeedBuffAfterDamage;
    public static float SpeedAfterDamageValue;
    public static bool PropitalHeal;
    public static bool Sandevistan;
    public static bool Akimbo;
    public static bool Katana;
    public static bool StrongArms;
    public static float StrongArmsKoef;
    public static bool StrongLegs;
    public static float StrongLegsKoef;

}
