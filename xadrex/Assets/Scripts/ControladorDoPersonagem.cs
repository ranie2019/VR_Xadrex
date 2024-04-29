using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControladorDoPersonagem : MonoBehaviour
{
    public Transform jogadorLocal;
    public float velocidadeDeSeguimento = 5f;
    public float alturaDesejada = 2f; // Altura desejada em rela��o ao ch�o

    private CharacterController characterController;

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
            return; // Sai do Update para evitar execu��o da l�gica abaixo sem um jogador
        }

        // L�gica para seguir o jogador local
        SeguirJogador();
    }

    void SeguirJogador()
    {
        // Dire��o para o jogador local
        Vector3 direcao = jogadorLocal.position - transform.position;
        direcao.y = 0; // Mant�m o personagem na mesma altura
        direcao.Normalize();

        // Movimento usando o CharacterController
        characterController.Move(direcao * velocidadeDeSeguimento * Time.deltaTime);

        // Configura manualmente a altura do jogador no eixo Y
        Vector3 posicaoAtual = transform.position;
        posicaoAtual.y = alturaDesejada;
        transform.position = posicaoAtual;
    }
}
