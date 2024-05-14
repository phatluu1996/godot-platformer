
using Godot;

public class PlayerSaberWeapon : PlayerWeapon
{
	public PlayerSaberWeapon(EPlayerWeapon type, Player player, PlayerWeaponController weaponController) : base(type, player, weaponController)
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
			Player.AC.PlayAnimation(animation);
		}


		if (Player.IsAttacking)
		{
			PlayerAnimation currentAnimation = Player.AC.Animation;
			if (Input.Attack.Pressed && currentAnimation.repeat
				&& Player.AS.Frame >= currentAnimation.repeatFrame
				&& Player.AS.FrameProgress >= currentAnimation.repeatProgress)
			{
				Player.AttackIndex = Mathf.Clamp(Player.AttackIndex + 1, 0, 3);
				PlayerAnimation nextAnimation = thisState.Animation[WeaponType].normal[Player.AttackIndex];
				Player.AC.PlayAnimation(nextAnimation, nextAnimation.replayFrame, nextAnimation.replayProgress);
			}

			if (Player.AC.IsAnimationFinished())
			{
				Reset();
				OnAttackFinished(thisState);
				//Find transition of this animation
				PlayerAnimation resumeAnimation = thisState.Animation[EPlayerWeapon.NONE].normal[currentAnimation.resumeIndex];
				Player.AC.PlayAnimation(resumeAnimation, currentAnimation.resumeFrame, currentAnimation.resumeProgress);
			}
		}
	}

	public override bool CanStartAttack(PlayerState thisState)
	{
		switch (thisState.StateKey)
		{
			case EPlayerState.DASH:
				return (thisState as PlayerDashState).wasAttacked == false;

			default:
				return true;
		}
	}

	public override void OnAttackStarted(PlayerState thisState)
	{
		switch (thisState.StateKey)
		{
			case EPlayerState.DASH:
				(thisState as PlayerDashState).wasAttacked = true;
				break;
		}
	}

	public override void OnAttackFinished(PlayerState thisState)
	{

	}

	public override void AttackTransition(PlayerState from, PlayerState to)
	{
		base.AttackTransition(from, to);
		PlayerAnimationPair animation = to.Animation[EPlayerWeapon.BUSTER];
		if (IsTransitionOf((from, to), (EPlayerState.JUMP, EPlayerState.FALL))
		|| IsTransitionOf((from, to), (EPlayerState.WALLJUMP, EPlayerState.FALL)))
		{
			//Skip
		}
		else
		{

		}
	}
}
