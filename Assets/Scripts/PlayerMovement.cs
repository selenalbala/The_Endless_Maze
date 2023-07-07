using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private BoxCollider2D bodyBox;
    private RaycastHit2D hit;
    private Vector3 movePlyr;

    public Animator animator;
    public float speed;

    private void Start(){
        gameManager = GameManager.instance;
        bodyBox = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate(){
        bool mvHoz = false;
        bool mvVer = false;
        float hor = 0, ver = 0;

        if (gameManager.otherLives <= 0)
        {
            return;
        }

        if(Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0){
            if(mvHoz){
                    ver = Input.GetAxisRaw("Vertical");
            }else if(mvVer){
                    hor = Input.GetAxisRaw("Horizontal");
            }
        }else{
            mvHoz = Input.GetAxisRaw("Horizontal") != 0;
            hor = Input.GetAxisRaw("Horizontal");
            mvVer = Input.GetAxisRaw("Vertical") != 0;
            ver = Input.GetAxisRaw("Vertical");
        }

        movePlyr = new Vector3(hor * speed, ver * speed, 0);

        animator.SetFloat("Horizontal",hor);
        animator.SetFloat("Vertical",ver);

        hit = Physics2D.BoxCast(transform.position,bodyBox.size,0,new Vector2(0,movePlyr.y),Mathf.Abs(movePlyr.y),LayerMask.GetMask("Player","Blocking"));
        if(hit.collider == null){
            transform.Translate(0, movePlyr.y,0);
        }

        hit = Physics2D.BoxCast(transform.position,bodyBox.size,0,new Vector2(movePlyr.x,0),Mathf.Abs(movePlyr.x),LayerMask.GetMask("Player","Blocking"));
        if(hit.collider == null){
            transform.Translate(movePlyr.x,0,0);
        }
    }
}