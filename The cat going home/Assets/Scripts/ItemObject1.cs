using UnityEngine;

public class ItemObject1 : MonoBehaviour
{
    [SerializeField] ItemSO data;

    public int GetPoint()
    {
        return data.point;
    }
}
