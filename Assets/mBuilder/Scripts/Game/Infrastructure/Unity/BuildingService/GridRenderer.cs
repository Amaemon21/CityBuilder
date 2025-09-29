using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GridRenderer : MonoBehaviour
{
    private const string hiddenInternalColored = "Hidden/Internal-Colored";
    
    [SerializeField] private GridConfig gridConfig;
    [SerializeField] private Transform groundPlane;
    
    private Material lineMaterial;
    
    private void Awake()
    {
        lineMaterial = new Material(Shader.Find(hiddenInternalColored));
    }
    
    private void OnPostRender()
    {
        if (lineMaterial == null || groundPlane == null) 
            return;

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.white);

        float offset = 0.5f;

        // Вертикальные линии
        for (int x = 0; x <= gridConfig.GridSize.x; x++)
        {
            float xPos = x - offset;
            GL.Vertex(groundPlane.TransformPoint(new Vector3(xPos, 0, -offset)));
            GL.Vertex(groundPlane.TransformPoint(new Vector3(xPos, 0, gridConfig.GridSize.y - offset)));
        }

        // Горизонтальные линии
        for (int y = 0; y <= gridConfig.GridSize.y; y++)
        {
            float yPos = y - offset;
            GL.Vertex(groundPlane.TransformPoint(new Vector3(-offset, 0, yPos)));
            GL.Vertex(groundPlane.TransformPoint(new Vector3(gridConfig.GridSize.x - offset, 0, yPos)));
        }

        GL.End();
    }
}