using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}