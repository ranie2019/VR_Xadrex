using UnityEngine;

public class VerificarColisao : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float tempoAtivacao = 3f; // Tempo em segundos

    private void Start()
    {
        // Certifica-se de que o MeshRenderer existe antes de acessá-lo
        InicializarComponentes();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se a colisão ocorreu com um objeto "White" ou "Black"
        if (other.CompareTag("White") || other.CompareTag("Black"))
        {
            // Registra a colisão no console
            Debug.Log("Colisão detectada com um objeto " + other.tag + "! Nome: " + other.gameObject.name);

            // Ativa o MeshRenderer e inicia a contagem regressiva para desativá-lo
            AtivarMeshRenderer();
        }
    }

    private void InicializarComponentes()
    {
        // Obtém o componente MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();

        // Verifica se o MeshRenderer foi encontrado
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer não encontrado no objeto: " + gameObject.name);
        }
        else
        {
            // Desativa o MeshRenderer inicialmente
            meshRenderer.enabled = false;
        }
    }

    private void AtivarMeshRenderer()
    {
        // Ativa o MeshRenderer
        meshRenderer.enabled = true;

        // Inicia a contagem regressiva para desativar o MeshRenderer após o tempo especificado
        Invoke("DesativarMeshRenderer", tempoAtivacao);
    }

    private void DesativarMeshRenderer()
    {
        // Desativa o MeshRenderer após o tempo especificado
        meshRenderer.enabled = false;
    }
}
