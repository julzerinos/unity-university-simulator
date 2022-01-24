using UnityEngine;

public class Walking : MonoBehaviour
{
    private AudioSource _walkingSource;

    private Animator _cameraAnimator;
    private static readonly int IsMoving = Animator.StringToHash("IsWalking");

    private void Awake()
    {
        _walkingSource = GetComponent<AudioSource>();
        _cameraAnimator = GetComponent<Animator>();
    }

    public void Walk()
    {
        _cameraAnimator.SetBool(IsMoving, true);
    }

    public void StopWalk()
    {
        _cameraAnimator.SetBool(IsMoving, false);
    }
    
    public void Run(bool isRunning)
    {
        _cameraAnimator.speed = isRunning ? 1.4f : 1f;
    }

    // public void Jump()
    // {
    //     _cameraAnimator.Rebind();
    //     _cameraAnimator.Update(0f);
    // }
    
    public void PlayStepSound()
    {
        _walkingSource.Play();
    }
}