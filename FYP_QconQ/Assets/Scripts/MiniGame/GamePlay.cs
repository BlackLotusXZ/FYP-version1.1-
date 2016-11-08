using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GamePlay : MonoBehaviour
{
    public float speed;
    private float timer;
    public int numofmouse;
    private float a;
    private float b;
    private float tempspeed;
    private int x = 0;
    private int random;
    public Text text;
    public GameObject mouse;
    public GameObject pic;
    private GameObject[] mouses;
    private Vector3[] pos;
    public Sprite[] types;
    private GameObject[] sequence;//bottom sequence

    public Animator animator;  //gets the animator component
    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        a = -3.45f;
        b = 3.45f;
        mouses = new GameObject[4];
        pos = new Vector3[4];
        System.Random rnd = new System.Random();
        sequence = new GameObject[5];

        for (int i = 1; i <= 4; i++)
        {
            random = rnd.Next(0, 5); // creates a number between 0 -5
            pos[i - 1] = new Vector3(a, (i - 2.2f) * -1.5f, 0);//store original position so can loop
            GameObject clone = (GameObject)Instantiate(mouse, new Vector3(a, (i - 2.2f) * -1.5f, 0), Quaternion.identity);
            clone.name = "mouse" + i;
            clone.transform.parent = GameObject.Find("Canvas").transform;
            clone.GetComponent<Image>().sprite = types[random];//image change
            if (a > 0)
            {
                clone.transform.localScale = new Vector3(3f, 2.3f, 2f);
            }
            else
               clone.transform.localScale = new Vector3(-3f, 2.3f, 2f);
        //    Button tempButton = clone.GetComponent<Button>();
         //   tempButton.onClick.AddListener(() => Imagecheck());
            // clone.transform.localScale = new Vector3(i*3.5f, i*3.5f, 0);
            a = a + b;
            b = a - b;
            a = a - b;
            mouses[i - 1] = clone;
        }
        for (int i = 0; i < sequence.Length; i++)
        {
            random = rnd.Next(0, 5); // creates a number between 0 -5
            GameObject clone = (GameObject)Instantiate(pic, new Vector3((i*1.2f)-2.4f, -4.5f, -1), Quaternion.identity);
            clone.name = "sequence" + (i + 1);
            clone.transform.parent = GameObject.Find("Canvas").transform;
            clone.transform.localScale = new Vector3(11f, 11f, 1f);
            clone.GetComponent<SpriteRenderer>().sprite = types[random];//image change
            sequence[i] = clone;
        }
    }

    // Update is called once per frame
    void Update()
    {
      //  timer -= 1.0f * Time.deltaTime;
        text.text = timer.ToString("F0");
        System.Random rnd = new System.Random();
        for (int i = 0; i < 4; i++)
        {
            Vector3 position = mouses[i].transform.localPosition;
            position.z = -1;
            mouses[i].transform.localPosition = position;//make sure z is always -1;
            switch (x)//swap speed
            {
                case 0:
                    tempspeed = speed;
                    break;
                case 1:
                    tempspeed = -speed;
                    break;
            }
           // if (timer > 40.0f)
            //{
                mouses[i].transform.Translate((tempspeed ) * Time.deltaTime, 0, 0);//translate the image
            //}
            //else if (timer > 20.0f)
            //{
           //     mouses[i].transform.Translate((tempspeed - 1) * Time.deltaTime, 0, 0);//translate the imagee
            //}
            //else
           //     mouses[i].transform.Translate((tempspeed) * Time.deltaTime, 0, 0);//translate the imagee
            
            if (x == 0)
                x = 1;
            else
                x = 0;

            if (mouses[i].transform.localPosition.x < -255 || mouses[i].transform.localPosition.x > 255)//check if in  not in screen
            {
                random = rnd.Next(0, 5); // creates a number between 0 -5
                mouses[i].GetComponent<Image>().sprite = types[random];
                mouses[i].transform.position = pos[i];
                /*
                switch(random)
                {
                    case 1:
                        break;
                    case 2:
                        animator.SetTrigger("anims2");
                        break;
                }*/
            }
        }
    }
    public void Imagecheck(Image name)
    {
        System.Random rnd = new System.Random();
        if (name.sprite.name == sequence[0].GetComponent<SpriteRenderer>().sprite.name)
        {
            timer += 1;
            Debug.Log("same");
            sequence[0].GetComponent<SpriteRenderer>().sprite = sequence[1].GetComponent<SpriteRenderer>().sprite;
            sequence[1].GetComponent<SpriteRenderer>().sprite = sequence[2].GetComponent<SpriteRenderer>().sprite;
            sequence[2].GetComponent<SpriteRenderer>().sprite = sequence[3].GetComponent<SpriteRenderer>().sprite;
            sequence[3].GetComponent<SpriteRenderer>().sprite = sequence[4].GetComponent<SpriteRenderer>().sprite;
            random = rnd.Next(0, 5);
            sequence[4].GetComponent<SpriteRenderer>().sprite = types[random];
        }
    }
}
