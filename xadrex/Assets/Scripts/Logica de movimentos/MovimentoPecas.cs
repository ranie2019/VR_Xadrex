using UnityEngine;

public class MovimentoPecas : MonoBehaviour
{
    private bool isPecaSelecionada = false;
    private Transform pecaSelecionada;
    private Vector3 offset;

    void Update()
    {
        // Verifica se o bot�o de intera��o foi pressionado (substitua "Fire1" pelo bot�o desejado no seu controle VR)
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use o ponto de origem do controle VR

            // Realiza um raio para detectar se atingiu uma pe�a
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica se atingiu uma pe�a (marque suas pe�as com a tag apropriada)
                if (hit.collider.CompareTag("Peca"))
                {
                    SelecionarPeca(hit.transform);
                }
            }
        }

        // Atualiza a posi��o da pe�a se estiver selecionada
        if (isPecaSelecionada)
        {
            AtualizarPosicaoPeca();
        }
    }

    // Seleciona uma pe�a para movimento
    void SelecionarPeca(Transform peca)
    {
        isPecaSelecionada = true;
        pecaSelecionada = peca;

        // Calcula o offset entre a posi��o da c�mera e a posi��o da pe�a para um movimento suave
        offset = pecaSelecionada.position - Camera.main.transform.position;
    }

    // Atualiza a posi��o da pe�a durante o movimento
    void AtualizarPosicaoPeca()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use o ponto de origem do controle VR
        RaycastHit hit;

        // Realiza um raio para detectar se atingiu uma posi��o v�lida no tabuleiro
        if (Physics.Raycast(ray, out hit))
        {
            // Verifica se atingiu uma posi��o v�lida (marque as posi��es v�lidas com a tag apropriada)
            if (hit.collider.CompareTag("PosicaoValida"))
            {
                // Move a pe�a para a posi��o v�lida
                pecaSelecionada.position = hit.point + offset;
            }
        }

        // Verifica se o bot�o de intera��o foi solto (substitua "Fire1" pelo bot�o desejado no seu controle VR)
        if (Input.GetButtonUp("Fire1"))
        {
            // Finaliza o movimento da pe�a
            isPecaSelecionada = false;
            pecaSelecionada = null;
        }
    }
}
