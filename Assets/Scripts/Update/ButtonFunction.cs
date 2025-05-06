using UnityEngine;

public class ButtonFunction : MonoBehaviour
{ //���� �������� �������, �� ��������� 10 20 50, ���� �����, �� ����� 1 2 3
    public int FireRateUpdatePercent;
    public int DamageUpdatePercent;
    public int MagazineValueUpdateValue;
    public int PlayerHpBuffValue;
    public int PlayerSpeedBuffAfterDamagePercent;

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
    {
        Debug.Log("���������������� ���� " + StaticHolder.CurrentGunFireRate);
        StaticHolder.CurrentGunFireRate = StaticHolder.CurrentGunFireRate * (1 - FireRateUpdatePercent / 100);
        Debug.Log("���������������� ��������� �� " + StaticHolder.CurrentGunFireRate);
    }
    public void UpdateDamage()
    {
        Debug.Log("���� ��� " + StaticHolder.CurrentGunDamage);
        StaticHolder.CurrentGunDamage = StaticHolder.CurrentGunDamage * (1 + DamageUpdatePercent / 100);
        Debug.Log("���� �������� �� " + StaticHolder.CurrentGunDamage);
    }
    public void UpdateMagazineValue()
    {
        Debug.Log("������ �������� ��� " + StaticHolder.CurrentGunMaxAmmo);
        StaticHolder.CurrentGunMaxAmmo = StaticHolder.CurrentGunMaxAmmo * (MagazineValueUpdateValue);
        Debug.Log("������ �������� �������� �� " + StaticHolder.CurrentGunMaxAmmo);
    }
    public void UpdateLCU()
    {
        StaticHolder.Difficulty = false;
        Debug.Log("��� ��������");
    }
    public void UpdateChangeCurrentGunTo0()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 0;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo1()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 1;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo2()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 2;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo3()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 3;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo5()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 5;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo6()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 6;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo7()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 7;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateChangeCurrentGunTo8()
    {
        Debug.Log("������ ���� " + StaticHolder.CurrentGun);
        StaticHolder.CurrentGun = 8;
        Debug.Log("������ ����� " + StaticHolder.CurrentGun);
    }
    public void UpdateAddGrenade()
    {
        Debug.Log("������� � ���������" + StaticHolder.CurrentGrenade);
        StaticHolder.CurrentGrenade = true;
        Debug.Log("������� � ���������" + StaticHolder.CurrentGrenade);
    }
    public void UpdateHPBuff()
    {
        Debug.Log("������� ������������ �������� ������ - " + StaticHolder.PlayerHP);
        StaticHolder.PlayerHP += PlayerHpBuffValue;
        Debug.Log("����� ������������ �������� ������ - " + StaticHolder.PlayerHP);
    }
    public void UpdateSpeedUp()
    {
        Debug.Log("������� ������������ �������� ������ - " + StaticHolder.PlayerSpeed);
        StaticHolder.SpeedBuffAfterDamage = true;
        StaticHolder.SpeedAfterDamageValue = StaticHolder.SpeedAfterDamageValue * (1 + PlayerSpeedBuffAfterDamagePercent / 100);
        Debug.Log("����� ������������ �������� ������ - " + StaticHolder.PlayerSpeed);

    }
    public void UpdateHealer()
    {
        Debug.Log("�������� ���� - " + StaticHolder.PropitalHeal);
        StaticHolder.PropitalHeal = true;
        Debug.Log("�������� ������ - " + StaticHolder.PropitalHeal);
    }
    public void UpdateSandevistan()
    {
        Debug.Log("����������� ���� - " + StaticHolder.Sandevistan);
        StaticHolder.Sandevistan = true;
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
