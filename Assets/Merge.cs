using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    public GameObject MergedObject;
    
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    bool CanMerge;
    // Start is called before the first frame update
    void Start()
    {
        ID = GetInstanceID();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void MoveTowards()
    {
        if (CanMerge)
        {
            transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            if (Vector2.Distance(Block1.position, Block2.position) < Distance)
            {
                
               
                //GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                Destroy(Block1.gameObject);
                Destroy(Block2.gameObject);
                //Destroy(gameObject);
                for(int i = 0; i < DataRecorder.destroyItems.Length; i++)
                {
                    Destroy(DataRecorder.destroyItems[i]);
                }
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MergeBlock"))
        {
            if(collision.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                Block1 = transform;
                Block2 = collision.transform;
                DataRecorder.collisionTime++;
                
                Debug.Log($"SENDING MESSAGE FROM {gameObject.name} With numofBlocks {DataRecorder.numOfBlocks}");
                Debug.Log($"SENDING MESSAGE FROM {gameObject.name} With collision time {DataRecorder.collisionTime}");
                DataRecorder.destroyItems[DataRecorder.numOfBlocks] = collision.gameObject;
                    
                DataRecorder.numOfBlocks++;
                    
                 
                
              
                if (DataRecorder.collisionTime == 6)
                {
                    Debug.Log("Running.。。。。");
                    DataRecorder.collisionTime = 0;
                    DataRecorder.numOfBlocks = 0;
                      
                    //Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<Rigidbody2D>());
                    CanMerge = true;  
                }
            }
            else
            {
                DataRecorder.collisionTime = 0;
            }
        }
    }
}
