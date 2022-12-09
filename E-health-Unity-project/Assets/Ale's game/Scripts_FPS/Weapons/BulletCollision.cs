using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletCollision : MonoBehaviour

{
    [SerializeField] private string bulletShape;
    // private void Awake()
    // {
    //     Destroy(gameObject, 3.0f);
    // }

    private Vector3 mPrevPos;

    private void Start()
    {
        mPrevPos = transform.position; //Initial bullet position
    }

    private void Update()
    {
        RaycastHit hit;

        string bulletColor;
        bulletColor = gameObject.GetComponent<Renderer>().material.name;

        if (Physics.Raycast(new Ray(mPrevPos, (transform.position - mPrevPos).normalized), out hit,
                (transform.position - mPrevPos).magnitude))
        {
            if (SceneManager.GetActiveScene().name == "SameColor" || SceneManager.GetActiveScene().name == "SameColorDifferentShape")
            {
                if (hit.transform.GetComponent<Renderer>().material.name == bulletColor)
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(1);
                    bool isDead = enemyStats.IsDead();
                    if (isDead == true) Destroy(hit.transform.gameObject);
                }
                Destroy(gameObject);
            }
            else
            {
                if (hit.transform.tag == bulletShape)
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(1);
                    bool isDead = enemyStats.IsDead();
                    if (isDead == true) Destroy(hit.transform.gameObject);
                }
                Destroy(gameObject);
            }
        }

        mPrevPos = transform.position; // updating the bullet position
    }
}
