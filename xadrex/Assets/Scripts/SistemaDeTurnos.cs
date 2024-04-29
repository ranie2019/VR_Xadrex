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
        // Encontra as pe�as brancas e pretas no in�cio do jogo
        pecasBrancas = GameObject.FindGameObjectsWithTag("White");
        pecasPretas = GameObject.FindGameObjectsWithTag("Black");

        // Desabilita a intera��o para as pe�as pretas no in�cio
        DesativarInteracaoPecas(pecasPretas);
        // Ativa a intera��o para as pe�as brancas no in�cio
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
                    // Se a pe�a com a tag "White" sofrer colis�o, habilita a intera��o para as pe�as pretas
                    AtivarInteracaoPecas(pecasPretas);
                }
                else if (pecaSelecionada.CompareTag("Black"))
                {
                    // Desabilita a intera��o para as pe�as pretas ap�s o movimento
                    DesativarInteracaoPecas(pecasPretas);
                }

                // Troca o turno ap�s o movimento
                turnoPecasBrancas = !turnoPecasBrancas;
            }
        }
    }

    void MoverPeca(GameObject peca)
    {
        // L�gica de movimento da pe�a aqui
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
                // Ativa todas as camadas de intera��o
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
                // Desativa todas as camadas de intera��o
                grabInteractable.interactionLayers = 0;
            }
        }
    }
}
