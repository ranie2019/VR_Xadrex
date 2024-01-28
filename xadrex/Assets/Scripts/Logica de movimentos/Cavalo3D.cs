using UnityEngine;

/// <summary>
/// Classe que representa o comportamento do Cavalo em um ambiente 3D.
/// </summary>
public class Cavalo3D : MonoBehaviour
{
    /// <summary>
    /// Método que representa o movimento específico do Cavalo em 3D.
    /// </summary>
    public void Mover()
    {
        // Exibe uma mensagem indicando o movimento em L do cavalo
        Debug.Log("Cavalo move-se em L em 3D.");
        // Lógica específica de RV aqui
    }
}

/// <summary>
/// Classe principal do programa para o Cavalo.
/// </summary>
public class ProgramCavalo : MonoBehaviour
{
    /// <summary>
    /// Método chamado no início do jogo.
    /// </summary>
    void Start()
    {
        // Instancia um objeto da classe Cavalo3D
        Cavalo3D cavalo = new Cavalo3D();
        // Chama o método Mover() para realizar o movimento do cavalo
        cavalo.Mover();
    }
}
