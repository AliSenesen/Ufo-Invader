using _0_Scripts.Enums;
using UnityEngine;

namespace _0_Scripts.Collectables
{
    public class AnimalAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private AnimalAnimationStates animStates;

        private void ChangeAnimData(AnimalAnimationStates _animalAnimationStates)
        {
            animStates = _animalAnimationStates;
        }

        public void StartWalkAnim()
        {
            ChangeAnimData(AnimalAnimationStates.Walk);
            animator.SetBool("Run",false);
        }

        public void StartRunAnim()
        {
            ChangeAnimData(AnimalAnimationStates.Run);
            animator.SetBool("Run",true);
        }
    }
}