using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contactPage : System.Web.UI.Page
{
    string a = "";
    const string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            clear();
            string code = GenerateVCodeImage();
            txtSCode.Text = code;
            ViewState["capcha"] = txtSCode.Text;
            //lblcode.Text = txtSCode.Text;
            Image2.ImageUrl = "/Upload/a1.jpeg" + "?" + txtSCode.Text.Trim();
        }

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        MailtoAdmin();
        Mailtoclient();
        clear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your request has been sent successfully!');window.location ='contactPage.aspx';", true);
    }
    private void clear()
    {
        txtName.Text = "";
        txtEmail.Text = "";
        txtphone.Text = "";
        TextMassage.Text = "";
        txtcountry.Text = "";
        CompanyName.Text = "";
        txtCode.Text = "";
    }

    private void Mailtoclient()
    {
        string filename = Server.MapPath("MailFormat/ClientContact.html");
        string mailbody = System.IO.File.ReadAllText(filename);
        mailbody = mailbody.Replace("##YourName##", txtName.Text);
        mailbody = mailbody.Replace("##Date##", System.DateTime.Now.ToString("dd/MM/yyyy"));
        MailMessage mailmsg = new MailMessage();
        MailAddress mailaddress = new MailAddress("webauth@barodaweb.net", "Supertech Fabrics");

        mailmsg.From = mailaddress;
        mailmsg.To.Add(txtEmail.Text);

        mailmsg.IsBodyHtml = true;
        mailmsg.Priority = System.Net.Mail.MailPriority.High;
        mailmsg.Subject = "Contact to Supertech Fabrics";
        mailmsg.Body = mailbody;

        SmtpClient smtp = new SmtpClient("bizmail.thehpanel.com", 587);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("akash@barodaweb.net", "lIaDBsd3u4WFx");
        smtp.EnableSsl = true;
        smtp.Send(mailmsg);



    }

    private void MailtoAdmin()
    {

        string filename = Server.MapPath("MailFormat/AdminContact.html");
        string mailbody = System.IO.File.ReadAllText(filename);
        mailbody = mailbody.Replace("##YourName##", txtName.Text);
        mailbody = mailbody.Replace("##CompanyName##", CompanyName.Text);
        mailbody = mailbody.Replace("##Date##", System.DateTime.Now.ToString("dd/MM/yyyy"));
        mailbody = mailbody.Replace("##Email##", txtEmail.Text);
        // mailbody = mailbody.Replace("##Country##", drpcountry.SelectedItem.Text);
        mailbody = mailbody.Replace("##Phone##", txtphone.Text);
        mailbody = mailbody.Replace("##Service##", txtcountry.Text);
        mailbody = mailbody.Replace("##Details##", TextMassage.Text);
        MailMessage mailmsg = new MailMessage();
        MailAddress mailaddress = new MailAddress("webauth@barodaweb.net", "Supertech Fabrics");
        mailmsg.From = mailaddress;

        mailmsg.To.Add("info@supertechfabrics.com");


        mailmsg.IsBodyHtml = true;
        mailmsg.Priority = MailPriority.High;
        mailmsg.Subject = "Inquiry Details";
        mailmsg.Body = mailbody;

        SmtpClient smtp = new SmtpClient("bizmail.thehpanel.com", 587);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("akash@barodaweb.net", "lIaDBsd3u4WFx");
        smtp.EnableSsl = true;
        smtp.Send(mailmsg);
    }

    protected void lnkSecutiryCode_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        string code = GenerateVCodeImage();
        txtSCode.Text = code;
        ViewState["capcha"] = txtSCode.Text;
        //lblcode.Text = txtSCode.Text;
        Image2.ImageUrl = "~/Upload/a1.jpeg" + "?" + txtSCode.Text.Trim();
        //txtSCode.Text = code;
        //Image2.ImageUrl = "~/Upload/a.jpeg" + "?" + code;

        //txtSCode.Text = code;
        //Image2.ImageUrl = "~/Upload/capcha-img.jpeg" + "?" + code;

    }

    private string GenerateVCodeImage()
    {

        System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap(150, 35);
        System.Drawing.Graphics oGraphic = System.Drawing.Graphics.FromImage(oBitmap);
        System.Drawing.Color foreColor = default(System.Drawing.Color);
        System.Drawing.Color backColor = default(System.Drawing.Color);

        string sText = generateVCode(4);
        string sFont = "Comic Sans MS";
        ViewState["capcha"] = sText;

        foreColor = System.Drawing.Color.FromArgb(220, 220, 220);
        backColor = System.Drawing.Color.FromArgb(190, 190, 190);

        System.Drawing.Drawing2D.HatchBrush oBrush = new System.Drawing.Drawing2D.HatchBrush((System.Drawing.Drawing2D.HatchStyle)generateHatchStyle(), foreColor, backColor);

        System.Drawing.SolidBrush oBrushWrite = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

        oGraphic.FillRectangle(oBrush, 0, 0, 150, 50);
        oGraphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        System.Drawing.Font oFont = new System.Drawing.Font(sFont, 14);
        System.Drawing.PointF oPoint = new System.Drawing.PointF(5f, 4f);

        oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint);

        //Response.ContentType = "images/jpeg"

        //a = Server.MapPath("Upload") + "\\" + "a.jpeg";
        a = Server.MapPath("Upload") + "\\" + "a1.jpeg";
        oBitmap.Save(a, System.Drawing.Imaging.ImageFormat.Jpeg);
        //oBitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        oGraphic.Dispose();
        oBitmap.Dispose();
        GC.Collect();

        return sText;
    }
    private string generateVCode(int CodeLength)
    {

        Random rand = new Random();
        int maxRand = Letters.Length - 1;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i <= CodeLength; i++)
        {
            int index = rand.Next(maxRand);
            sb.Append(Letters[index]);
        }

        return sb.ToString();
    }

    private System.Drawing.Drawing2D.HatchStyle generateHatchStyle()
    {
        ArrayList slist = new ArrayList();
        foreach (System.Drawing.Drawing2D.HatchStyle style in System.Enum.GetValues(typeof(System.Drawing.Drawing2D.HatchStyle)))
        {
            slist.Add(style);
        }

        Random randObj = new Random();
        int index = randObj.Next(slist.Count - 1);

        return (System.Drawing.Drawing2D.HatchStyle)slist[index];
    }



}