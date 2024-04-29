using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    // Declare os Transform públicos para cada peça
    public Transform PeaoPreto1;
    public Transform PeaoPreto2;
    public Transform PeaoPreto3;
    public Transform PeaoPreto4;
    public Transform PeaoPreto5;
    public Transform PeaoPreto6;
    public Transform PeaoPreto7;
    public Transform PeaoPreto8;

    public Transform BispoNegro;
    public Transform BispoNegro2;
    public Transform ReiNegro;
    public Transform CavaleiroNegro;
    public Transform CavaleiroNegro2;
    public Transform RainhaNegra;
    public Transform TorreNegra;
    public Transform TorreNegra2;

    public Transform PeaoBranco1;
    public Transform PeaoBranco2;
    public Transform PeaoBranco3;
    public Transform PeaoBranco4;
    public Transform PeaoBranco5;
    public Transform PeaoBranco6;
    public Transform PeaoBranco7;
    public Transform PeaoBranco8;

    public Transform BispoBranco;
    public Transform BispoBranco2;
    public Transform ReiBranco;
    public Transform CavaleiroBranco;
    public Transform CavaleiroBranco2;
    public Transform RainhaBranco;
    public Transform TorreBranco;
    public Transform TorreBranco2;

    // Estrutura para armazenar dados de cada peça
    [System.Serializable]
    public struct ChessPieceData
    {
        public Transform transform;
        public Vector3 originalPosition;
        public Quaternion originalRotation;
    }

    // Lista para armazenar todas as peças
    private System.Collections.Generic.List<ChessPieceData> chessPieces = new System.Collections.Generic.List<ChessPieceData>();

    void Start()
    {
        // Adicione todas as peças desejadas à lista no início
        AddObjectsToList();
    }

    // Adicione as peças à lista e armazene suas posições e rotações originais
    void AddObjectsToList()
    {
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto1, originalPosition = PeaoPreto1.position, originalRotation = PeaoPreto1.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto2, originalPosition = PeaoPreto2.position, originalRotation = PeaoPreto2.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto3, originalPosition = PeaoPreto3.position, originalRotation = PeaoPreto3.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto4, originalPosition = PeaoPreto4.position, originalRotation = PeaoPreto4.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto5, originalPosition = PeaoPreto5.position, originalRotation = PeaoPreto5.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto6, originalPosition = PeaoPreto6.position, originalRotation = PeaoPreto6.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto7, originalPosition = PeaoPreto7.position, originalRotation = PeaoPreto7.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoPreto8, originalPosition = PeaoPreto8.position, originalRotation = PeaoPreto8.rotation });
        chessPieces.Add(new ChessPieceData { transform = BispoNegro, originalPosition = BispoNegro.position, originalRotation = BispoNegro.rotation });
        chessPieces.Add(new ChessPieceData { transform = BispoNegro2, originalPosition = BispoNegro2.position, originalRotation = BispoNegro2.rotation });
        chessPieces.Add(new ChessPieceData { transform = ReiNegro, originalPosition = ReiNegro.position, originalRotation = ReiNegro.rotation });
        chessPieces.Add(new ChessPieceData { transform = CavaleiroNegro, originalPosition = CavaleiroNegro.position, originalRotation = CavaleiroNegro.rotation });
        chessPieces.Add(new ChessPieceData { transform = CavaleiroNegro2, originalPosition = CavaleiroNegro2.position, originalRotation = CavaleiroNegro2.rotation });
        chessPieces.Add(new ChessPieceData { transform = RainhaNegra, originalPosition = RainhaNegra.position, originalRotation = RainhaNegra.rotation });
        chessPieces.Add(new ChessPieceData { transform = TorreNegra, originalPosition = TorreNegra.position, originalRotation = TorreNegra.rotation });
        chessPieces.Add(new ChessPieceData { transform = TorreNegra2, originalPosition = TorreNegra2.position, originalRotation = TorreNegra2.rotation });

        chessPieces.Add(new ChessPieceData { transform = PeaoBranco1, originalPosition = PeaoBranco1.position, originalRotation = PeaoBranco1.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco2, originalPosition = PeaoBranco2.position, originalRotation = PeaoBranco2.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco3, originalPosition = PeaoBranco3.position, originalRotation = PeaoBranco3.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco4, originalPosition = PeaoBranco4.position, originalRotation = PeaoBranco4.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco5, originalPosition = PeaoBranco5.position, originalRotation = PeaoBranco5.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco6, originalPosition = PeaoBranco6.position, originalRotation = PeaoBranco6.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco7, originalPosition = PeaoBranco7.position, originalRotation = PeaoBranco7.rotation });
        chessPieces.Add(new ChessPieceData { transform = PeaoBranco8, originalPosition = PeaoBranco8.position, originalRotation = PeaoBranco8.rotation });
        chessPieces.Add(new ChessPieceData { transform = BispoBranco, originalPosition = BispoBranco.position, originalRotation = BispoBranco.rotation });
        chessPieces.Add(new ChessPieceData { transform = BispoBranco2, originalPosition = BispoBranco2.position, originalRotation = BispoBranco2.rotation });
        chessPieces.Add(new ChessPieceData { transform = ReiBranco, originalPosition = ReiBranco.position, originalRotation = ReiBranco.rotation });
        chessPieces.Add(new ChessPieceData { transform = CavaleiroBranco, originalPosition = CavaleiroBranco.position, originalRotation = CavaleiroBranco.rotation });
        chessPieces.Add(new ChessPieceData { transform = CavaleiroBranco2, originalPosition = CavaleiroBranco2.position, originalRotation = CavaleiroBranco2.rotation });
        chessPieces.Add(new ChessPieceData { transform = RainhaBranco, originalPosition = RainhaBranco.position, originalRotation = RainhaBranco.rotation });
        chessPieces.Add(new ChessPieceData { transform = TorreBranco, originalPosition = TorreBranco.position, originalRotation = TorreBranco.rotation });
        chessPieces.Add(new ChessPieceData { transform = TorreBranco2, originalPosition = TorreBranco2.position, originalRotation = TorreBranco2.rotation });
    }

    // Resetar todas as peças para suas posições e rotações originais
    public void ResetAllObjects()
    {
        foreach (var chessPiece in chessPieces)
        {
            chessPiece.transform.position = chessPiece.originalPosition;
            chessPiece.transform.rotation = chessPiece.originalRotation;
        }
    }
}
