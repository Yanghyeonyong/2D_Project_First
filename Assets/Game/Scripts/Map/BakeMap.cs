using Unity.AI.Navigation;
using UnityEngine;

public class BakeMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NavMeshSurface navMeshSurface;
    private void OnEnable()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.Log("네브메쉬 없음");
        }
    }
}
