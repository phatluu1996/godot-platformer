
using Godot;

public class PlayerSaberWeapon : PlayerWeapon
{
    public PlayerSaberWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController) : base(type, player, weaponController)
    {
    }

    public override void Execute(PlayerState thisState)
    {
        base.Execute(thisState);
        if (Input.Attack.Pressed && !Player.IsAttacking)
		{
			Player.IsAttacking = true;
			Player.AttackIndex = 0;
			PlayerAnimation animation = thisState.Animation[WeaponType].normal[0];
			Player.PlayAnimation(animation.name, animation.startFrame, 0);
		}



		if (Player.IsAttacking)
		{
			PlayerAnimation currentAnimation = Player.GetAnimation(Player.AS.Animation);
			if (Input.Attack.Pressed && currentAnimation.canPlayNext
				&& currentAnimation.repeatFrame > 0
				&& Player.AS.Frame >= currentAnimation.repeatFrame
				&& Player.AS.FrameProgress >= currentAnimation.repeatFrameProgess)
			{
				Player.AttackIndex = Mathf.Clamp(Player.AttackIndex + 1, 0, 2);
				Player.PlayAnimation(thisState.Animation[WeaponType].normal[Player.AttackIndex].name, 0, 0);
			}

			if (Player.IsAnimationFinished())
			{

				Player.PlayAnimation(currentAnimation.resumeAnimation, 0, 0);
			}
		}
    }
}
