using UnityEngine;

public class ButtonFunction : MonoBehaviour
{ //���� �������� �������, �� ��������� 10 20 50, ���� �����, �� ����� 1 2 3
    [Header("�������� ��������")]
    public int FireRateUpdatePercent;
    [Header("���� ������")]
    public int DamageUpdatePercent;
    [Header("������ ��������")]
    public float MagazineValueUpdateValue;
    [Header("�������� �������� ������")]
    public int PlayerHpBuffValue;
    [Header("��������� ����� ��������� �����")]
    public int PlayerSpeedBuffAfterDamagePercent;
    public float PlayerSpeedBuffAfterDamageTime;
    [Header("������ ������")]
    public int HealHPPoints;
    [Header("�����������")]
    public int SandewistanTimeWorkable;
    public float SandewistanTimeSlower;//����������� ����� � ����� 1


    public int MeleeDamageBuffPercent;
    public int SpeedBuffAllTimePercent;

    public int PlayerHandDamageBuffPercent;
    public int PlayerSpeedBuffPercent;

    //������ ���������� ��������, ��� ��������� �������� ���������� ����� ��� ���������
    //�� ������ �������� ����� �� ��������� ��� ��������� ���� ��������
    public void ShablonButton()
    {
        Debug.Log("");

        Debug.Log("");
    }
    public void UpdateFireRate() //��� ������, ��� �������� �������� ������ ������. FireRate - ����� ����� ����������
    {//��� ��������
        Debug.Log("���������������� ���� " + StaticHolder.BuffGunFireRate);
        StaticHolder.BuffGunFireRate = StaticHolder.BuffGunFireRate * (1f - FireRateUpdatePercent / 100f);
        Debug.Log("���������������� ��������� �� " + StaticHolder.BuffGunFireRate);
    }
    public void UpdateDamage()
    {//��� ��������
        Debug.Log("���� ��� " + StaticHolder.BuffGunDamage);
        StaticHolder.BuffGunDamage = StaticHolder.BuffGunDamage * (1f + DamageUpdatePercent/100f);
        Debug.Log("���� �������� �� " + StaticHolder.BuffGunDamage);
    }
    public void UpdateMagazineValue()
    {//��� ��������
        Debug.Log("������ �������� ��� " + StaticHolder.BuffGunMaxAmmo);
        StaticHolder.BuffGunMaxAmmo = StaticHolder.BuffGunMaxAmmo * (MagazineValueUpdateValue);
        Debug.Log("������ �������� �������� �� " + StaticHolder.BuffGunMaxAmmo);
    }
    public void UpdateLCU()
    {//��� ��������
        StaticHolder.Difficulty = false;
        Debug.Log("��� ��������");
    }
    public void UpdateChangeCurrentGunTo0()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 0;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo1()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 1;
        if (StaticHolder.AkimboWas || StaticHolder.Akimbo)
        {
            StaticHolder.Akimbo = true;
        }
        StaticHolder.AkimboWas = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo2()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 2;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo3()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 3;
        if (StaticHolder.AkimboWas || StaticHolder.Akimbo)
        {
            StaticHolder.Akimbo = true;
        }
        StaticHolder.AkimboWas = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo5()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 5;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo6()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 6;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo7()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 7;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo8()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 8;
        if (StaticHolder.Akimbo)
        {
            StaticHolder.AkimboWas = true;
        }
        StaticHolder.Akimbo = false;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateAddGrenade()
    {
        Debug.Log("������� � ���������" + StaticHolder.CurrentGrenade);
        StaticHolder.CurrentGrenade = true;
        Debug.Log("������� � ���������" + StaticHolder.CurrentGrenade);
    }
    public void UpdateHPBuff()
    {//��� ��������
        Debug.Log("������� ������������ �������� ������ - " + StaticHolder.PlayerHPBuff);
        StaticHolder.PlayerHPBuff += PlayerHpBuffValue;
        Debug.Log("����� ������������ �������� ������ - " + StaticHolder.PlayerHPBuff);
    }
    public void UpdateSpeedUp()// � ������� �������� ������������
    {//������ ��������
        Debug.Log("������� ������������ �������� ������ - " + StaticHolder.PlayerBasicSpeed);
        StaticHolder.SpeedBuffAfterDamage = true;
        StaticHolder.SpeedTimeAfterDamage = PlayerSpeedBuffAfterDamageTime;
        StaticHolder.SpeedAfterDamageValue = StaticHolder.SpeedAfterDamageValue * (1f + PlayerSpeedBuffAfterDamagePercent / 100f);
        Debug.Log("����� ������������ �������� ������ - " + StaticHolder.PlayerBasicSpeed);
    }
    public void UpdateHealer()// � ������� �������� ������������
    {//�������� �� ����� �����������! ������� ������ �� ����
        Debug.Log("�������� ���� - " + StaticHolder.PropitalHeal);
        StaticHolder.PropitalHeal = true;
        StaticHolder.PropitalHealValue = HealHPPoints;
        Debug.Log("�������� ������ - " + StaticHolder.PropitalHeal);
    }
    public void UpdateSandevistan()// � ������� �������� ������������
    {//��� ������� ��������, � � ����
        Debug.Log("����������� ���� - " + StaticHolder.Sandevistan);
        StaticHolder.Sandevistan = true;
        StaticHolder.SandevistanTime = SandewistanTimeWorkable;
        StaticHolder.SandevistanTimeSlower = SandewistanTimeSlower;
        Debug.Log("����������� ������ - " + StaticHolder.Sandevistan);
    }
    public void UpdateAkimbo()
    {
        Debug.Log("������ ���� - " + StaticHolder.Akimbo);
        StaticHolder.Akimbo = true;
        Debug.Log("������ ������ - " + StaticHolder.Akimbo);
    }
    public void UpdateKatana()
    {
        Debug.Log("������ ���� - " + StaticHolder.Katana);
        StaticHolder.Katana = true;
        Debug.Log("������ ������ - " + StaticHolder.Katana);
    }
    public void UpdateStrongArm()
    {
        Debug.Log("������� ���� ���� - " + StaticHolder.StrongArms);
        StaticHolder.StrongArms = true;
        StaticHolder.StrongArmsKoef += 1 + MeleeDamageBuffPercent/100;
        Debug.Log("������� ���� ������ - " + StaticHolder.StrongArms);
        Debug.Log("���� ����� ������ ������ - " + StaticHolder.StrongArmsKoef);
    }
    public void UpdateStrongLeg()
    {
        Debug.Log("������� ���� ���� - " + StaticHolder.StrongLegs);
        StaticHolder.StrongLegs = true;
        StaticHolder.StrongLegsKoef += 1 + SpeedBuffAllTimePercent / 100;
        Debug.Log("������� ���� ������ - " + StaticHolder.StrongLegs);
        Debug.Log("���� �������� ��� ����� ������ - " + StaticHolder.StrongArmsKoef);
    }
    public void UpdateMeleeBerserk()
    {
        Debug.Log("");

        Debug.Log("");
    }
}
