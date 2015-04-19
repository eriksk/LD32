using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TmxMeshCreator
{
    public Mesh CreateMesh(int[] grid, int width, int height, int columnsInSheet, int rowsInSheet, bool cullEmptyCells = false, int cellToConsiderEmpty = 0)
    {
        return CreatePlane(grid, 1f, 1f, height, width, columnsInSheet, rowsInSheet, cullEmptyCells, cellToConsiderEmpty);
    }

    private Mesh CreatePlane(int[] data, float tileHeight, float tileWidth, int gridHeight, int gridWidth, int columnsInSheet, int rowsInSheet, bool cullEmptyCells, int cellToConsiderEmpty)
    {
        if (data.Length == 0)
            return null;

        var mesh = new Mesh();

        var tileSizeX = 1.0f / columnsInSheet;
        var tileSizeY = 1.0f / rowsInSheet;

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        var index = 0;
        for (var col = 0; col < gridWidth; col++)
        {
            for (var row = 0; row < gridHeight; row++)
            {
                int cell = data[col + row * gridWidth];
                if(cullEmptyCells && cell == cellToConsiderEmpty) continue;

                // We use grid height - row to flip it upside down
                AddVertices(tileHeight, tileWidth, gridHeight - row, col, vertices);
                index = AddTriangles(index, triangles);
                AddNormals(normals);

                int tileCol = cell % columnsInSheet;
                int tileRow = cell / rowsInSheet;
                AddUvs((rowsInSheet - tileRow) -1, tileSizeY, tileSizeX, uvs, tileCol);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    private static void AddVertices(float tileHeight, float tileWidth, int y, int x, ICollection<Vector3> vertices)
    {
        vertices.Add(new Vector3((x * tileWidth), (y * tileHeight), 0));
        vertices.Add(new Vector3((x * tileWidth) + tileWidth, (y * tileHeight), 0));
        vertices.Add(new Vector3((x * tileWidth) + tileWidth, (y * tileHeight) + tileHeight, 0));
        vertices.Add(new Vector3((x * tileWidth), (y * tileHeight) + tileHeight, 0));
    }

    private static int AddTriangles(int index, ICollection<int> triangles)
    {
        triangles.Add(index + 2);
        triangles.Add(index + 1);
        triangles.Add(index);
        triangles.Add(index);
        triangles.Add(index + 3);
        triangles.Add(index + 2);
        index += 4;
        return index;
    }

    private static void AddNormals(ICollection<Vector3> normals)
    {
        normals.Add(Vector3.forward);
        normals.Add(Vector3.forward);
        normals.Add(Vector3.forward);
        normals.Add(Vector3.forward);
    }

    private static void AddUvs(int tileRow, float tileSizeY, float tileSizeX, ICollection<Vector2> uvs, int tileColumn)
    {
        uvs.Add(new Vector2(tileColumn * tileSizeX, tileRow * tileSizeY));
        uvs.Add(new Vector2((tileColumn + 1) * tileSizeX, tileRow * tileSizeY));
        uvs.Add(new Vector2((tileColumn + 1) * tileSizeX, (tileRow + 1) * tileSizeY));
        uvs.Add(new Vector2(tileColumn * tileSizeX, (tileRow + 1) * tileSizeY));
    }
}