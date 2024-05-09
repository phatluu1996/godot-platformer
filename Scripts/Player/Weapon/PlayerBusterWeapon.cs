
public class PlayerBusterWeapon : PlayerWeapon
{
    public PlayerBusterWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController) : base(type, player, weaponController)
    {
    }

    public override void Execute(PlayerState thisState)
    {
        base.Execute(thisState);
        
    }
}
