using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager Instance;

    [SerializeField] AudioClip StepSound;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] AudioClip JumpGroundSound;

    [SerializeField] AudioClip PickaxeSound;
    [SerializeField] AudioClip SculptingSound;
    [SerializeField] AudioClip SculptingPlusSound;

    [SerializeField] AudioClip BellSound;
    [SerializeField] AudioClip CoinSound;
    [SerializeField] AudioClip ItemSound;

    [SerializeField] AudioClip ClickSound1;
    [SerializeField] AudioClip ClickSound2;
    [SerializeField] AudioClip UISound;


    [SerializeField] AudioSource StepAudio;
    [SerializeField] AudioSource ToolEffectAudio;
    [SerializeField] AudioSource WorkShopAudio;
    [SerializeField] AudioSource UIAudio;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        StepAudio.clip = StepSound;
        ToolEffectAudio.clip = PickaxeSound;
    }

    public void PlayStepSound()
    {
        StepAudio.clip = StepSound;
        StepAudio.Play();
    }

    public void PlayJumpSound()
    {
        StepAudio.clip = JumpSound;
        StepAudio.Play();
    }

    public void PlayJumpGroundSound()
    {
        StepAudio.clip = JumpGroundSound;
        StepAudio.Play();
    }

    public void PlayPickaxeSound()
    {
        ToolEffectAudio.clip = PickaxeSound;
        ToolEffectAudio.Play();
    }
    
    public void PlaySculptingToolSound()
    {
        ToolEffectAudio.clip = SculptingSound;
        ToolEffectAudio.Play();
    }

    public void PlayBellSound()
    {
        WorkShopAudio.clip = BellSound;
        WorkShopAudio.Play();
    }

    public void PlayCoinSound()
    {
        WorkShopAudio.clip = CoinSound;
        WorkShopAudio.Play();
    }

    public void PlayItemSound()
    {
        WorkShopAudio.clip = ItemSound;
        WorkShopAudio.Play();
    }

    public void PlayClick1Sound()
    {
        UIAudio.clip = ClickSound1;
        UIAudio.Play();
    }

    public void PlayClick2Sound()
    {
        UIAudio.clip = ClickSound2;
        UIAudio.Play();
    }

    public void PlayUISound()
    {
        UIAudio.clip = UISound;
        UIAudio.Play();
    }
}
