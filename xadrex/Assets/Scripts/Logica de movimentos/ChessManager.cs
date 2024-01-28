using UnityEngine;

public class ChessManager : MonoBehaviour
{
    public enum PlayerTurn
    {
        Player1,
        Player2
    }

    public PlayerTurn currentPlayerTurn = PlayerTurn.Player1;

    // Adicione referências às peças e outros elementos necessários
    public GameObject[] player1Pieces;
    public GameObject[] player2Pieces;

    private void Start()
    {
        // Inicie o jogo com o turno do Jogador 1
        SwitchTurn(PlayerTurn.Player1);
    }

    private void Update()
    {
        // Aqui você pode adicionar lógica para o input do jogador, movimentação de peças, etc.
        // Certifique-se de verificar o turno atual antes de permitir movimentos.
        if (Input.GetMouseButtonDown(0))
        {
            // Adicione lógica de seleção/movimento de peças aqui
            // Certifique-se de verificar o turno antes de permitir que o jogador mova suas peças.
            MovePiece();
        }
    }

    private void MovePiece()
    {
        // Adicione lógica para mover a peça
        // Aqui você pode verificar se a peça selecionada pertence ao jogador cujo turno é atual.
        // Certifique-se de desativar ou bloquear as peças do jogador adversário durante este turno.

        // Exemplo (certifique-se de substituir pelos seus próprios scripts/lógica):
        // Se for o turno do Jogador 1, mova as peças do Jogador 1 e bloqueie as do Jogador 2.
        if (currentPlayerTurn == PlayerTurn.Player1)
        {
            MovePlayerPieces(player1Pieces);
            BlockPlayerPieces(player2Pieces);
        }
        // Se for o turno do Jogador 2, mova as peças do Jogador 2 e bloqueie as do Jogador 1.
        else if (currentPlayerTurn == PlayerTurn.Player2)
        {
            MovePlayerPieces(player2Pieces);
            BlockPlayerPieces(player1Pieces);
        }

        // Após o movimento, troque o turno.
        SwitchTurn((currentPlayerTurn == PlayerTurn.Player1) ? PlayerTurn.Player2 : PlayerTurn.Player1);
    }

    private void MovePlayerPieces(GameObject[] pieces)
    {
        // Adicione lógica para mover as peças do jogador atual.
        foreach (var piece in pieces)
        {
            // Simplesmente mova a peça no eixo Y (altura) como exemplo.
            piece.transform.Translate(Vector3.up * 2.0f, Space.World);
        }
    }

    private void BlockPlayerPieces(GameObject[] pieces)
    {
        // Adicione lógica para bloquear as peças do jogador adversário.
        foreach (var piece in pieces)
        {
            // Adicione lógica específica para bloquear/desativar as peças (por exemplo, desativando os colliders).
            piece.GetComponent<Collider>().enabled = false;
        }
    }

    private void SwitchTurn(PlayerTurn newTurn)
    {
        // Troque o turno e faça outras atualizações necessárias.
        currentPlayerTurn = newTurn;

        // Exemplo: Atualize a interface do usuário, exiba mensagens de turno, etc.
        Debug.Log("É o turno do Jogador " + ((currentPlayerTurn == PlayerTurn.Player1) ? "1" : "2"));
    }
}
