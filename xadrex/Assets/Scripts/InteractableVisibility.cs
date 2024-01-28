using UnityEngine;

public class InteractableVisibility : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject plane;  // O plano que você deseja tornar visível
    public LayerMask interactableLayer;  // Camada do objeto interagível
    public float displayDuration = 3f;  // Duração em segundos para exibir o plano

    private bool isVisible = false;

    private void Start()
    {
        InicializarVisibilidade();  // Configura a visibilidade inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifique se o objeto interagível entrou na zona de interação
        if (InteragivelEntrouNaZona(other.gameObject))
        {
            ExibirPlanoPorDuracao(displayDuration);  // Exiba o plano por um determinado tempo
            Debug.Log("Objeto detectado");
        }
    }

    private bool InteragivelEntrouNaZona(GameObject otherObject)
    {
        // Verifica se o objeto está na camada correta
        return otherObject.layer == interactableLayer;
    }

    private void ExibirPlanoPorDuracao(float duracao)
    {
        if (PodeExibirPlano() && !isVisible)
        {
            ConfigurarVisibilidadeDoPlano(true);

            // Use uma coroutine para aguardar um determinado tempo antes de tornar o plano invisível novamente
            StartCoroutine(EsconderAposAtraso(duracao));
        }
    }

    private System.Collections.IEnumerator EsconderAposAtraso(float atraso)
    {
        yield return new WaitForSeconds(atraso);

        // Torna o plano invisível após o tempo especificado
        if (PodeExibirPlano() && isVisible)
        {
            ConfigurarVisibilidadeDoPlano(false);
        }
    }

    private void InicializarVisibilidade()
    {
        ConfigurarVisibilidadeDoPlano(false);
    }

    private bool PodeExibirPlano()
    {
        return plane != null && !isVisible;
    }

    private void ConfigurarVisibilidadeDoPlano(bool visivel)
    {
        if (plane != null)
        {
            plane.SetActive(visivel);
            isVisible = visivel;
        }
    }
}
