using UnityEngine;

/// <summary>
/// Classe que representa o comportamento da Torre em um ambiente 3D.
/// </summary>
public class Torre3D : MonoBehaviour
{
    /// <summary>
    /// Método que representa o movimento específico da Torre em 3D.
    /// </summary>
    public void Mover()
    {
        // Exibe uma mensagem indicando o movimento vertical ou horizontal da torre
        Debug.Log("Torre move-se vertical ou horizontalmente em 3D.");
        // Lógica específica de RV aqui
    }
}

/// <summary>
/// Classe principal do programa para a Torre.
/// </summary>
public class ProgramTorre : MonoBehaviour
{
    /// <summary>
    /// Método chamado no início do jogo.
    /// </summary>
    void Start()
    {
        // Instancia um objeto da classe Torre3D
        Torre3D torre = new Torre3D();
        // Chama o método Mover() para realizar o movimento da torre
        torre.Mover();
    }
}
