using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public int FireRateUpdatePercent;
    public int DamageUpdatePercent;
    public int MagazineValueUpdatePercent;
    //������ ���������� ��������, ��� ��������� �������� ���������� ����� ��� ���������
    //�� ������ �������� ����� �� ��������� ��� ��������� ���� ��������
    public void ShablonButton()
    {

    }
    public void UpdateFireRate() //��� ������, ��� �������� �������� ������ ������. FireRate - ����� ����� ����������
    {
        Debug.Log("���������������� ���� " + StaticHolder.CurrentGunFireRate);
        StaticHolder.CurrentGunFireRate = StaticHolder.CurrentGunFireRate * (100 - FireRateUpdatePercent) / 100;
        Debug.Log("���������������� ��������� �� " + StaticHolder.CurrentGunFireRate);
    }
    public void UpdateDamage()
    {
        Debug.Log("���� ��� " + StaticHolder.CurrentGunDamage);
        StaticHolder.CurrentGunDamage = StaticHolder.CurrentGunDamage * (100 + DamageUpdatePercent) / 100;
        Debug.Log("���� �������� �� " + StaticHolder.CurrentGunDamage);
    }
    public void UpdateMagazineValue()
    {
        Debug.Log("������ �������� ��� " + StaticHolder.CurrentGunMaxAmmo);
        StaticHolder.CurrentGunMaxAmmo = StaticHolder.CurrentGunMaxAmmo * (100 + MagazineValueUpdatePercent)/100;
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
}
