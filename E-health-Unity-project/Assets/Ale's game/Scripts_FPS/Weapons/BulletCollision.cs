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
        string matColor;
        string matShape;

        if (Physics.Raycast(new Ray(mPrevPos, (transform.position - mPrevPos).normalized), out hit,
                (transform.position - mPrevPos).magnitude))
        {
            if (SceneManager.GetActiveScene().name == "SameColor" || SceneManager.GetActiveScene().name == "SameColorDifferentShape")
            {
                matColor = hit.transform.GetComponent<Renderer>().material.name;
                bulletColor = gameObject.GetComponent<Renderer>().material.name;
                matShape = hit.transform.tag;
                if (matColor == bulletColor)
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(1);
                    bool isDead = enemyStats.IsDead();
                    PlayerHUDfps.istance.UpdateScoreAmount();
                    if (isDead == true) Destroy(hit.transform.gameObject);
                }
                else if (matColor != bulletColor && (matShape == "Cone" || matShape == "Cube" || matShape == "Hexagon" || matShape == "Sphere"))
                    PlayerHUDfps.istance.LessdateScoreAmount();
                Destroy(gameObject);
            }
            else
            {
                matShape = hit.transform.tag;
                if (matShape == bulletShape)
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(1);
                    PlayerHUDfps.istance.UpdateScoreAmount();
                    bool isDead = enemyStats.IsDead();
                    if (isDead == true) Destroy(hit.transform.gameObject);
                }
                else if (matShape != bulletShape && (matShape == "Cone" || matShape == "Cube" || matShape == "Hexagon" || matShape == "Sphere"))
                    PlayerHUDfps.istance.LessdateScoreAmount();
                Destroy(gameObject);
            }
        }

        mPrevPos = transform.position; // updating the bullet position
    }
}
