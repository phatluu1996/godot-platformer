public class PlayerWeapon
{
    PlayerWeaponController WeaponController;
    public EPlayerWeapon WeaponType;
    public Player Player;
    public InputSystem Input;

    public PlayerWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController)
    {
        WeaponType = type;
        Player = player;
        WeaponController = weaponController;
        Input = player.Input;
    }

    public virtual void Execute(PlayerState thisState)
    {

    }

    public virtual void Reset(){
        Player.IsAttacking = false;
        Player.AttackIndex = 0;
    }
}