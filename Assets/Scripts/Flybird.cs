using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flybird : MonoBehaviour
{
    private Rigidbody2D rb;
    public  Manager manager;
    public FaceDetector Face;
    float norm;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        norm = Mathf.Clamp(Face.faceY / Screen.height, 0.0f, 1.0f);
        float targetY = Mathf.Lerp(-1.0f, 1.0f, norm);
        Vector2 movement = new Vector2(0.0f, -targetY) * 1.5f;
        rb.velocity = movement;
    }
    void OnCollisionEnter2D(Collision2D col){
        manager.Gameover();
        Debug.Log("collided");
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag=="Scorebox"){
            manager.score++;
            col.gameObject.SetActive(false);
        }
         if(col.gameObject.tag=="Bottom"){
            manager.Gameover();
            Destroy(this);
    }
    }
     
}
// Update is called once per frame
    /*void Update()
    {
        if (Face.faceY>250){
            norm=-0.5f;
        } if(Face.faceY<250){
            norm=0.5f;
        }
        
        rb.velocity=new Vector3(0,norm,0)*2.0f;
        //Debug.Log(norm);
    }*/