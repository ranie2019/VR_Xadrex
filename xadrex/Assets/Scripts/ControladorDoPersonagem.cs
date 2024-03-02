using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControladorDoPersonagem : MonoBehaviour
{
    public Transform jogadorLocal;
    public float velocidadeDeSeguimento = 5f; // Adicione uma variável para a velocidade

    private CharacterController characterController;
    private Vector3 velocidade;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Atribui o jogador local automaticamente (por exemplo, encontrando por tag)
        if (jogadorLocal == null)
        {
            jogadorLocal = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        // Verifica se o jogador local ainda existe
        if (jogadorLocal == null)
        {
            jogadorLocal = GameObject.FindGameObjectWithTag("Player")?.transform;
            return; // Sai do Update para evitar execução da lógica abaixo sem um jogador
        }

        // Lógica para seguir o jogador local
        SeguirJogador();
    }

    void SeguirJogador()
    {
        // Direção para o jogador local
        Vector3 direcao = jogadorLocal.position - transform.position;
        direcao.y = 0; // Mantém o personagem na mesma altura

        // Normaliza a direção para evitar movimentos mais rápidos na diagonal
        direcao.Normalize();

        // Movimento usando o CharacterController
        characterController.Move(direcao * velocidadeDeSeguimento * Time.deltaTime);
    }
}
