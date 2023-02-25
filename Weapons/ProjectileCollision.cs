using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Structure") || collision.collider.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }
    }
}
