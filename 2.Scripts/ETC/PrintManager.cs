using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


//https://stackoverflow.com/questions/49188307/how-to-direct-printing-of-photo-or-text-using-unity-without-preview

public class PrintManager : MonoBehaviour
{
    public Button printbtn;
    public GameObject printTimer;
    public TextMeshProUGUI waitTimeText;

    string printImgPath;
    int currTime = 30;



    void Start()
    {
        printImgPath = Application.persistentDataPath + "/6.Print/PrintPicture.png";
    }

    public void PrintButtonOn()
    {
        GameManager.instance.SFXSound("ClickSound");

        string printerName = "Sinfonia CHC-S2245";
        string _rePrintPath = Regex.Replace(printImgPath, "/", "\\");
        string printFullCommand =
            "rundll32 C:\\WINDOWS\\system32\\shimgvw.dll,ImageView_PrintTo " + "\"" + 
            _rePrintPath + "\"" + " " + "\"" + printerName + "\"";

        PrinterStart(printFullCommand);
    }

    void PrinterStart(string _cmd)
    {
        try
        {
            Process myProcess = new Process();
            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = "/c " + _cmd;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();

            printTimer.transform.GetChild(0).transform.DORotate
                (new Vector3(0f, 0f, -360f), 2.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
                .SetLoops(-1);

            StartCoroutine(PrintWait());
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }

    IEnumerator PrintWait()
    {
        while(currTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currTime -= 1;
            waitTimeText.text = "Printing.. " + currTime.ToString();
        }

        if (currTime >= 0)
        {
            printbtn.interactable = false;
            printbtn.gameObject.SetActive(true);
            printTimer.SetActive(false);
        }  
    }
}
