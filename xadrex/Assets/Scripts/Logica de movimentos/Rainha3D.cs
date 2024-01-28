using UnityEngine;

/// <summary>
/// Classe que representa o comportamento da Rainha em um ambiente 3D.
/// </summary>
public class Rainha3D : MonoBehaviour
{
    /// <summary>
    /// Método que representa o movimento específico da Rainha em 3D.
    /// </summary>
    public void Mover()
    {
        // Exibe uma mensagem indicando o movimento diagonal, vertical ou horizontal da rainha
        Debug.Log("Rainha move-se diagonal, vertical ou horizontalmente em 3D.");
        // Lógica específica de RV aqui
    }
}

/// <summary>
/// Classe principal do programa para a Rainha.
/// </summary>
public class ProgramRainha : MonoBehaviour
{
    /// <summary>
    /// Método chamado no início do jogo.
    /// </summary>
    void Start()
    {
        // Instancia um objeto da classe Rainha3D
        Rainha3D rainha = new Rainha3D();
        // Chama o método Mover() para realizar o movimento da rainha
        rainha.Mover();
    }
}
