using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject linePrefab;
    Transform line;

    [System.Serializable]
    public class Billboards{
        public SpriteRenderer SpriteComponent;
        [Header("Sprites")]
        public Sprite Front;
        public Sprite Back;
        public Sprite Right;
        public Sprite Left;

        [Header("Sprites")]
        public Sprite Walk_Left_01; 
        public Sprite Walk_Left_02; 
        public Sprite Walk_Right_01; 
        public Sprite Walk_Right_02; 
    }

    public Billboards PlayerSprites;

    public string controller;
    public Color color;

    Rigidbody rigidbody;

    Vector3 velocity;
    public Hat heldHat;

    public float speed = 5f;

    public float maxVelocityMagnitude = 30f;
    public List<Order> PlayerOrders;

    GameObject objToIntWith;

    Quaternion rotation;

    public int Points { get; private set; }

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Walking());
	}

    public void Setup(Color col)
    {
        color = col;
        GetComponent<Renderer>().material.color = col;
    }
	
	// Update is called once per frame
	void Update () {


        velocity = new Vector3(hInput.GetAxis(controller + "Horizontal"), 0 , -hInput.GetAxis(controller + "Vertical"));
        velocity *= speed;

        if(rigidbody.velocity.magnitude < maxVelocityMagnitude)
        {
           
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }

        float clampedY = Mathf.Clamp(transform.position.y, 2f, 8f);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z); //magic numbers ftw!!

        if (hInput.GetButtonDown(controller + "Use"))
        {
            TryUse();
        }

        TestInteraction();
        DrawLine();

        float x = velocity.x;
        float z = velocity.z;
        print(velocity);

        if (x > 0f && z >= 0)
        {
            // TOP RIGHT
            if(x > z){
                isSideWalking = true;
                isWalkingLeft = false;
            }
            else
            {
                isSideWalking = false;
                isWalkingUp = true;

            }
        }else if(x >= 0f && z < 0)
        {
            
            // BOTTOM RIGHT
            if (x > Mathf.Abs(z))
            {
                isSideWalking = true;
                isWalkingLeft = false;
            }
            else
            {
                isSideWalking = false;
                isWalkingUp = false;
               
            }
        }
        else if (x < 0f && z <= 0)
        {
            // BOTTOM LEFT
            if (Mathf.Abs(x) > Mathf.Abs(z))
            {
                isSideWalking = true;
                isWalkingLeft = true;
            }
            else
            {
                isSideWalking = false;
                isWalkingUp = false;
            }
        }else{
            // TOP LEFT
            if (x > Mathf.Abs(z))
            {
                isSideWalking = true;
                isWalkingLeft = true;
            }
            else
            {
                isSideWalking = false;
                isWalkingUp = true;
               
            }
        }
    }

    bool isSideWalking = false;
    bool isWalkingLeft = false;
    bool isWalkingUp = false;
    IEnumerator WalkingLeft; 
    IEnumerator WalkingRight;
    float Speed = 0.1f;

    IEnumerator WalkingLeftRoutine(){
        while(true){
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Left;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Walk_Left_01;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Left;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Walk_Left_02;
            yield return new WaitForSeconds(Speed);
        }
    }

    IEnumerator WalkingRightRoutine(){
        while(true){
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Right;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Walk_Right_01;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Right;
            yield return new WaitForSeconds(Speed);
            PlayerSprites.SpriteComponent.sprite = PlayerSprites.Walk_Right_02;
            yield return new WaitForSeconds(Speed);
        }
    }

    IEnumerator Walking ()
    {


        while(true)
        {
            if(isSideWalking)
            {
                if(isWalkingLeft)
                {
                    if(WalkingLeft == null){
                        WalkingLeft = WalkingLeftRoutine();
                        StartCoroutine(WalkingLeft);
                    }
                    if(WalkingRight != null){
                        StopCoroutine(WalkingRight);
                        WalkingRight = null;
                    }
                }
                else
                {
                    if (WalkingRight == null)
                    {
                        WalkingRight = WalkingRightRoutine();
                        StartCoroutine(WalkingRight);
                    }
                    if (WalkingLeft != null)
                    {
                        StopCoroutine(WalkingLeft);
                        WalkingLeft = null;
                    }
                }
                yield return new WaitForEndOfFrame();
            }else{
                if (WalkingLeft != null)
                {
                    StopCoroutine(WalkingLeft);
                    WalkingLeft = null;
                }
                if (WalkingRight != null)
                {
                    StopCoroutine(WalkingRight);
                    WalkingRight = null;
                }

                if(isWalkingUp){
                    PlayerSprites.SpriteComponent.sprite = PlayerSprites.Back;
                }else{
                    PlayerSprites.SpriteComponent.sprite = PlayerSprites.Front;
                }

            }
            yield return new WaitForEndOfFrame();
        }  
    }

    void DrawLine()
    {
        if (objToIntWith == null)
        {
            if(line != null)
            {
                Destroy(line.gameObject);
            }
            return;
        }

        if(line == null)
        {
            line = (Instantiate(linePrefab, transform).transform);
            line.localScale = Vector3.one;
            line.position = transform.position;
        }

        // position
        float dist = Vector3.Distance(objToIntWith.transform.position, transform.position) / 2;
        Vector3 middleWay = (objToIntWith.transform.position - transform.position).normalized * dist;
        line.transform.position = transform.position + middleWay;
        // rotate
        line.LookAt(objToIntWith.transform.position);
        line.localScale = new Vector3(0.2f,0.2f, dist);

        //Debug.DrawRay(transform.position, objToIntWith.transform.position-transform.position);
        
    }

    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
    }


    void TryUse()
    {
        if (heldHat != null)
        {
            DropHat();
            return;
        }
        else
        {
            Interact();
        }

    }

    public void DropHat()
    {
        
        heldHat.RemoveHatFromPlayer();
        heldHat = null;
    }



    void TestInteraction()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 8, Vector3.forward, 0.1f);

        List<GameObject> objects = new List<GameObject>();

        objToIntWith = null;
        
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.gameObject != gameObject || !hits[i].transform.IsChildOf(transform))
            {
                objects.Add(hits[i].transform.gameObject);
            }
        }

        //print(objects.Count);


        for (int i = 0; i < objects.Count; i++)
        {
            //print(objects[i].name);
            if (objects[i].CompareTag("Ground"))
            {
                continue;
            }
            else if (objects[i].CompareTag("Player") && (hits[i].transform.gameObject != gameObject || !hits[i].transform.IsChildOf(transform)))
            {
                objToIntWith = objects[i];
            }
            else if (objects[i].CompareTag("Hat"))
            {
                if(heldHat != null)
                {
                    if (objects[i] != heldHat.gameObject)
                    {
                        objToIntWith = objects[i];
                    }
                }
                else
                {
                    objToIntWith = objects[i];
                }
            }
            else if (objects[i].CompareTag("Station"))
            {
                objToIntWith = objects[i];
            }
        }
    }


    void Interact()
    {
        if(objToIntWith == null)
        {
            return;
        }

        objToIntWith.SendMessage("OnInteract", this, SendMessageOptions.DontRequireReceiver);
        if (objToIntWith.CompareTag("Hat"))
        {
            //is a hat. bind.
            heldHat = objToIntWith.GetComponent<Hat>();
        }

    }


}
