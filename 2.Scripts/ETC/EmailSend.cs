using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using TMPro;
using UnityEngine.UI;




public class EmailSend : MonoBehaviour
{
    public InputField virtualInputField;
    public TMP_InputField emailAddrInputField;

    public GameObject completePanel;
    public GameObject waitImg;
    public TextMeshProUGUI waitText;



    public void EmailSendButtoOn()
    {
        if(emailAddrInputField.text != "")
        {
            completePanel.SetActive(true);
            StartCoroutine(WaitSend());
            StartCoroutine(_EmailSend());
        }
    }


    IEnumerator _EmailSend()
    {
        yield return new WaitForSeconds(0.2f);
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("gateways2021@naver.com"); //������ ���
        mail.To.Add(emailAddrInputField.text);  //�޴»��
        mail.Subject = "MyPhotoBooth ����";
        mail.Body = "My Photo Booth�� �̿��� �ּż� �����մϴ�.\n" +
            "������ ������ �߾��� �����帳�ϴ�.\n" +
            "(Thank you for using My Photo Booth. We will send you precious memories.)\n\n" +
            "��������ν� ���񽺰� �ñ��Ͻ� ����(If you are curious about the My Photo Booth service,)\n" +
            "https://smartstore.naver.com/gateways/products/8430425136 �� �湮�Ͻø� �پ��� ������ ������ �� �ֽ��ϴ�.\n" +
            "(You can get various information by visiting.)\n\n" +
            "����Ʈ������ ���������� (Gateways main page)\n" +
            "http://gateways.kr/ \n" +
            "����Ʈ������ ��(Gateways Mall)\n" +
            "https://smartstore.naver.com/gateways \n\n";

        //÷������ - ��뷮�� �ȵ�
        Attachment attachment;
        //���ϰ��
        attachment = new Attachment(Application.persistentDataPath + "/6.Print/PrintPicture.png");
        mail.Attachments.Add(attachment);

        SmtpClient smtpServer = new SmtpClient("smtp.naver.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential("gateways2021@naver.com", "gw2022!!##"); //������ ����ּ� �� ���
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

    IEnumerator WaitSend()
    {
        waitImg.SetActive(true);

        waitText.text = "Sending..";
        yield return new WaitForSeconds(1);
        waitText.text = "Sending...";
        yield return new WaitForSeconds(1);
        waitText.text = "Sending..";
        yield return new WaitForSeconds(1);
        waitText.text = "Sending...";
        yield return new WaitForSeconds(1);
        waitText.text = "Sending..";
        yield return new WaitForSeconds(1);

        waitImg.SetActive(false);
    }

    public void EmailAddressShow()
    {
        emailAddrInputField.text = virtualInputField.text;
    }
}
