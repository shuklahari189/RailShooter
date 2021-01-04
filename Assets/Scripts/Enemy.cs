using UnityEngine;

public class Enemy : MonoBehaviour
{
    BoxCollider boxCollider;
    ScoreBoard scoreBoard;

    public int ScorePerHits = 10;
    public int Hits = 100;

    public GameObject DeathFx;
    public Transform SpawningRoot;

    private void Start()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        scoreBoard = FindObjectOfType<ScoreBoard>();

        boxCollider.isTrigger = false;
    }

    void OnParticleCollision()
    {
        scoreBoard.ScoreHit(ScorePerHits);

        Hits--;

        if (Hits == 0) 
        {
            GameObject deathParticles = Instantiate(DeathFx, transform.position, Quaternion.identity);
            deathParticles.transform.parent = SpawningRoot;
            Destroy(gameObject);
        }
    }
}
