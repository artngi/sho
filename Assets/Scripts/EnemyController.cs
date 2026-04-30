using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject prefab;
    public Vector3Int initialPosition;
    public int difficulity = 0;
    Vector3 temporaryPlayerPosition;
    private float randomXpos;
    public bool isClone = false;

    void Start()
    {
        if (!isClone)
        {
            transform.position = initialPosition;
            InvokeRepeating(nameof(SetSpear), 3f, 1f);
        }
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    void SetSpear()
    {
        temporaryPlayerPosition.x = Mathf.Clamp(transform.position.x + UnityEngine.Random.Range(-1, 1), -2.75f, 2.75f);
        for (int i = 0; i < MathF.Sqrt(Mathf.Sqrt(difficulity)) + 2; i++)
        {
            while (true)
            {
                randomXpos = UnityEngine.Random.Range(-2.75f, 2.75f);
                if (!(Mathf.Abs(temporaryPlayerPosition.x - randomXpos) <= 0.5f))
                {
                    GenerateSpear(new Vector3(randomXpos, transform.position.y, transform.position.z));
                    break;
                }
            }
            

        }
        difficulity++;
    }
    void GenerateSpear(Vector3 spawnPosition)
    {
        GameObject spear = Instantiate(prefab, spawnPosition, Quaternion.Euler(0, 0, 180));
        spear.GetComponent<EnemyController>().isClone = true;
        spear.AddComponent<Rigidbody2D>().AddForce(new Vector2(0f, difficulity / 30));
        spear.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    }
}
