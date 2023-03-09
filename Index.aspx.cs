using System;
using System.Data;


public partial class Index : System.Web.UI.Page
{
    private WebService ws = new WebService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        Session["menu"] = "0";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeraDestaque();
        }   
    
    }

    private void GeraDestaque()
    {
        DataSet dados = ws.ConsultaDestaque(0,"0",1);
        string script = "";

        script += "<section>";

        foreach (DataRow tRow in dados.Tables[0].Rows)
        {
            script += "<article>";
            script += "<div class='col-md-6'>";

            script += "<div class='panel panel-default'>";
            script += "<div class='panel-body'>";
            script += tRow["destaque"].ToString();
            script += "</div>";
            script += "</div>";

            script += "</div>";
            script += "</article>";
        }


        script += "</section>";
        lblDestaque.Text = script;

    }



}
