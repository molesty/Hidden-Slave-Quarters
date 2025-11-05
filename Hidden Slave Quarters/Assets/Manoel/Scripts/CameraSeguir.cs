using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public Transform player;
    public float suavidade = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 posicaoDesejada = player.position + offset;
            Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, suavidade * Time.deltaTime);
            transform.position = posicaoSuavizada;
        }
    }
}