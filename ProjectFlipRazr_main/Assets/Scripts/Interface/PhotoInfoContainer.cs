using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhotoInfoContainer : MonoBehaviour
{
    [Header("Photo MetaData")]
    public string photoName;
    public string photoTime;
    public string photoLocation;
    public string photoItems;
    public string photoTitle;

    private PhotoExpandOpener expandOpener;

    // Start is called before the first frame update
    void Awake()
    {
        expandOpener = FindAnyObjectByType(typeof(PhotoExpandOpener)).GetComponent<PhotoExpandOpener>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonSelected()
    {
        photoTitle = photoItems;
        if (string.IsNullOrEmpty(photoItems))
        {
            photoTitle = photoLocation;
        }

        expandOpener.selPhotoTime = photoTime;
        expandOpener.selPhotoName = photoName;
        expandOpener.selPhotoTitle = photoTitle;
        expandOpener.selPhotoLocation = photoLocation;
        expandOpener.selPhotoItems = photoItems;
    }

    public void OnClick()
    {
        expandOpener.OpenPhotoExpanded();
    }
}
