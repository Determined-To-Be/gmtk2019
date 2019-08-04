using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    MeshFilter filter;
    MeshRenderer render;
    Texture2D tex;

    [Range(3, 900)]
    public int numRays = 20;
    public float distance = 3f;
    public float maskCutawayDst = 1;
    public Material mat;

    void Start()
    {
        filter = gameObject.AddComponent<MeshFilter>();
        render = gameObject.AddComponent<MeshRenderer>();
        render.material = mat;
        filter.mesh = generateMesh(doCircleRays());
    }

    void LateUpdate()
    {
        filter.mesh = generateMesh(doCircleRays());
    }

    Vector3[] doCircleRays()
    {
        Stack<Vector3> points = new Stack<Vector3>();

        for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / numRays)
        {
            //i == theta or angle
            float x = Mathf.Cos(i), y = Mathf.Sin(i);
            Vector2 dir = new Vector2(x, y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, ~LayerMask.GetMask("Player", "Enviroment", "Item"));
            if (hit == false)
            {
                hit.point = (Vector2)transform.position + dir * distance;
            }
            else
            {
                if (hit.collider.transform.tag == "LightBox")
                {
                    hit.transform.gameObject.GetComponent<LightBox>().state = true;
                }
                else if (hit.collider.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyController>().isLit = true;
                }
            }
            //Debug.DrawLine(this.transform.position, hit.point);
            points.Push(hit.point - (Vector2)transform.position);
        }

        points.Push(Vector3.zero);
        return points.ToArray();
    }

    Mesh generateMesh(Vector3[] points)
    {
        Mesh mesh = new Mesh();
        Vector2[] uv = new Vector2[points.Length];
        int[] triangles = new int[(points.Length) * 3];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = points[i] * maskCutawayDst;
            if (i < points.Length - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        triangles[(points.Length - 2) * 3] = 0;
        triangles[(points.Length - 2) * 3 + 1] = points.Length - 2;
        triangles[(points.Length - 2) * 3 + 2] = 1;

        mesh.vertices = points;
        //mesh.uv = uv;
        mesh.triangles = triangles;

        //mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }
}
