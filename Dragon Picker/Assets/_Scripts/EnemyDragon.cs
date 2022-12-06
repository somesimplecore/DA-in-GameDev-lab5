using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed = 1;
    public float timeBetweenEggDrop = 1f;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.1f;
    void Start()
    {
        Invoke("DropEgg", 2f);
    }
    
    void DropEgg()
    {
        Vector3 MyVector = new Vector3(0.0f, 5.0f, 0.0f);
        GameObject egg = Instantiate<GameObject>(dragonEggPrefab);
        egg.transform.position = transform.position + MyVector;
        Invoke("DropEgg", timeBetweenEggDrop);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < -leftRightDistance)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistance)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate()
    {
        if(Random.value < chanceDirection)
        {
            speed *= -1;
        }
    }
}
