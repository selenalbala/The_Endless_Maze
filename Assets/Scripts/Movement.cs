using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    private Vector3 movePlyr;
    public Animator animator;
    public float speed;

    void FixedUpdate(){
        bool mvHoz = false;
        bool mvVer = false;
        float hor = 0, ver = 0;

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
    }
}
