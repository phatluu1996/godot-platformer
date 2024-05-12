
using PSTC = PlayerStateTransitionCallback;
public class PlayerStateTransitionInitializer
{
    public static void AddStateTransitions(PlayerStateMachine fsm){
        //Define State transition for all
        fsm.AddStateTransition(new PSTC(fsm, EPlayerState.JUMP, EPlayerState.FALL, EPlayerWeapon.SABER, (from, to) => {
            //Do nothing
        }));

        fsm.AddStateTransition(new PSTC(fsm, EPlayerState.WALLJUMP, EPlayerState.FALL, EPlayerWeapon.SABER, (from, to) => {
            //Do nothing
        }));

        
    }
}
