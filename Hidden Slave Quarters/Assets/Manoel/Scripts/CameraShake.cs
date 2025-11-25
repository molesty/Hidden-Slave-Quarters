using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float intensidade = 0.1f;
    public float duracao = 0.3f;
    Vector3 posOriginal;

    public void Shake()
    {
        posOriginal = transform.localPosition;
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    System.Collections.IEnumerator ShakeRoutine()
    {
        float tempo = 0;
        while (tempo < duracao)
        {
            transform.localPosition = posOriginal + (Vector3)Random.insideUnitCircle * intensidade;
            tempo += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = posOriginal;
    }
}
