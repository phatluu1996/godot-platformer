using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	public PlayerRaycastController RaycastController;
	public PlayerAnimationController AC;
	public PlayerWeaponController WeaponController;
	public PlayerStateMachine FSM;
	public InputSystem Input;
	public Vector2 velocity;
	[Export]
	public AnimatedSprite2D AS;
	[Export]
	public CollisionShape2D CS;
	[Export]
	public AnimatedSprite2D BusterSprite;
	public float Facing {
		get => Transform.X.X;
		set{
			Transform2D transform = Transform;
			transform.X.X = value;
			Transform = transform;
		}
	}
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

	public void FlipH()
	{
		Facing *= -1f;
	}
	

	public override void _Ready()
	{
		// Facing = 1f;
		EnergyLimit = 16;
		Energy = EnergyLimit;

		//Init input settings
		Input = new InputSystem();

		//Init state machine
		FSM = new PlayerStateMachine(this);

		//Init animation controller
		AC = new PlayerAnimationController(AS, this);

		//Init raycast controller
		RaycastController = new PlayerRaycastController(this);

		//Init weapon controller
		WeaponController = new PlayerWeaponController(this);
					
		//Setting up weapon
		WeaponController.AddWeapon(new PlayerSaberWeapon(EPlayerWeapon.SABER, this, WeaponController));
		WeaponController.AddWeapon(new PlayerBusterWeapon(EPlayerWeapon.BUSTER, this, WeaponController));
		//Select main and sub weapon
		WeaponController.Init(EPlayerWeapon.SABER, EPlayerWeapon.BUSTER);

		//Load animation from json
		PlayerAnimationInitializer.LoadAnimations(AC);

		//Load animation events
		PlayerAnimationEventInitializer.LoadAnimationEvents(AC);		

		//Setting up state machine
		FSM.AddState(EPlayerState.IDLE, new PlayerIdleState(this, FSM, AC.GetState(EPlayerState.IDLE)));
		FSM.AddState(EPlayerState.WALK, new PlayerWalkState(this, FSM, AC.GetState(EPlayerState.WALK)) { Speed = Constants.WALK_SPEED });
		FSM.AddState(EPlayerState.JUMP, new PlayerJumpState(this, FSM, AC.GetState(EPlayerState.JUMP)));
		FSM.AddState(EPlayerState.FALL, new PlayerFallState(this, FSM, AC.GetState(EPlayerState.FALL)));
		FSM.AddState(EPlayerState.LAND, new PlayerIdleState(this, FSM, AC.GetState(EPlayerState.LAND)));
		FSM.AddState(EPlayerState.DASH, new PlayerDashState(this, FSM, AC.GetState(EPlayerState.DASH)));
		FSM.AddState(EPlayerState.WALLCLING, new PlayerWallClingState(this, FSM, AC.GetState(EPlayerState.WALLCLING)));
		FSM.AddState(EPlayerState.WALLJUMP, new PlayerWallJumpState(this, FSM, AC.GetState(EPlayerState.WALLJUMP)));
		FSM.AddState(EPlayerState.HURT, new PlayerHurtState(this, FSM, AC.GetState(EPlayerState.HURT)));
		FSM.AddState(EPlayerState.CLIMB, new PlayerClimbState(this, FSM, AC.GetState(EPlayerState.CLIMB)));
		FSM.AddState(EPlayerState.CLIMBDOWN, new PlayerClimbDownState(this, FSM, AC.GetState(EPlayerState.CLIMBDOWN)));
		FSM.AddState(EPlayerState.CLIMBUP, new PlayerClimbUpState(this, FSM, AC.GetState(EPlayerState.CLIMBUP)));
		FSM.AddState(EPlayerState.GRIP, new PlayerGrippingState(this, FSM, AC.GetState(EPlayerState.GRIP)));

		//Add animation transition event
		PlayerStateTransitionInitializer.AddStateTransitions(FSM);

		//Start default state
		FSM.Start(EPlayerState.IDLE);


		//Hide buster sprite
		BusterSprite.Hide();
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

	public bool CanWalkWhenAttacking()
	{
		if (!IsAttacking || WeaponController.Main.WeaponType == EPlayerWeapon.BUSTER)
		{
			return true;
		}

		return false;
	}

	public void Damage(int damage)
	{
	}
}
