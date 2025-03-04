using UnityEngine;

public class SpriteSorter : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         SpriteRenderer sr = animator.GetComponent<SpriteRenderer>();
          if (sr != null)
            sr.sortingLayerName = "Slime";
        CapsuleCollider2D capsule = animator.GetComponent<CapsuleCollider2D>();
        if (capsule != null){
            capsule.isTrigger = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    //  public SpriteRenderer spriteRenderer;

    // public void ChangeSortingLayer()
    // {
    //     spriteRenderer.sortingLayerName = "Slime"; // 원하는 레이어 이름으로 변경
    // }
}
