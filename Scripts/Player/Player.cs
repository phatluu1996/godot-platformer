using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	public PlayerRaycastController RaycastController;
	public PlayerAnimationController AnimationController;
	public PlayerWeaponController WeaponController;
	public PlayerStateMachine FSM;
	public InputSystem Input;
	public Vector2 velocity;
	[Export]
	public AnimatedSprite2D AS;
	[Export]
	public CollisionShape2D CS;
	public float Facing;
	public bool IsAttacking;
	public int AttackIndex;
	public bool CanClimbLadder;
	public bool CanGrip;
	public bool OnMomentum;
	[Export]
	public Area2D GripTrigger;
	public Node2D Ladder;
	public Area2D GripableObject;
	public int Energy;
	public int EnergyLimit;


	public float x
	{
		get => Position.X;
		set
		{
			Vector2 pos = Position;
			pos.X = value;
			Position = pos;
		}
	}

	public float y
	{
		get => Position.Y;
		set
		{
			Vector2 pos = Position;
			pos.Y = value;
			Position = pos;
		}
	}

	public RectangleShape2D CollisionBox => CS.Shape as RectangleShape2D;

	public virtual void SetupAnimation()
	{
		AnimationController = new PlayerAnimationController(AS);
		PlayerAnimationInitializer.LoadAnimations(AnimationController);
		AS.AnimationChanged += () => OnAnimationChanged(AS.Animation);
		AS.FrameChanged += () => OnFrameChangedEvent(AS.Frame);
		AS.AnimationFinished += () => OnAnimationFinished(AS.Animation);
		AS.AnimationLooped += () => OnAnimationLooped(AS.Animation);
	}

	public void FlipH()
	{
		Facing *= -1f;
		AS.FlipH = Facing < 0;
	}

	public override void _Ready()
	{
		Facing = 1f;
		EnergyLimit = 16;
		Energy = EnergyLimit;

		Input = new InputSystem();
		RaycastController = new PlayerRaycastController(this);
		WeaponController = new PlayerWeaponController(this);
		WeaponController.AddWeapon(new PlayerSaberWeapon(EPlayerWeapon.SABER, this, WeaponController));	
		WeaponController.AddWeapon(new PlayerBusterWeapon(EPlayerWeapon.BUSTER, this, WeaponController));	
		WeaponController.Init(EPlayerWeapon.SABER, EPlayerWeapon.BUSTER);

		SetupAnimation();
		FSM = new PlayerStateMachine(this);
		FSM.AddState(EPlayerState.IDLE, new PlayerIdleState(this, FSM, AnimationController.GetState(EPlayerState.IDLE)));
		FSM.AddState(EPlayerState.WALK, new PlayerWalkState(this, FSM, AnimationController.GetState(EPlayerState.WALK)) { Speed = Constants.WALK_SPEED });
		FSM.AddState(EPlayerState.JUMP, new PlayerJumpState(this, FSM, AnimationController.GetState(EPlayerState.JUMP)));
		FSM.AddState(EPlayerState.FALL, new PlayerFallState(this, FSM, AnimationController.GetState(EPlayerState.FALL)));
		FSM.AddState(EPlayerState.LAND, new PlayerIdleState(this, FSM, AnimationController.GetState(EPlayerState.LAND)));
		FSM.AddState(EPlayerState.DASH, new PlayerDashState(this, FSM, AnimationController.GetState(EPlayerState.DASH)));
		FSM.AddState(EPlayerState.WALLCLING, new PlayerWallClingState(this, FSM, AnimationController.GetState(EPlayerState.WALLCLING)));
		FSM.AddState(EPlayerState.WALLJUMP, new PlayerWallJumpState(this, FSM, AnimationController.GetState(EPlayerState.WALLJUMP)));
		FSM.AddState(EPlayerState.HURT, new PlayerHurtState(this, FSM, AnimationController.GetState(EPlayerState.HURT)));
		FSM.AddState(EPlayerState.CLIMB, new PlayerClimbState(this, FSM, AnimationController.GetState(EPlayerState.CLIMB)));
		FSM.AddState(EPlayerState.CLIMBDOWN, new PlayerClimbDownState(this, FSM, AnimationController.GetState(EPlayerState.CLIMBDOWN)));
		FSM.AddState(EPlayerState.CLIMBUP, new PlayerClimbUpState(this, FSM, AnimationController.GetState(EPlayerState.CLIMBUP)));
		FSM.AddState(EPlayerState.GRIP, new PlayerGrippingState(this, FSM, AnimationController.GetState(EPlayerState.GRIP)));
		FSM.Start(EPlayerState.IDLE);

		

	}


	public override void _Process(double delta)
	{
		base._Process(delta);

		velocity = Velocity;

		RaycastController.Execute();

		FSM.Excute();

		if (Godot.Input.IsActionJustPressed("ui_select"))
		{
			FSM.SetNextState(EPlayerState.HURT);
		}

		Velocity = velocity;

		MoveAndSlide();
	}

	public void GravityForceApply()
	{
		velocity.Y = Mathf.Clamp(velocity.Y + Constants.GRAVITY * (float)GetProcessDeltaTime(), -Constants.JUMP_FORCE, Constants.JUMP_FORCE);
	}

	public void GravityForceApply(float min, float max)
	{
		velocity.Y = Mathf.Clamp(velocity.Y + Constants.GRAVITY * (float)GetProcessDeltaTime(), min, max);
	}

	public bool CanWalkWhenAttacking(){
		if(!IsAttacking || WeaponController.Main.WeaponType == EPlayerWeapon.BUSTER){
			return true;
		}

		return false;
	}

	public void Damage(int damage)
	{
	}

	public virtual void PlayAnimation(string name, int frame = 0, float frameProgress = 0, bool playBack = false)
	{
		if(frame == -1){
			frame = AS.Frame;
			frameProgress = AS.FrameProgress;
		}
		AS.Play(name, fromEnd: playBack);		
		AS.SetFrameAndProgress(frame, frameProgress);
	}

	public bool IsAnimationFinished()
	{
		int frameCount = AS.SpriteFrames.GetFrameCount(AS.Animation) - 1;
		return AS.Frame >= frameCount && AS.FrameProgress == 1f;
	}

	public PlayerAnimation GetAnimation(string name)
	{
		return AnimationController.GetAnimation(name);
	}

	public virtual void OnAnimationChanged(string animationName)
	{
	}

	public virtual void OnFrameChangedEvent(int frame)
	{
		FSM.OnFrameChangedEvent(frame);
	}

	public virtual void OnAnimationFinished(string animationName)
	{
		FSM.OnAnimationFinished(animationName);
	}

	public virtual void OnAnimationLooped(string animationName)
	{
		int loopFrame = GetAnimation(animationName).loopFrame;
		if (loopFrame != 0)
		{
			AS.SetFrameAndProgress(loopFrame, 0);
		}
		FSM.OnAnimationLooped(animationName);
	}

	public virtual void LogicUpdate(PlayerState thisState)
	{

		WeaponController.Main.Execute(thisState);
	}
}
