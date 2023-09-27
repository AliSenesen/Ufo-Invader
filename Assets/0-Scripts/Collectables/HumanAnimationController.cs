using Enums;
using UnityEngine;

namespace _0_Scripts.Collectables
{
    public class HumanAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private HumanAnimationStates animStates;

        private void ChangeAnimData(HumanAnimationStates _humanAnimationStates)
        {
            animStates = _humanAnimationStates;
        }

        public void StartWalkAnim()
        {
            ChangeAnimData(HumanAnimationStates.Walk);
            animator.SetBool("Fall",false);
        }

        public void StartFallAnim()
        {
            ChangeAnimData(HumanAnimationStates.Fall);
            animator.SetBool("Fall",true);
        }

        public void StartRunAnim()
        {
            ChangeAnimData(HumanAnimationStates.Run);
            animator.SetBool("Fall",false);
        }
    }
}