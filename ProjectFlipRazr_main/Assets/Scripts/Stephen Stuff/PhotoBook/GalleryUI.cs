using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GalleryUI : MonoBehaviour
{
    public Canvas galleryOne;
    public Canvas galleryTwo;

    public List<Texture2D> image;
    public int maxImageSlots = 18;
    public int pageSize = 9;


    // Start is called before the first frame update
    void Start()
    {
        galleryTwo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            FlipPage();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && galleryTwo.enabled == true)
        {
            FlipBack();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            AddImage();
        }
        //List<Image> list = new List<Image>();

        //if (list.Count <= 9)

    }

    public void FlipPage()
    {
        galleryOne.enabled = false;
        galleryTwo.enabled = true;
    }

    public void FlipBack()
    {
        galleryTwo.enabled = false;
        galleryOne.enabled = true;
    }

    public void AddImage()
    {
        Texture2D tex = Resources.Load<Texture2D>("Snapshots");
        image.Add(tex);
    }
}
