using System.Linq;
using UnityEngine;

public class CarDeformation : MonoBehaviour
{
    public float deformationForce = 1f; // The deformation force
    public float deformationRadius = 0.5f; // The radius within which deformation occurs

    [SerializeField]private MeshFilter deformedMeshFilter; // Reference to the deformed mesh filter
    private Mesh deformedMesh; // The deformed mesh

    private void Start()
    {
        // Create a copy of the original mesh as the initial deformed mesh
        deformedMesh = Instantiate(deformedMeshFilter.sharedMesh);
        // Assign the deformed mesh to the deformed mesh filter
        deformedMeshFilter.sharedMesh = deformedMesh;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calculate the deformation based on the collision

        foreach (ContactPoint contact in collision.contacts)
        {
            // Convert the contact point to local space
            Vector3 localContactPoint = transform.InverseTransformPoint(contact.point);

            // Get the vertex indices within the deformation radius
            int[] vertexIndices = deformedMesh.vertices
                .Select((v, index) => new { Vertex = v, Index = index })
                .Where(v => Vector3.Distance(localContactPoint, v.Vertex) <= deformationRadius)
                .Select(v => v.Index)
                .ToArray();

            // Apply deformation to the vertices within the deformation radius
            foreach (int vertexIndex in vertexIndices)
            {
                // Calculate the deformation force based on the distance from the contact point
                float distance = Vector3.Distance(localContactPoint, deformedMesh.vertices[vertexIndex]);
                float deformationFactor = 1f - (distance / deformationRadius);

                // Apply deformation to the vertex
                Vector3 deformation = collision.relativeVelocity * deformationForce * deformationFactor;
                deformedMesh.vertices[vertexIndex] += transform.InverseTransformVector(deformation);
            }
        }

        // Update the deformed mesh
        deformedMesh.RecalculateNormals();
        deformedMesh.RecalculateBounds();
        deformedMeshFilter.sharedMesh = deformedMesh;
    }
}
