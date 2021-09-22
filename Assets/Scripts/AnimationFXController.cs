using UnityEngine;

public class AnimationFXController : MonoBehaviour
{
    [SerializeField] AudioSource[] footsteps;
    [SerializeField] AudioSource[] growls;
    [SerializeField] AudioSource swipe;
    [SerializeField] Collider rightHandCollider;
    [SerializeField] Collider leftHandCollider;
    [SerializeField] Collider headCollider;
    [SerializeField] Animator animator;

    int swipeAnim = Animator.StringToHash("Swipe");
    int swipeMirrorAnim = Animator.StringToHash("SwipeMirror");

    public void PlayFootstep()
    {
        var fx = footsteps[Random.Range(0, footsteps.Length)];
        fx.loop = false;
        fx.pitch = Random.Range(1f, 1.5f);
        fx.Play();
    }

    public void PlaySwipe()
    {
        swipe.pitch = Random.Range(.5f, 1.5f);
        swipe.Play();
    }

    public void PlayAttackSound()
    {
        var fx = growls[Random.Range(0, growls.Length)];
        fx.pitch = Random.Range(.5f, 1.5f);
        Invoke("PlaySwipe", 0.2f);
        fx.Play();
    }

    public void StartSwipe()
    {
        ScoreManager.Instance.SetScoreMultiplier(1.5f);

        var mirror = animator.GetBool(swipeMirrorAnim);

        if (mirror)
        {
            rightHandCollider.enabled = true;
        }
        else
        {
            leftHandCollider.enabled = true;
        }
        
        headCollider.enabled = false;
    }

    public void EndSwipe()
    {
        ScoreManager.Instance.SetScoreMultiplier(1.0f);

        var mirror = animator.GetBool(swipeMirrorAnim);

        if (mirror)
        {
            rightHandCollider.enabled = false;
        }
        else
        {
            leftHandCollider.enabled = false;
        }
    }

    public void EnableHead()
    {
        headCollider.enabled = true;
    }
}
