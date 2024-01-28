using UnityEngine;

public class ColisaoTrigger : MonoBehaviour
{
    // Este método é chamado quando o objeto entra em um trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger está na camada "Interactable"
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            // Exibe uma mensagem no console confirmando a colisão
            Debug.Log("Colisão detectada com o objeto Interactable: " + other.gameObject.name);
        }
    }
}
