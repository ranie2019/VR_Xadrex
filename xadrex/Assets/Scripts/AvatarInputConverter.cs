using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    // Transformações do Avatar
    public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;
    public Transform avatarHandLeft;
    public Transform avatarHandRight;

    // Transformações do XRRig
    public Transform xrHead;
    public Transform xrHandLeft;
    public Transform xrHandRight;

    public Vector3 headPositionOffset;
    public Vector3 handRotationOffset;

    // Método chamado a cada quadro
    void Update()
    {
        // Sincronização da cabeça e do corpo
        SyncHeadAndBody();

        // Sincronização das mãos
        SyncHands(avatarHandRight, xrHandRight);
        SyncHands(avatarHandLeft, xrHandLeft);
    }

    // Sincroniza a posição e rotação da cabeça e do corpo
    void SyncHeadAndBody()
    {
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, xrHead.position + headPositionOffset, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, xrHead.rotation, 0.5f);

        // A rotação do corpo é sincronizada apenas no eixo Y
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);
    }

    // Sincroniza a posição e rotação das mãos
    void SyncHands(Transform avatarHand, Transform xrHand)
    {
        avatarHand.position = Vector3.Lerp(avatarHand.position, xrHand.position, 0.5f);
        avatarHand.rotation = Quaternion.Lerp(avatarHand.rotation, xrHand.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);
    }
}
