using UnityEngine;

public class MovimentoPecas : MonoBehaviour
{
    private bool isPecaSelecionada = false;
    private Transform pecaSelecionada;
    private Vector3 offset;

    void Update()
    {
        // Verifica se o botão de interação foi pressionado (substitua "Fire1" pelo botão desejado no seu controle VR)
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use o ponto de origem do controle VR

            // Realiza um raio para detectar se atingiu uma peça
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica se atingiu uma peça (marque suas peças com a tag apropriada)
                if (hit.collider.CompareTag("Peca"))
                {
                    SelecionarPeca(hit.transform);
                }
            }
        }

        // Atualiza a posição da peça se estiver selecionada
        if (isPecaSelecionada)
        {
            AtualizarPosicaoPeca();
        }
    }

    // Seleciona uma peça para movimento
    void SelecionarPeca(Transform peca)
    {
        isPecaSelecionada = true;
        pecaSelecionada = peca;

        // Calcula o offset entre a posição da câmera e a posição da peça para um movimento suave
        offset = pecaSelecionada.position - Camera.main.transform.position;
    }

    // Atualiza a posição da peça durante o movimento
    void AtualizarPosicaoPeca()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use o ponto de origem do controle VR
        RaycastHit hit;

        // Realiza um raio para detectar se atingiu uma posição válida no tabuleiro
        if (Physics.Raycast(ray, out hit))
        {
            // Verifica se atingiu uma posição válida (marque as posições válidas com a tag apropriada)
            if (hit.collider.CompareTag("PosicaoValida"))
            {
                // Move a peça para a posição válida
                pecaSelecionada.position = hit.point + offset;
            }
        }

        // Verifica se o botão de interação foi solto (substitua "Fire1" pelo botão desejado no seu controle VR)
        if (Input.GetButtonUp("Fire1"))
        {
            // Finaliza o movimento da peça
            isPecaSelecionada = false;
            pecaSelecionada = null;
        }
    }
}
