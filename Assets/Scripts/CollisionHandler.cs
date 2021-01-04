using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject Explosion;
    public Transform SpawningRoot;

    void OnTriggerEnter()
    {
        GameObject Deathf = Instantiate(Explosion, transform.position, Quaternion.identity);
        Deathf.transform.parent = SpawningRoot;

        SendMessage("Collided");
    }
}
