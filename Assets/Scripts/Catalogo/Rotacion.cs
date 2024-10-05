using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;
    [SerializeField] GameObject backBotton;
    [SerializeField] GameObject fowardBotton;

    public void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
        backBotton.SetActive(false);
    }

    public void RotateFoward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180;
        FowardButtonOptions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));

    }

    public void FowardButtonOptions()
    {
        if (backBotton.activeInHierarchy == false)
        {
            backBotton.SetActive(true);
        }
        if (index == pages.Count - 1)
        {
            fowardBotton.SetActive(false);
        }
    }


    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0;
        pages[index].SetAsLastSibling();
        BackButtonOptions();
        StartCoroutine(Rotate(angle, false));

    }
    public void BackButtonOptions()
    {
        if (fowardBotton.activeInHierarchy == false)
        {
            fowardBotton.SetActive(true);
        }
        if (index - 1 == -1)
        {
            backBotton.SetActive(false);
        }
    }


    IEnumerator Rotate(float angle, bool foward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation);
            if (angle1 < 0.1f)
            {
                if (foward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }



}
