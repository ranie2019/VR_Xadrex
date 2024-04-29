using UnityEngine;

/// <summary>
/// Classe que representa o comportamento do Bispo em um ambiente 3D.
/// </summary>
public class Bispo3D : MonoBehaviour
{
    /// <summary>
    /// Método que representa o movimento específico do Bispo em 3D.
    /// </summary>
    public void Mover()
    {
        // Exibe uma mensagem indicando o movimento diagonal do bispo
        Debug.Log("Bispo move-se diagonalmente em 3D (considerando profundidade).");
        // Lógica específica de RV aqui
    }
}

/// <summary>
/// Classe principal do programa para o Bispo.
/// </summary>
public class ProgramBispo : MonoBehaviour
{
    /// <summary>
    /// Método chamado no início do jogo.
    /// </summary>
    void Start()
    {
        // Instancia um objeto da classe Bispo3D
        Bispo3D bispo = new Bispo3D();
        // Chama o método Mover() para realizar o movimento do bispo
        bispo.Mover();
    }
}
