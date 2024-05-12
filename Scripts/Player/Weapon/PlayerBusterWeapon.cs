
using Godot;

public class PlayerBusterWeapon : PlayerWeapon
{
    public PlayerBusterWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController) : base(type, player, weaponController)
    {
    }

    public override void Execute(PlayerState thisState)
    {
        base.Execute(thisState);
		

        if (Input.Attack.Pressed && !Player.IsAttacking && CanStartAttack(thisState))
		{
			Player.IsAttacking = true;
			Player.AttackIndex = 0;
			PlayerAnimation animation = thisState.Animation[WeaponType].normal[0];
			OnAttackStarted(thisState);
			Player.AC.PlayAnimation(animation, animation.startFrame, 0);
		}


		if (Player.IsAttacking)
		{
			PlayerAnimation currentAnimation = Player.AC.Animation;
			if (Input.Attack.Pressed
				&& currentAnimation.repeatFrame > 0
				&& Player.AS.Frame >= currentAnimation.repeatFrame
				&& Player.AS.FrameProgress >= currentAnimation.repeatFrameProgess)
			{
				Player.AC.PlayAnimation(thisState.Animation[WeaponType].normal[Player.AttackIndex], 0, 0);
			}

			if (Player.AC.IsAnimationFinished())
			{
				Reset();			
				OnAttackFinished(thisState);	
				Player.AC.PlayAnimation(thisState.Animation[EPlayerWeapon.NONE].normal[currentAnimation.resumeIndex], currentAnimation.resumeFrame, 0);
			}
		}
    }

	public override bool CanStartAttack(PlayerState thisState){
		return true;
	}

	public override void OnAttackStarted(PlayerState thisState){
		
	}

	public override void OnAttackFinished(PlayerState thisState){
		
	}
}
