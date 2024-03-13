using CustomAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Piece : MonoBehaviour
{
    public float Length = 100f;
    public float Width = 200f;
    public float Depth = 5f;

    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshRenderer meshRenderer;
    private Mesh mesh = null;

    [Button("UpdateMesh")]
    public bool doUpdateMesh = false;


    private void Reset()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();    
    }

    public void SetDimensions(int width, int length, int depth, bool updateMesh = true)
    {
        this.Width = width;
        this.Length = length;
        this.Depth = depth;
        if (updateMesh)
        {
            UpdateMesh();
        }
    }

    public void UpdateMesh() 
    {
        if (this.mesh != null)
        {
            Object.Destroy(this.mesh);
        }

        // generate container boxes
        List<Vector3> verticesList = new List<Vector3>();
        verticesList.AddRange(generateVertices(new Vector3(Width, Length, Depth), Vector3.zero));

        List<int> trianglesList = new List<int>();
        for (int i = 0; i < verticesList.Count; i++)
        {
            trianglesList.Add(i);
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = verticesList.ToArray();
        newMesh.triangles = trianglesList.ToArray();
        newMesh.RecalculateNormals();

        meshFilter.mesh = newMesh;

        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
    }

    private List<Vector3> generateVertices(Vector3 _edgeA, Vector3 _edgeB)
    {
        return new List<Vector3>()
        {
            // box top
            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeB.x, _edgeA.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeA.z),

            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeB.z),

            // box bottom
            new Vector3(_edgeB.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeB.x, _edgeB.y, _edgeA.z),

            new Vector3(_edgeB.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),

            // box back
            new Vector3(_edgeB.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeB.z),

            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeB.z),

            // box front
            new Vector3(_edgeB.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeB.x, _edgeA.y, _edgeA.z),

            new Vector3(_edgeB.x, _edgeA.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeA.z),

            // box left
            new Vector3(_edgeB.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeB.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),

            new Vector3(_edgeB.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeB.x, _edgeB.y, _edgeA.z),
            new Vector3(_edgeB.x, _edgeA.y, _edgeA.z),

            // box right
            new Vector3(_edgeA.x, _edgeB.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),

            new Vector3(_edgeA.x, _edgeA.y, _edgeB.z),
            new Vector3(_edgeA.x, _edgeA.y, _edgeA.z),
            new Vector3(_edgeA.x, _edgeB.y, _edgeA.z),
        };
    }
}
