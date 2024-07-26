using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public Gun pepeLePew;

    [SerializeField] private RectTransform ammoBar;
    public float ammoCurrent;

    public Sprite FullAmmo, FiveAmmo, FourAmmo, ThreeAmmo, TwoAmmo, OneAmmo, Empty;

    // Start is called before the first frame update
    void Start()
    {
        ammoCurrent = pepeLePew.currentClipBullets;
    }

    // Update is called once per frame
    void Update()
    {
        AmmoBarUp();
    }

    public void AmmoBarUp()
    {
        ammoCurrent = pepeLePew.currentClipBullets;

        if (ammoCurrent == 6)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = FullAmmo;
        }
        else if (ammoCurrent == 5)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = FiveAmmo;
        }
        else if (ammoCurrent == 4)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = FourAmmo;
        }
        else if (ammoCurrent == 3)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ThreeAmmo;
        }
        else if (ammoCurrent == 2)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = TwoAmmo;
        }
        else if (ammoCurrent == 1)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = OneAmmo;
        }
        else if (ammoCurrent == 0)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Empty;
        }
    }
}