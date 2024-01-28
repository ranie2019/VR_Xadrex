using UnityEngine;

public class Peao : MonoBehaviour
{
    // Representa o estado atual do tabuleiro de xadrez
    private char[,] tabuleiro = new char[,]
    {
        {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'},
        {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
        {'.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.'},
        {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
        {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'}
    };

    // Método chamado na inicialização do script
    void Start()
    {
        ImprimirTabuleiro();
    }

    // Método para imprimir o estado atual do tabuleiro
    void ImprimirTabuleiro()
    {
        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Debug.Log(tabuleiro[linha, coluna] + " ");
            }
            Debug.Log("\n");
        }
    }

    // Verifica se o movimento do peão é válido
    bool MovimentoPeaoValido(Vector2 origem, Vector2 destino)
    {
        int origemColuna = (int)origem.x;
        int origemLinha = (int)origem.y;
        int destinoColuna = (int)destino.x;
        int destinoLinha = (int)destino.y;

        // Verifica se o movimento é na mesma coluna
        if (origemColuna != destinoColuna)
        {
            Debug.Log("Movimento inválido: Movimento deve ser na mesma coluna.");
            return false;
        }

        // Movimento simples para frente
        if (tabuleiro[origemLinha, origemColuna] == 'P' && origemLinha - destinoLinha == 1)
        {
            return true;
        }
        else if (tabuleiro[origemLinha, origemColuna] == 'p' && destinoLinha - origemLinha == 1)
        {
            return true;
        }

        // Movimento duplo inicial
        if (tabuleiro[origemLinha, origemColuna] == 'P' && origemLinha == 6 && origemLinha - destinoLinha == 2)
        {
            return true;
        }
        else if (tabuleiro[origemLinha, origemColuna] == 'p' && origemLinha == 1 && destinoLinha - origemLinha == 2)
        {
            return true;
        }

        Debug.Log("Movimento inválido: Condições não atendidas.");
        return false;
    }

    // Executa o movimento do peão se for válido
    bool MoverPeao(Vector2 origem, Vector2 destino)
    {
        int origemColuna = (int)origem.x;
        int origemLinha = (int)origem.y;
        int destinoColuna = (int)destino.x;
        int destinoLinha = (int)destino.y;

        if (MovimentoPeaoValido(origem, destino))
        {
            // Atualiza o tabuleiro com o novo estado após o movimento
            tabuleiro[destinoLinha, destinoColuna] = tabuleiro[origemLinha, origemColuna];
            tabuleiro[origemLinha, origemColuna] = '.';

            Debug.Log("Movimento do peão executado com sucesso!");
            return true;
        }
        else
        {
            Debug.Log("Movimento do peão não executado: Movimento inválido.");
            return false;
        }
    }

    // Método chamado a cada quadro (frame) do jogo
    void Update()
    {
        // Exemplo de uso: Movimento inválido
        MoverPeao(new Vector2(4, 1), new Vector2(4, 3));

        // Exemplo de uso: Movimento válido
        MoverPeao(new Vector2(4, 1), new Vector2(4, 2));

        // Exibe o estado atual do tabuleiro
        ImprimirTabuleiro();
    }
}
