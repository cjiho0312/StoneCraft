using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void PlayStep()
    {
        AudioManager.Instance.PlayStepSound();
    }

    public void PlayJump()
    {
        AudioManager.Instance.PlayJumpSound();
    }

    public void PlayJumpGround()
    {
        AudioManager.Instance.PlayJumpGroundSound();
    }
}
