using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Properties;

public class TextDisplayScript : MonoBehaviour
{
    //public string textValue;

    //A reference to every comment box
    public Text comment1;
    public Text comment2;
    public Text comment3;

    //Specifically the generic comment list
    public List<string> genericComments = new List<string>();
    //public GameObject comment;
    //public GameObject commentBox;

    
    public void Awake()//Generates the generic comment list before the first frame
    {
        genericComments.Add("I'm Testing");
        genericComments.Add("Please Work");
        genericComments.Add("Your Mom");
        genericComments.Add("Eggs");
        genericComments.Add("George");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayMessage()
    {
        List<int> usedIndexes = new List<int>();//Keeps track of what comments were used from the comment list

        for (int i = 0; i < 3; i++)
        {
            int randomNumber; 
            do
            {
                randomNumber = Random.Range(0, genericComments.Count);//Picks a random index number from 0 to whatever amount of generic comments there is
            } while (usedIndexes.Contains(randomNumber));

            usedIndexes.Add(randomNumber);//Adds the random number just selected to the used list

            switch (i)//This is to switch the index if it has been used and pick a new one from the generic comment list
            {
                case 0:
                    comment1.text = genericComments[randomNumber]; 
                    break;
                case 1:
                    comment2.text = genericComments[randomNumber];
                    break;
                case 2:
                    comment3.text = genericComments[randomNumber];
                    break;
                default:
                    break;
            }
        }
        
        //GameObject newComment = Instantiate<GameObject>(comment);
        //newComment.transform.parent = commentBox.transform;
        //float yPos = (-50f + (commentNumber * -100));
        // newComment.transform.position = new Vector3(400f, yPos, 0f);
        //Text commentText = newComment.AddComponent<Text>();
        //comment1.text = genericComments[randomNumber];
        //comment2.text = genericComments[randomNumber];
        //comment3.text = genericComments[randomNumber];
    }

    //const int maxComments = 4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))//Displays the messages when pressing space
        {
            DisplayMessage();
        }
        
        
        
        
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = commentBox.transform.childCount; i >= 0; i--)
            {
                Destroy(commentBox.transform.GetChild(i).gameObject);
            }
            int commentsNum = Random.Range(0, maxComments);
            for (int i = 0; i < commentsNum; i++)
            {
                DisplayMessage(i);
            }
        }*/
    }
}
