using System.Collections;
using UnityEngine;

public class StoneObject : MonoBehaviour
{
    [SerializeField] int StoneId;
    [SerializeField] ParticleSystem CrashEffect;

    public int GetStoneID() {  return StoneId; }

    private void Start()
    {
        CrashEffect.Stop();
        CrashEffect.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            StartCoroutine(Crash());
        }
    }

    private void PlayCrashEffect()
    {
        CrashEffect.gameObject.SetActive(true);
        CrashEffect.Play();
    }

    IEnumerator Crash()
    {
        PlayCrashEffect();
        StartCoroutine(Disappear());
        yield return new WaitForSeconds(1f);
        Debug.Log("와장창 깨져버린 돌");
        Destroy(gameObject);
    }

    IEnumerator Disappear()
    {
        bool isDisappeared = false;

        while (!isDisappeared)
        {
            if (isDisappeared) break;

            gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

            if (gameObject.transform.localScale.x <= 0.1f)
            {
                isDisappeared = true;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
