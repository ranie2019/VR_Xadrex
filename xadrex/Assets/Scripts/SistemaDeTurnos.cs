using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SistemaDeTurnos : MonoBehaviour
{
    public GameObject[] pecasBrancas;
    public GameObject[] pecasPretas;

    private bool turnoPecasBrancas = false;

    void Start()
    {
        InicializarPecas();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TentarMoverPeca();
        }
    }

    void InicializarPecas()
    {
        // Encontra as peças brancas e pretas no início do jogo
        pecasBrancas = GameObject.FindGameObjectsWithTag("White");
        pecasPretas = GameObject.FindGameObjectsWithTag("Black");

        // Desabilita a interação para as peças pretas no início
        DesativarInteracaoPecas(pecasPretas);
        // Ativa a interação para as peças brancas no início
        AtivarInteracaoPecas(pecasBrancas);
    }

    void TentarMoverPeca()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject pecaSelecionada = hit.collider.gameObject;

            if ((turnoPecasBrancas && pecaSelecionada.CompareTag("White")) ||
                (!turnoPecasBrancas && pecaSelecionada.CompareTag("Black")))
            {
                MoverPeca(pecaSelecionada);

                if (pecaSelecionada.CompareTag("White"))
                {
                    // Se a peça com a tag "White" sofrer colisão, habilita a interação para as peças pretas
                    AtivarInteracaoPecas(pecasPretas);
                }
                else if (pecaSelecionada.CompareTag("Black"))
                {
                    // Desabilita a interação para as peças pretas após o movimento
                    DesativarInteracaoPecas(pecasPretas);
                }

                // Troca o turno após o movimento
                turnoPecasBrancas = !turnoPecasBrancas;
            }
        }
    }

    void MoverPeca(GameObject peca)
    {
        // Lógica de movimento da peça aqui
        // Exemplo simples: Movendo para frente
        peca.transform.Translate(Vector3.forward);
    }

    void AtivarInteracaoPecas(GameObject[] pecas)
    {
        foreach (GameObject peca in pecas)
        {
            XRGrabInteractable grabInteractable = peca.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                // Ativa todas as camadas de interação
                grabInteractable.interactionLayers = -1;
            }
        }
    }

    void DesativarInteracaoPecas(GameObject[] pecas)
    {
        foreach (GameObject peca in pecas)
        {
            XRGrabInteractable grabInteractable = peca.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                // Desativa todas as camadas de interação
                grabInteractable.interactionLayers = 0;
            }
        }
    }
}
