
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
			Player.AC.PlayAnimation(animation, animation.startFrame, 0);
		}


		if (Player.IsAttacking)
		{
			PlayerAnimation currentAnimation = Player.AC.Animation;
			if (Input.Attack.Pressed && currentAnimation.canPlayNext
				&& currentAnimation.repeatFrame > 0
				&& Player.AS.Frame >= currentAnimation.repeatFrame
				&& Player.AS.FrameProgress >= currentAnimation.repeatFrameProgess)
			{
				Player.AttackIndex = Mathf.Clamp(Player.AttackIndex + 1, 0, 2);
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
		switch (thisState.StateKey)
		{
			case EPlayerState.DASH:
				return (thisState as PlayerDashState).wasAttacked == false;

			default:
				return true;
		}
	}

	public override void OnAttackStarted(PlayerState thisState){
		switch (thisState.StateKey)
		{
			case EPlayerState.DASH:
				(thisState as PlayerDashState).wasAttacked = true;
				break;
		}
	}

	public override void OnAttackFinished(PlayerState thisState){
		
	}

    public override void AttackTransition(PlayerState from, PlayerState to)
    {
        base.AttackTransition(from, to);
		PlayerAnimationPair animation = to.Animation[EPlayerWeapon.BUSTER];
		if(IsTransitionOf((from, to), (EPlayerState.JUMP, EPlayerState.FALL)) 
		|| IsTransitionOf((from, to), (EPlayerState.WALLJUMP, EPlayerState.FALL))){
			//Skip
		}else{
			Player.FSM.DefaultTransition(to.StateKey);
		}
    }
}
