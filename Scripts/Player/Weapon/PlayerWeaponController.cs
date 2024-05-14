

using System.Collections.Generic;

public class PlayerWeaponController
{
    public Player Player;
    public PlayerWeapon Main;
    public PlayerWeapon Sub;
    protected Dictionary<EPlayerWeapon, PlayerWeapon> Weapons;
    public PlayerWeaponController(Player player)
    {
        Player = player;
        Weapons = new Dictionary<EPlayerWeapon, PlayerWeapon>();
    }

    public void Init(EPlayerWeapon main, EPlayerWeapon sub)
    {
        Main = Weapons[main];
        Sub = Weapons[sub];
    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        if (!Weapons.ContainsKey(weapon.WeaponType))
        {
            Weapons.Add(weapon.WeaponType, weapon);
        }
    }

    public void ChangeWeapon(EPlayerWeapon weapon, bool isMainWeapon = true)
    {

    }

    public void SwapWeapon()
    {
        if (!Player.IsAttacking)
        {
            PlayerWeapon temp = Main;
            Main = Sub;
            Sub = temp;
        }
    }

}
