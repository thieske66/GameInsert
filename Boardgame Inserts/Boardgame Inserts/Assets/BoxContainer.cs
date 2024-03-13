using CustomAttributes;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BoxContainer : MonoBehaviour
{
    public int Width = 100;
    public int Length = 100;
    public int Height = 50;

    public int MaterialThickness = 5;

    /// <summary>
    /// If true, the dimensions describe the "inner volume" available for content. 
    /// If false, it describes the bounding box of the entire container.
    /// </summary>
    public bool FrameContentOnly = true;

    public List<Piece> Pieces = new List<Piece>();

    [Button("generateMesh")]
    public bool doGenerateMesh;

    private void generateMesh()
    {
        if (Application.isPlaying)
        {
            Debug.Log("Cannot generate mesh in playmode");
            return;
        }

        // Destroy existing pieces
        foreach (Piece piece in Pieces)
        {
            GameObject.DestroyImmediate(piece.gameObject);
        }
        Pieces.Clear();

        // Generate pieces
        int bottomPieceWidth = FrameContentOnly ? Width + 2 * MaterialThickness : Width;
        int bottomPieceLength = FrameContentOnly ? Length + 2 * MaterialThickness : Length;
        int bottomPieceThickness = MaterialThickness;
        Piece bottomPiece = createPiece(bottomPieceWidth, bottomPieceLength, MaterialThickness, "Bottom Piece");
        bottomPiece.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.right) * bottomPiece.transform.rotation;
        if (FrameContentOnly)
        {
            bottomPiece.transform.localPosition += Vector3.down * MaterialThickness;
            bottomPiece.transform.localPosition += Vector3.forward * (2 * MaterialThickness);
            bottomPiece.transform.localPosition += Vector3.left * MaterialThickness;
        }
        else
        {
            bottomPiece.transform.localPosition += Vector3.forward * MaterialThickness;
        }


        int leftPieceLength = FrameContentOnly ? Length + 2 * MaterialThickness : Length;
        int leftPieceHeight = FrameContentOnly ? Height + MaterialThickness : Height;
        int leftPieceThickness = MaterialThickness;
        Piece leftPiece = createPiece(leftPieceLength, leftPieceHeight, MaterialThickness, "Left Piece");
        leftPiece.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.up) * leftPiece.transform.rotation;
        if (FrameContentOnly)
        {
            leftPiece.transform.localPosition += Vector3.back * Length;
            leftPiece.transform.localPosition += Vector3.down * MaterialThickness;
        }
        else
        {
            leftPiece.transform.localPosition += Vector3.back * (Length - MaterialThickness);
            leftPiece.transform.localPosition += Vector3.right * MaterialThickness;
        }


        int rightPieceLength = FrameContentOnly ? Length + 2 * MaterialThickness : Length;
        int rightPieceHeight = FrameContentOnly ? Height + MaterialThickness : Height;
        int rightPieceThickness = MaterialThickness;
        Piece rightPiece = createPiece(rightPieceLength, rightPieceHeight, MaterialThickness, "Right Piece");
        rightPiece.transform.rotation = Quaternion.AngleAxis(90f, Vector3.up) * rightPiece.transform.rotation;
        if (FrameContentOnly)
        {
            rightPiece.transform.localPosition += Vector3.right * Width;
            rightPiece.transform.localPosition += Vector3.down * MaterialThickness;
            rightPiece.transform.localPosition += Vector3.forward * (2 * MaterialThickness);
        }
        else
        {
            rightPiece.transform.localPosition += Vector3.right * (Width - MaterialThickness);
            rightPiece.transform.localPosition += Vector3.forward * MaterialThickness;
        }


        int frontPieceWidth = FrameContentOnly ? Width + 2 * MaterialThickness : Width;
        int frontPieceHeight = FrameContentOnly ? Height + MaterialThickness : Height;
        int frontPieceThickness = MaterialThickness;
        Piece frontPiece = createPiece(frontPieceWidth, frontPieceHeight, MaterialThickness, "Front Piece");
        if (FrameContentOnly)
        {
            frontPiece.transform.localPosition += Vector3.down * MaterialThickness;
            frontPiece.transform.localPosition += Vector3.forward * MaterialThickness;
            frontPiece.transform.localPosition += Vector3.left * MaterialThickness;
        }
        else
        {
            // Do nothing
        }

        int backPieceWidth = FrameContentOnly ? Width + 2 * MaterialThickness : Width;
        int backPieceHeight = FrameContentOnly ? Height + MaterialThickness : Height;
        int backPieceThickness = MaterialThickness;
        Piece backPiece = createPiece(backPieceWidth, backPieceHeight, MaterialThickness, "Back Piece");
        if (FrameContentOnly)
        {
            backPiece.transform.localPosition += Vector3.down * MaterialThickness;
            backPiece.transform.localPosition += Vector3.left * MaterialThickness;
            backPiece.transform.localPosition += Vector3.back * Length;
        }
        else
        {
            backPiece.transform.localPosition += Vector3.back * (Length - MaterialThickness);
        }

        Pieces.AddRange(new Piece[] { bottomPiece, leftPiece, rightPiece, frontPiece, backPiece });
    }

    private Piece createPiece(int width, int length, int materialThickness, string name)
    {
        GameObject pieceObject = new GameObject(name, typeof(Piece));
        pieceObject.transform.parent = this.transform;
        pieceObject.transform.localPosition = Vector3.zero;
        pieceObject.transform.localRotation = Quaternion.identity;
        Piece piece = pieceObject.GetComponent<Piece>();
        piece.SetDimensions(width, length, materialThickness);
        return piece;
    }
}
