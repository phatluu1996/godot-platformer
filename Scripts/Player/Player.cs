using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	public PlayerAnimationController AnimationController;
	public PlayerStateMachine FSM;
	public InputSystem Input;
	public Vector2 velocity;
	[Export]
	public AnimatedSprite2D AS { get; set; }
	public float Facing;
	public bool IsAttacking;

	public bool OnMomentum;
	public EPlayerWeapon EquippedWeaponType;

	public override void _Ready()
	{
		Input = new InputSystem();
		SetupAnimation();
		FSM = new PlayerStateMachine(this);
		FSM.AddState(EPlayerState.IDLE, new PlayerIdleState(this, FSM, AnimationController.GetState(EPlayerState.IDLE)));		
		FSM.AddState(EPlayerState.WALK, new PlayerWalkState(this, FSM, AnimationController.GetState(EPlayerState.WALK)){Speed = Constants.WALK_SPEED});
		FSM.AddState(EPlayerState.JUMP, new PlayerJumpState(this, FSM, AnimationController.GetState(EPlayerState.JUMP)));
		FSM.AddState(EPlayerState.FALL, new PlayerFallState(this, FSM, AnimationController.GetState(EPlayerState.FALL)));
		FSM.AddState(EPlayerState.LAND, new PlayerIdleState(this, FSM, AnimationController.GetState(EPlayerState.LAND)));
		FSM.AddState(EPlayerState.DASH, new PlayerDashState(this, FSM, AnimationController.GetState(EPlayerState.DASH)));
		FSM.Start(EPlayerState.IDLE);
		
		Facing = 1f;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		velocity = Velocity;
		FSM.Update();
		Velocity = velocity;
		MoveAndSlide();
	}

	
	public override void _PhysicsProcess(double delta)
	{


		// Add the gravity.
		// if (!IsOnFloor())
		// 	velocity.Y += Constants.GRAVITY * (float)delta;

		// // Handle Jump.
		// if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		// 	velocity.Y = Constants.JUMP_FORCE;

		// // Get the input direction and handle the movement/deceleration.
		// // As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// if (direction != Vector2.Zero)
		// {
		// 	velocity.X = direction.X * Constants.WALK_SPEED;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Constants.WALK_SPEED);
		// }

	}

	public virtual void SetupAnimation()
	{
		AnimationController = new PlayerAnimationController(AS);		
		PlayerAnimationInitializer.LoadAnimations(AnimationController);
		AS.AnimationChanged += () => OnAnimationChanged(AS.Animation);
		AS.FrameChanged += () => OnFrameChangedEvent(AS.Frame);
		AS.AnimationFinished += () => OnAnimationFinished(AS.Animation);
		AS.AnimationLooped += () => OnAnimationLooped(AS.Animation);		
	}

	public void GravityForceApply(){
		velocity.Y = Mathf.Clamp(velocity.Y + Constants.GRAVITY * (float)GetProcessDeltaTime(), -Constants.JUMP_FORCE, Constants.JUMP_FORCE);
	}

	public void Damage(int damage)
	{

	}

	public virtual void OnAnimationChanged(string animationName){
		// GD.Print("Play " + animationName);
	}

	public virtual void OnFrameChangedEvent(int frame)
	{
		FSM.OnFrameChangedEvent(frame);
	}

	public virtual void OnAnimationFinished(string animationName){
		FSM.OnAnimationFinished(animationName);
	}

	public virtual void OnAnimationLooped(string animationName){
		int loopFrame = AnimationController.AllAnimations[animationName].loopFrame;
		if(loopFrame != 0){
			AS.SetFrameAndProgress(loopFrame, 0);			
		}
		// GD.Print("Loop " + animationName);
		FSM.OnAnimationLooped(animationName);
	}	

	public virtual void AnimationTransition(Dictionary<EPlayerWeapon, List<PlayerAnimation>> thisStateAnimation)
	{
		if (!IsAttacking)
		{
			// float frame = thisStateAnimation.transitionFrame;
			// float frameProgress = 0;
			// if (thisStateAnimation.transitionFrame == -1)
			// {
			// 	frame = AS.Frame;
			// 	frameProgress = AS.FrameProgress;
			// }
			PlayAnimation(thisStateAnimation[EPlayerWeapon.NONE][0].name, 0, 0);
		}
	}

	public virtual void PlayAnimation(string name, int frame = 0, int frameProgress = 0){		
		AS.Play(name);
		AS.SetFrameAndProgress(frame, frameProgress);
	}

	
}
