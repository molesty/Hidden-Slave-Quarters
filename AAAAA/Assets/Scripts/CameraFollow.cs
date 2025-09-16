using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O personagem que a câmera vai seguir
    public float smoothSpeed = 0.125f; // Suavidade do movimento
    public Vector3 offset; // Distância entre a câmera e o personagem

    void LateUpdate()
    {
        if (target != null)
        {
            // Posição desejada = posição do alvo + offset
            Vector3 desiredPosition = target.position + offset;

            // Suaviza o movimento da câmera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplica a posição suavizada
            transform.position = smoothedPosition;
        }
    }
}
