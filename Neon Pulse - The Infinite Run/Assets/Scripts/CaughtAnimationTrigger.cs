using UnityEngine;

public class CaughtAnimationTrigger : MonoBehaviour
{
    public Animator animator;
    public string animationTrigger = "PlayCaught";

    public void PlayCaughtAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(animationTrigger);
        }
        else
        {
            Debug.LogWarning("Animator not assigned to CaughtAnimationTrigger.");
        }
    }
}