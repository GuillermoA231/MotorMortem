using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class PPManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume volumeNormal;
    [SerializeField] private PostProcessVolume volumeHurt;

    [SerializeField] private float transitionSpeed = 2f;
    [SerializeField] private float hurtDuration = 0.3f;

    private Coroutine currentRoutine;

    void Start()
    {
        volumeNormal.weight = 1f;
        volumeHurt.weight = 0f;

        volumeNormal.gameObject.SetActive(true);
        volumeHurt.gameObject.SetActive(true);
    }

    public void PlayHurtEffect()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(HurtRoutine());
    }

    private IEnumerator HurtRoutine()
    {
        float t = 0f;

        // Fade IN hurt
        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;

            volumeNormal.weight = 1f - t;
            volumeHurt.weight = t;

            yield return null;
        }

        yield return new WaitForSeconds(hurtDuration);

        t = 1f;

        // Fade OUT hurt
        while (t > 0f)
        {
            t -= Time.deltaTime * transitionSpeed;

            volumeNormal.weight = 1f - t;
            volumeHurt.weight = t;

            yield return null;
        }

        volumeNormal.weight = 1f;
        volumeHurt.weight = 0f;
    }
}
