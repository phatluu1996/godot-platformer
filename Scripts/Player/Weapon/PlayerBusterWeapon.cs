
using Godot;

public class PlayerBusterWeapon : PlayerWeapon
{	
	public AnimatedSprite2D BusterSprite;
	public PlayerBusterWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController) : base(type, player, weaponController)
	{
		BusterSprite = player.BusterSprite;
		BusterSprite.Hide();
		Timer = 0;
	}

	public override void Execute(PlayerState thisState)
	{
		base.Execute(thisState);


		if (Input.Attack.Pressed && !Player.IsAttacking && CanStartAttack(thisState))
		{
			Player.IsAttacking = true;
			PlayerAnimation animation = thisState.Animation[WeaponType].normal[0];
			OnAttackStarted(thisState);
			Player.AC.PlayAnimation(animation, animation.startFrame, 0);
		}


		if (Player.IsAttacking)
		{
			Timer += Player.GetProcessDeltaTime();
			PlayerAnimation currentAnimation = Player.AC.Animation;
			if (Input.Attack.Pressed && Timer >= 0.2f)
			{
				Timer = 0;
				if(!currentAnimation.skipReplay && Player.AS.Frame >= currentAnimation.repeatFrame){
					Player.AC.PlayAnimation(currentAnimation, currentAnimation.repeatFrame);
				}				
			}

			if (Timer >= 0.4f)
			{
				Reset();
				OnAttackFinished(thisState);
				Player.AC.PlayAnimation(thisState.Animation[EPlayerWeapon.NONE].normal[currentAnimation.resumeIndex], currentAnimation.resumeFrame);
			}
		}
	}

	public override bool CanStartAttack(PlayerState thisState)
	{
		return true;
	}

	public override void OnAttackStarted(PlayerState thisState)
	{
		BusterSprite.Show();
	}

	public override void OnAttackFinished(PlayerState thisState)
	{
		BusterSprite.Hide();
	}

    public override void Reset()
    {
        base.Reset();
		Timer = 0;
		BusterSprite.Hide();
    }

    public override void AttackTransition(PlayerState from, PlayerState to)
    {
        base.AttackTransition(from, to);
		PlayerAnimationPair animation = to.Animation[EPlayerWeapon.BUSTER];
		if(IsTransitionOf((from, to), (EPlayerState.JUMP, EPlayerState.FALL)) 
		|| IsTransitionOf((from, to), (EPlayerState.WALLJUMP, EPlayerState.FALL))){
			//Skip
		}else{
			Player.AC.PlayAnimation(animation.normal[0]);
		}

    }
}
