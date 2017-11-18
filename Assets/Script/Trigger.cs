using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    Vector3 firstPosition,lastPosition;
    public GameObject boat, RPaddle, LPaddle;
    bool start ;
    float path,LastPath=0;

    Vector3 currentposition;

    private void Update()
    {
        currentposition = boat.transform.position;
    }
    private void OnTriggerEnter(Collider other){
        if (other.name == "Right" || other.name == "left"){
            if(RPaddle.transform.position.y> LPaddle.transform.position.y )
                firstPosition = LPaddle.transform.position;
            else if (RPaddle.transform.position.y < LPaddle.transform.position.y)
                firstPosition = RPaddle.transform.position;
        }

        
    }

    private void OnTriggerStay(Collider other) {
        start = true;
        StartCoroutine(move(other.name));
    }

    private void OnTriggerExit(Collider other){
        start = false;

        //離開碰撞體後，再做小小的前進或後退
        boat.transform.position = new Vector3(
            Mathf.Lerp(currentposition.x, currentposition.x, 0.005f),
            Mathf.Lerp(currentposition.y, currentposition.y, 0.005f),
            Mathf.Lerp(currentposition.z, currentposition.z + 0.1f*path, 0.005f));
    }

    IEnumerator move(string name)
    {
        yield return new WaitForSeconds(0.5f);
        if (true == start)
        {
            if (RPaddle.transform.position.y > LPaddle.transform.position.y)
                lastPosition = LPaddle.transform.position;
            else if (RPaddle.transform.position.y < LPaddle.transform.position.y)
                lastPosition = RPaddle.transform.position;

            path = (firstPosition.z * 100f)-(lastPosition.z * 100f);

            //划右邊前進或左邊後退 往左偏
            if ((name == "Right" && path > 0) || (name == "left" &&  path < 0))
            {
                boat.transform.Rotate(new Vector3(0f, -0.008f, 0f));
            }
            //划左邊前進或右邊後退 往右偏
            else if ((name == "left" && path > 0) || (name == "Right" && path < 0))
            {
                boat.transform.Rotate(new Vector3(0f, 0.008f, 0f));
            }

            
           

            print(path - LastPath);
            //path決定前進或後退
            if (path - LastPath != 0)
            {
                LastPath = path;

                if (path > 0)
                    path = 1;
                else
                    path =-1;
                boat.transform.position = new Vector3(
                    Mathf.Lerp(currentposition.x, currentposition.x, 0.003f),
                    Mathf.Lerp(currentposition.y, currentposition.y, 0.003f),
                    Mathf.Lerp(currentposition.z, currentposition.z + 1f * path, 0.003f));
            }

            
        }
    }
}
