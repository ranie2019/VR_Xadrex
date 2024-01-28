using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 5f; // Velocidade de rotação desejada

    void Update()
    {
        // Girar o objeto em torno do eixo Y
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
