using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class N : MonoBehaviour
{
    public static string S = "";
    public void A()
    {
        S = "a";
        SceneManager.LoadScene("SampleScene");
    }
    public void B()
    {
        S = "b";
        SceneManager.LoadScene("SampleScene");
    }
    public void C()
    {
        S = "c";
        SceneManager.LoadScene("SampleScene");
    }
    public void D()
    {
        S = "d";
        SceneManager.LoadScene("SampleScene");
    }
}
