using UnityEngine;

/// <summary>
/// Classe que representa o comportamento do Rei em um ambiente 3D.
/// </summary>
public class Rei3D : MonoBehaviour
{
    /// <summary>
    /// Método que representa o movimento específico do Rei em 3D.
    /// </summary>
    public void Mover()
    {
        // Exibe uma mensagem indicando o movimento do rei em qualquer direção, mas apenas uma casa por vez
        Debug.Log("Rei move-se para qualquer direção, mas apenas uma casa por vez em 3D.");
        // Lógica específica de RV aqui
    }
}

/// <summary>
/// Classe principal do programa para o Rei.
/// </summary>
public class ProgramRei : MonoBehaviour
{
    /// <summary>
    /// Método chamado no início do jogo.
    /// </summary>
    void Start()
    {
        // Instancia um objeto da classe Rei3D
        Rei3D rei = new Rei3D();
        // Chama o método Mover() para realizar o movimento do rei
        rei.Mover();
    }
}
