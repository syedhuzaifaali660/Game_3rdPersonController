
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 5f);
    }


}
