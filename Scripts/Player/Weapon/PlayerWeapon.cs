using System;

public class PlayerWeapon
{
    public double Timer;
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

    public virtual bool CanStartAttack(PlayerState thisState){
        return true;
	}

    public virtual void OnAttackStarted(PlayerState thisState){

	}

	public virtual void OnAttackFinished(PlayerState thisState){

	}

    public virtual void AttackTransition(PlayerState from, PlayerState to)
    {
        
    }

    public bool IsTransitionOf((PlayerState, PlayerState) states, (EPlayerState, EPlayerState) stateTypes){
        return states.Item1.StateKey == stateTypes.Item1 && states.Item2.StateKey == stateTypes.Item2;
    }
}