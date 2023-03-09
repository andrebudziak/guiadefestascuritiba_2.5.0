using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public partial class categoria : System.Web.UI.Page
{
    private int vTipoCategoria = 0;
    private string vCategoria = "";
    private AjaxControlToolkit.Accordion acc;
    private WebService ws = new WebService();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["tipo_categoria"] != null)
            {
                string ncategoria = Session["tipo_categoria"].ToString();
                string busca = "";
                DataSet dadosDesc = new DataSet();
                dadosDesc = ws.descricaoCategoria(ncategoria);

                if (Session["busca"] != null)
                {
                    busca = Session["busca"].ToString();

                    if (dadosDesc.Tables[0].Rows.Count > 0)
                    {
                        vTipoCategoria = Convert.ToInt32(dadosDesc.Tables[0].Rows[0]["codigo"].ToString());
                        Session["tipo_categoria"] = vTipoCategoria;
                        DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, busca);
                        if (dados.Tables[0].Rows.Count == 0)
                        {
                            dvInfo.Visible = true;
                           //pnAnuncioCategoria.Visible = false;
                        }
                        else
                        {
                            dvInfo.Visible = false;
                            dlAnunciante.DataSource = dados;
                            dlAnunciante.DataBind();
                            lblTituloCat.Text = "Resultado da busca:" + dados.Tables[0].Rows.Count.ToString();
                        }
                        
                        Session["busca"] = null;
                    }
                }
                else if (dadosDesc.Tables[0].Rows.Count > 0)
                {
                   vTipoCategoria = Convert.ToInt32(dadosDesc.Tables[0].Rows[0]["codigo"].ToString());
                   Session["tipo_categoria"] = vTipoCategoria;
                   DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, "0");
                   if (dados.Tables[0].Rows.Count == 0)
                   {
                       dvInfo.Visible = true;
                      // pnAnuncioCategoria.Visible = false;
                   }
                   else
                   {
                       dvInfo.Visible = false;
                       dlAnunciante.DataSource = dados;
                       dlAnunciante.DataBind();
                   }

                }

            }
            else
            {
                string busca = "";
                DataSet dadosDesc = new DataSet();

                if (Session["busca"] == null)
                {
                    if (Request.QueryString["tipo_categoria"] != null)
                    {
                        Session["tipo_categoria"] = Request.QueryString["tipo_categoria"].ToString();
                    }
                    else
                    {
                        string ncategoria = Page.Request.Url.Query;
                        int pos = ncategoria.IndexOf("Categoria/");
                        if (pos == - 1)
                        {
                            Response.Redirect("Index.aspx");
                        }
                        else
                        {

                            ncategoria = ncategoria.Substring(pos + 10, ncategoria.Length - pos - 10);
                            ncategoria = Server.UrlDecode(ncategoria);

                            ncategoria = Regex.Replace(ncategoria, "-", " ");
                            dadosDesc = ws.descricaoCodigoCategoria(ncategoria);

                            Session["tipo_categoria"] = dadosDesc.Tables[0].Rows[0]["codigo"].ToString();
                        }
                    }

                }

                if (Session["busca"] != null)
                {
                    busca = Session["busca"].ToString();

                    DataSet dados = ws.ConsultaAnuncioBusca(busca);
                    if (dados.Tables[0].Rows.Count == 0)
                    {
                        dvInfo.Visible = true;
                       // pnAnuncioCategoria.Visible = false;
                    }
                    else
                    {
                        dvInfo.Visible = false;                        
                        dlAnunciante.DataSource = dados;
                        dlAnunciante.DataBind();
                        lblTituloCat.Text = "Resultado da busca:" + dados.Tables[0].Rows.Count.ToString();

                    }
                    Session["busca"] = null;

                }
                else if (dadosDesc.Tables.Count > 0)
                {
                    vTipoCategoria = Convert.ToInt32(dadosDesc.Tables[0].Rows[0]["codigo"].ToString());
                    Session["tipo_categoria"] = vTipoCategoria;
                    DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, "0");
                    if (dados.Tables[0].Rows.Count == 0)
                    {
                        dvInfo.Visible = true;
                        //pnAnuncioCategoria.Visible = false;
                    }
                    else
                    {
                        dvInfo.Visible = false;
                        dlAnunciante.DataSource = dados;
                        dlAnunciante.DataBind();
                    }
                }
                else
                {

                    vTipoCategoria = Convert.ToInt32(Session["tipo_categoria"].ToString());
                    DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, "0");
                    if (dados.Tables[0].Rows.Count == 0)
                    {
                        dvInfo.Visible = true;
                        //pnAnuncioCategoria.Visible = false;
                    }
                    else
                    {
                        dvInfo.Visible = false;
                        dlAnunciante.DataSource = dados;
                        dlAnunciante.DataBind();
                    }
                }

            }


            DataSet dadosT = new DataSet();
            dadosT = ws.descricaoCategoria(Convert.ToString(vTipoCategoria));
            Session["tipo_categoria"] = vTipoCategoria;
            if (dadosT.Tables[0].Rows.Count != 0)
            {
                lblTituloCat.Text = dadosT.Tables[0].Rows[0]["descricao"].ToString();
                Page.Title = dadosT.Tables[0].Rows[0]["descricao"].ToString();
            }

            recarregaLayout();
            Session["tipo_categoria"] = null;
            Session["ordena_categoria"] = null;
            Session["ordena_categoria"] = vTipoCategoria;
        }
        /*else
        {
            Session.Clear();
        
        }*/
    }

    protected void dlAnunciante_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView dbr = (DataRowView)e.Item.DataItem;

        Label lblCodigoAnuncio = (Label)e.Item.FindControl("lblCodigoAnuncio");
        lblCodigoAnuncio.Text = Convert.ToString(DataBinder.Eval(dbr, "codigo"));

        Label lblDescricao = (Label)e.Item.FindControl("lblDescricao");
        lblDescricao.Text = Convert.ToString(DataBinder.Eval(dbr, "descricao"));

        Label lblTituloAnuncio = (Label)e.Item.FindControl("lblNomeFantasia");
        lblTituloAnuncio.Text = Convert.ToString(DataBinder.Eval(dbr, "nome_fantasia"));

//        Label lblTituloAnuncio2 = (Label)e.Item.FindControl("lblModalAnuncio");
//        lblTituloAnuncio2.Text = Convert.ToString(DataBinder.Eval(dbr, "nome_fantasia"));

        Label lblBairroCidade = (Label)e.Item.FindControl("lblBairroCidade");
        lblBairroCidade.Text = Convert.ToString(DataBinder.Eval(dbr, "bairro")) + "-" + Convert.ToString(DataBinder.Eval(dbr, "cidade"));

        //Label lblBairro = (Label)e.Item.FindControl("lblBairro");
        //lblBairro.Text = Convert.ToString(DataBinder.Eval(dbr, "bairro")) ;

        Label lblEndereco = (Label)e.Item.FindControl("lblEndereco");
        lblEndereco.Text = Convert.ToString(DataBinder.Eval(dbr, "endereco"));

        Label lblInformacoes = (Label)e.Item.FindControl("lblInformacoes");
        lblInformacoes.Text = Convert.ToString(DataBinder.Eval(dbr, "informacoes"));

        if (Convert.ToString(DataBinder.Eval(dbr, "logo")) != "")
        {
            Image btn = (Image)e.Item.FindControl("btnImgLogo");

            string logo = Page.ResolveUrl("~/logos/" + Convert.ToString(DataBinder.Eval(dbr, "logo")));

            btn.ImageUrl = logo;

            //Image imgLogo = (Image)e.Item.FindControl("ImgLogo");
            //imgLogo.ImageUrl = logo;

            //btn.Attributes["href"] = "detalhe_categoria.aspx?id_anuncio=" + Convert.ToString(DataBinder.Eval(dbr, "codigo"));
        }

        if (Convert.ToString(DataBinder.Eval(dbr, "site")) == "")
        {

            //HyperLink lnk = (HyperLink)e.Item.FindControl("aDetalheAnuncio");
        }
        else
        {
            //HyperLink lnk = (HyperLink)e.Item.FindControl("aSite");
            //lnk.Attributes["href"] = "detalhe_categoria.aspx?id_anuncio=" + Convert.ToString(DataBinder.Eval(dbr, "codigo"));
            
            //string myScript = "window.open('" + "http://" + Convert.ToString(DataBinder.Eval(dbr, "site")) + "', null,''); void(0)";
            //lnk.Attributes["onclick"] = myScript;

            //HyperLink lnk2 = (HyperLink)e.Item.FindControl("aLogo");

            //string myScript = "window.open('" + "http://" + Convert.ToString(DataBinder.Eval(dbr, "site")) + "', null,''); void(0)";
            //lnk2.Attributes["onclick"] = myScript;

            //lnk2.Attributes["href"] = "detalhe_categoria.aspx?id_anuncio=" + Convert.ToString(DataBinder.Eval(dbr, "codigo"));

        }


        if (Convert.ToString(DataBinder.Eval(dbr, "email")) == "")
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aEmail");
        }
        else
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aEmail");
            lnk.Attributes["href"] = "mailto:" + Convert.ToString(DataBinder.Eval(dbr, "email"));
        }


        if (Convert.ToString(DataBinder.Eval(dbr, "endereco")) == "")
        {
            //string endereco = Convert.ToString(DataBinder.Eval(dbr, "endereco")) + " " + Convert.ToString(DataBinder.Eval(dbr, "bairro")) + " " + Convert.ToString(DataBinder.Eval(dbr, "cidade"));
            //string info = "";//lblTituloAnuncio.Text + "</br>" + endereco;

            //LinkButton lnk = (LinkButton)e.Item.FindControl("aMapa");
           // lnk.Enabled = false;
            //lnk.Attributes["onclick"] = "javascript:window.open('mapa.aspx?info=" + info + "&endereco="+endereco+"', null, 'left = 400, top = 100, height = 480, width = 500, status = no, resizable = no, scrollbars = no, toolbar = no, location = no, menubar = no'); void(0)";

        }

        if (Convert.ToString(DataBinder.Eval(dbr, "facebook")) != "")
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aFacebook");
            lnk.Attributes["href"] = Convert.ToString(DataBinder.Eval(dbr, "facebook"));
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-facebook";
        }
        else
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aFacebook");
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-facebook disabled";        
        }

        if (Convert.ToString(DataBinder.Eval(dbr, "gmais")) != "")
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aGmais");
            lnk.Attributes["href"] = Convert.ToString(DataBinder.Eval(dbr, "gmais"));
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-google-plus";        
        }
        else
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aGmais");
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-google-plus disabled";        
        }

        if (Convert.ToString(DataBinder.Eval(dbr, "orkut")) != "")
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aInsta");
            lnk.Attributes["href"] = Convert.ToString(DataBinder.Eval(dbr, "orkut"));
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-instagram";
        }
        else
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aInsta");
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-instagram disabled";
        }

        if (Convert.ToString(DataBinder.Eval(dbr, "twitter")) != "")
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aTwitter");
            lnk.Attributes["href"] = Convert.ToString(DataBinder.Eval(dbr, "twitter"));
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-twitter";
        }
        else
        {
            HyperLink lnk = (HyperLink)e.Item.FindControl("aTwitter");
            lnk.Attributes["class"] = "btn btn-social-icon btn-link btn-twitter disabled";
        }

    }   


    protected void dlAnunciante_ItemCommand(object source, DataListCommandEventArgs e)
    {


       /*  if (e.CommandName == "ContaClick") 
        {
            Label lnk1 = (Label)e.Item.FindControl("lblCodigoAnuncio"); 
            string key = lnk1.Text;
            ws.contaClicks(Convert.ToInt32(key));

            HyperLink lnk = (HyperLink)e.Item.FindControl("aSite");

            string myScript = "window.open('"+lnk.NavigateUrl+"', null,''); void(0)";

            lnk.Attributes["onclick"] = myScript; 
        }

        if (e.CommandName == "MostraMapa")
        {

            Label lblEndereco = (Label)e.Item.FindControl("lblEndereco");
            Label lblBairroCidade = (Label)e.Item.FindControl("lblBairroCidade");
            Label lblNomeFantasia = (Label)e.Item.FindControl("lblNomeFantasia");

            string endereco;
            endereco = lblEndereco.Text +" "+ lblBairroCidade.Text ;

            ifCategoriaAnuncio.Attributes["src"] = Page.ResolveUrl("~/mapa.aspx?endereco=" + endereco+"&t="+lblNomeFantasia.Text);
            AjaxControlToolkit.ModalPopupExtender mp = (AjaxControlToolkit.ModalPopupExtender)e.Item.FindControl("btnMapaCategoria_ModalPopupExtender");
            mp.Show();
            UpdatePanel1.Update();
        
        }

        if (e.CommandName == "MostraOferta")
        {
            Label lblCodigoOferta = (Label)e.Item.FindControl("lblCodigoOferta");
            Session["Oferta"] = lblCodigoOferta.Text;

            int codigo = Convert.ToInt32(lblCodigoOferta.Text);
            DataSet dados = ws.ConsultaOfertaTela(codigo, "0");
            string descricao = dados.Tables[0].Rows[0]["nome_fantasia"].ToString();

            UrlRewrite urlr = new UrlRewrite();
            descricao = urlr.RemoveSpecialCharacters(descricao);

            Response.Redirect("~/Oferta/" + descricao);


        }*/

    }

    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList actionsDDL = sender as DropDownList;
        Label projectCandidateIdHF = actionsDDL.Parent.FindControl("lblCodAnuncio") as Label;
    }

    private void recarregaLayout()
    {
        //btnFechar.ImageUrl = Page.ResolveUrl("~/imagens/close.png");
        Image img = (Image)UpdateProgress2.FindControl("imgAguarde");
        img.ImageUrl = Page.ResolveUrl("~/imagens/wait.gif");
        
    }



    protected void lnkAnuncio_Click(object sender, EventArgs e)
    {
        if (Session["ordena_categoria"] != null)
        {
            vTipoCategoria = Convert.ToInt32(Session["ordena_categoria"].ToString());
            DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, "0");
            DataTable table = dados.Tables["anuncio"];
            DataView view = table.DefaultView;
            view.Sort = "nome_fantasia";

            dlAnunciante.DataSource = view;
            dlAnunciante.DataBind();
        }
        UpdatePanel2.Update();
    }

    protected void lnkBairro_Click(object sender, EventArgs e)
    {
        if (Session["ordena_categoria"] != null)
        {
            vTipoCategoria = Convert.ToInt32(Session["ordena_categoria"].ToString());
            DataSet dados = ws.ConsultaAnuncioTela(vTipoCategoria, "0");
            DataTable table = dados.Tables["anuncio"];
            DataView view = table.DefaultView;
            view.Sort = "bairro";

            dlAnunciante.DataSource = view;
            dlAnunciante.DataBind();
        }
        UpdatePanel2.Update();

    }



}
