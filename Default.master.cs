using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;

public partial class _Default : System.Web.UI.MasterPage
{
    private int vTipoCategoria = 0, ht = 0;
    private WebService ws = new WebService();
    DataTable tabela = new DataTable();
    private DataSet dadosBanner = new DataSet();
    private DataSet dadosBannerTopo = new DataSet();
    PagedDataSource pds = new PagedDataSource();

    private void Page_PreInit(object sender, EventArgs e)
    {        
        Session["menu"] = "0";
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
                CarregaSlideGrande();
                CarregaMenu();

                Session["menu"] = "1";

                //banner lateral direito
                DataSet dadosBanner = ws.montabannerlateral("2");
                dlPublicidade.DataSource = dadosBanner;
                dlPublicidade.DataBind();

                montaBanner();


            string appPath = Page.Request.ApplicationPath.ToString();

            lblCodigo.Visible = false;
            lblCodigoCategoria.Visible = false;
            lblDescricao.Visible = false;

            if (Session["tipo_categoria"] != null)
            {
                string ncategoria = Session["tipo_categoria"].ToString();
                DataSet dados = new DataSet();
                dados = ws.descricaoCodigoCategoria(ncategoria);

                if (dados.Tables[0].Rows.Count > 0)
                    vTipoCategoria = Convert.ToInt32(dados.Tables[0].Rows[0]["codigo"].ToString());


                //btnFechar.PostBackUrl = Page.Request.ApplicationPath.ToString() + "/Categoria/" + dados.Tables[0].Rows[0]["descricao"].ToString() + "/CURITIBA";

                string palavras = "Curitiba,Guia de Festas Curitiba,Festas Curitiba,festas,";               

            }

            //frame.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnBusca.UniqueID + "').click();return false;}} else {return true}; ");
            recarregaLayout();


        }      
    }

    private void CarregaMenu()
    {
        UrlRewrite urlr = new UrlRewrite();
        string script = string.Empty;

//        UrlMapping urlMap = null;

        // Abre o Web.config
  //      Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

        // Recupera a seção urlMappings, do web.config
    //    UrlMappingsSection urlMapSection = (UrlMappingsSection)config.GetSection("system.web/urlMappings");



        script += "<ul class='nav navbar-nav'>";
        script += "<li><a href='Index.aspx'>Home<span class='sr-only'>(current)</span></a></li>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Locais<span class='caret'></span></a>";
        script +="<ul class='dropdown-menu' role='menu'>";

        DataSet dadosLocal = ws.montamenu("1");
        foreach (DataRow tRow in dadosLocal.Tables[0].Rows)
        {
            
            script += "<li><a href='" +  urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
      //      urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

        //    urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
          //  config.Save(); 


        }
        script += "</ul>";
        script += "</li>";
        script += "</ul>";
        
        //lblLocal.Text = script;
        //script = string.Empty;

        script += "<ul class='nav navbar-nav'>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Decoração<span class='caret'></span></button>";
        script += "<ul class='dropdown-menu' role='menu'>";

        DataSet dadosDecoracao = ws.montamenu("2");
        foreach (DataRow tRow in dadosDecoracao.Tables[0].Rows)
        {
            script += "<li><a href='" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

            //urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            //config.Save(); 


        }
        script += "</ul>";
        script += "</li>";
        script += "</ul>";
        //lblDecoracao.Text = script;
        //script = string.Empty;

        script += "<ul class='nav navbar-nav'>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Alimentação<span class='caret'></span></button>";
        script += "<ul class='dropdown-menu' role='menu'>";

        DataSet dadosAlimentacao = ws.montamenu("3");
        foreach (DataRow tRow in dadosAlimentacao.Tables[0].Rows)
        {
            script += "<li><a href='" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

            //urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            //config.Save(); 

        }
        script += "</ul>";
        script += "</li>";
        script += "</ul>";
       // lblAlimentacao.Text = script;
       // script = string.Empty;

        script += "<ul class='nav navbar-nav'>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Diversão<span class='caret'></span></button>";
        script += "<ul class='dropdown-menu' role='menu'>";

        DataSet dadosDiversao = ws.montamenu("4");
        foreach (DataRow tRow in dadosDiversao.Tables[0].Rows)
        {
            script += "<li><a href='" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

            //urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            //config.Save(); 

        }
        script += "</ul>";
        script += "</li>";
        script += "</ul>";
        //lblDiversao.Text = script;
       // script = string.Empty;

        script += "<ul class='nav navbar-nav'>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Serviços<span class='caret'></span></button>";
        script += "<ul class='dropdown-menu' role='menu'>";

        DataSet dadosServico = ws.montamenu("5");
        foreach (DataRow tRow in dadosServico.Tables[0].Rows)
        {
            script += "<li><a href='" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

            //urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            //config.Save(); 

        }
        script += "</ul>";
        script += "</li>";
        script += "</ul>";
        //lblServicos.Text = script;
       // script = string.Empty;

        script += "<ul class='nav navbar-nav'>";
        script += "<li class='dropdown'>";
        script += "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Tipos<span class='caret'></span></button>";
        script += "<ul class='dropdown-menu' role='menu'>";

        DataSet dadosTipo = ws.montamenu("6");
        foreach (DataRow tRow in dadosTipo.Tables[0].Rows)
        {
            script += "<li><a href='" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()) + "'>" + tRow["descricao"].ToString() + "</a></li>";
            script += "<li class='divider'></li>";

            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Categoria/" + urlr.RemoveSpecialCharacters(tRow["descricao"].ToString()), "~/categoria.aspx?tipo_categoria=" + tRow["codigo"].ToString());

            //urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            //config.Save(); 

        }
        script += "</ul>";
        script += "</li>";

        script += "</ul>";      
        

        lblLocal.Text = script;
        script = string.Empty;
    }

    private void CarregaSlideGrande()
    {

        string script = "", foto="", link = "",script2="";
        int cont=0,i;

        //banner 730X220
        DataSet dadosT = new DataSet();
        dadosBanner = ws.montaBannerPermuta(1);

        script = string.Empty;
        script2 = string.Empty;
        script += "<div class='carousel-inner' role='listbox'>";
        
        foreach (DataRow tRow in dadosBanner.Tables[0].Rows)
        {
            if (cont == 0)
               script2 += "<li data-target='#carousel-example-generic' data-slide-to=" + cont.ToString() + " class='active'></li>";
            else
               script2 += "<li data-target='#carousel-example-generic' data-slide-to=" + cont.ToString() + "></li>";

            cont++;

            if (cont == 1)
                script += "<div class='item active'>";
            else
                script += "<div class='item'>";
             
            foto = Page.ResolveUrl("~/banners/" + tRow["descricao"].ToString());
            dadosT = ws.ConsultaAnuncioCliente(Convert.ToInt32(tRow["codigo_cliente"].ToString()));
            link = "http://" + dadosT.Tables[0].Rows[0]["site"].ToString();
            script += "<a href='" + link + "' target='_new'>";
            script += "<img id='image' src='" + foto + "' border='0' alt=''  class='img-responsive' style='width: 100 %; height: 100 %;'  />";
            script += "</a>";
            
            
            script += "</div>";


        }
        script += "</div>";
        lblSlide.Text = script;
        //lblIndicators.Text = script2;

    }



    protected void btnBusca_Click(object sender, ImageClickEventArgs e)
    {
        if (txtBusca.Text != "")
        {
            //string appPath = Server.MapPath("~"); 
            
            Session["busca"] = txtBusca.Text;
            Session["tipo_categoria"] = null;

            UrlRewrite urlr = new UrlRewrite();
            txtBusca.Text = urlr.RemoveSpecialCharacters(txtBusca.Text);
            
            //Session["tipo_categoria"] = urlr.RemoveSpecialCharacters(Session["tipo_categoria"].ToString());
            
            Response.Redirect("~/Categoria/" + txtBusca.Text) ;
            
        }
        else
        {
            string myScript = @"alert('Digite um conteudo para pesquisa!');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clientscript", "<script language='JavaScript'>" + myScript + "</script>", false);
        }

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    private DataTable CriaDataTable()
    {

        DataTable mDataTable = new DataTable();

        DataColumn mDataColumn;

        mDataColumn = new DataColumn();
        mDataColumn.DataType = Type.GetType("System.String");
        mDataColumn.ColumnName = "email";
        mDataTable.Columns.Add(mDataColumn);

        mDataColumn = new DataColumn();
        mDataColumn.DataType = Type.GetType("System.String");
        mDataColumn.ColumnName = "usuario";
        mDataTable.Columns.Add(mDataColumn);
        
        return mDataTable;

    }

    private void incluirNoDataTable(string email, string usuario, DataTable mTable)
    {

        DataRow linha;
        linha = mTable.NewRow();

        linha["email"] = email;

        linha["usuario"] = usuario;

        mTable.Rows.Add(linha);

        tabela = mTable;
        HttpContext.Current.Session["dadosT"] = mTable;

    } 
 
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
       /* if (ddlTipoAcesso.SelectedValue == "2")
        {
            Session["UserName"] = txtUsuarioCliente.Text;
            Session["EmailUsuario"] = txtEmailCliente.Text;
        
        }
        string senha = SenhaHASH(Convert.ToString(DateTime.Now));
        Response.Redirect("Chat.aspx?h=" + senha + "&rid=2&tp=" + ddlTipoAcesso.SelectedValue, false);

        UpdatePanel2.Update();*/
    }

    protected void btnMini_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnLoginCliente_Click(object sender, ImageClickEventArgs e)
    {
        /*Service ws = new Service();
        int login = ws.authenticateUser(txtUsuario.Text, txtSenha.Text);
        if (login != 0)
        {
            string nomeUser = ws.NomeUser(txtUsuario.Text, txtSenha.Text);
            Session["UserName"] = nomeUser;
            Session["idCliente"] = login;

            string senha = SenhaHASH(Convert.ToString(DateTime.Now));
            Response.Redirect("Chat.aspx?h="+senha+"&rid=2&tp=" + ddlTipoAcesso.SelectedValue, false);
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clientscript", "<script language='JavaScript'>alert('Usuario e/ou Senha Invalido(s)! Verifique'); </script>", false);
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }
        UpdatePanel2.Update();*/

    }

    public string SenhaHASH(string Senha)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //TabContainer1.ActiveTabIndex = GeraIndice(20);
        //TabContainer2.ActiveTabIndex = GeraIndice(4);
        //upBannerTopoDireito.Update();
        //upBannerCentral.Update();
    }

    protected void ddlTipoAcesso_SelectedIndexChanged(object sender, EventArgs e)
    {
     /*   if (ddlTipoAcesso.SelectedValue != "0")
        {
            if (ddlTipoAcesso.SelectedValue == "1")
            {
                pnChatCli.Visible = true;
                pnChatUsr.Visible = false;

            }
            if (ddlTipoAcesso.SelectedValue == "2")
            {
                pnChatUsr.Visible = true;
                pnChatCli.Visible = false;
            }

            UpdatePanel2.Update();
        }*/
    }

    protected void montaBanner()
    {

        int contaBanner = 0;
        DataSet dadosT;

        if (Session["tipo_categoria"] != null)
        {

            string sCat = Session["tipo_categoria"].ToString();
            dadosT = ws.descricaoCodigoCategoria(sCat);

            if (dadosT.Tables[0].Rows.Count > 0)
            {
                string codigo = dadosT.Tables[0].Rows[0]["codigo"].ToString();
                vTipoCategoria = Convert.ToInt32(codigo);
            }
        }
        
    }


    protected void dlPublicidade_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView dbr = (DataRowView)e.Item.DataItem;

        if (Convert.ToString(DataBinder.Eval(dbr, "descricao")) != "")
        {

            Label lblCodigoBanner = (Label)e.Item.FindControl("lblCodigoBanner");
            lblCodigoBanner.Text = Convert.ToString(DataBinder.Eval(dbr, "codigo"));


            Image img = (Image)e.Item.FindControl("imgBanner");
            img.ImageUrl = Page.ResolveUrl("~/banners/" + Convert.ToString(DataBinder.Eval(dbr, "miniatura")));

            DataSet dados = ws.ConsultaBanner(Convert.ToInt32(lblCodigoBanner.Text),"");
            string codigo_cliente = dados.Tables[0].Rows[0]["codigo_cliente"].ToString();

            DataSet dadosT = ws.ConsultaAnuncioCliente(Convert.ToInt32(codigo_cliente));
            HyperLink lnkPub = (HyperLink)e.Item.FindControl("lnkPublicidadeGrande");
            lnkPub.NavigateUrl = "http://"+ dadosT.Tables[0].Rows[0]["site"].ToString();

            Label lblTitBanner = (Label)e.Item.FindControl("lblModalBannerAnuncio");
            lblTitBanner.Text = dadosT.Tables[0].Rows[0]["nome_fantasia"].ToString(); ;

            Image imgG = (Image)e.Item.FindControl("imgBannerPublicidadeGrande");
            imgG.ImageUrl = Page.ResolveUrl("~/banners/" + dados.Tables[0].Rows[0]["descricao"].ToString());
        }

    }

    protected void lnk_lnkCategoria_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        GridViewRow gv = (GridViewRow)(link.Parent.Parent);
        //DataListItem dv = (DataListItem)(link.Parent.Parent);

        LinkButton lnkCategoria = (LinkButton)gv.FindControl("lnkCategoria");
        
        Session["tipo_categoria"] = lnkCategoria.CommandArgument;
        Session["menu"] = "0";

        UrlRewrite urlr = new UrlRewrite();
        lnkCategoria.Text = urlr.RemoveSpecialCharacters(lnkCategoria.Text);
        
        Response.Redirect("~/Categoria/" + lnkCategoria.Text);
    }

    protected void lnk_lnkCategoriaSup_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        DataListItem dv = (DataListItem)(link.Parent);

        LinkButton lnkCategoria = (LinkButton)dv.FindControl("lnkCategoria");

        Session["tipo_categoria"] = lnkCategoria.CommandArgument;
        Session["menu"] = "0";

        UrlRewrite urlr = new UrlRewrite();
        lnkCategoria.Text = urlr.RemoveSpecialCharacters(lnkCategoria.Text);

        Response.Redirect("~/Categoria/" + lnkCategoria.Text);
    }

    private int GeraIndice(Int32 valor)
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        int index = rnd.Next(valor);
        return index;
    }

    private void GerMenu(GridView grd, string op)
    {
        DataSet dados = ws.montamenu(op);
        grd.DataSource = dados;
        grd.DataBind(); 
    }

    private void GerMenuSup(DataList grd, string op)
    {
        DataSet dados = ws.montamenu(op);
        grd.DataSource = dados;
        grd.DataBind();
    }

    private void recarregaLayout()
    {
        CssSite.Attributes["href"] = Page.ResolveUrl("~/Styles.css");
        //tabCss.Attributes["href"] = Page.ResolveUrl("~/visoft__tab_xpie7.css");
      //  imgLocaisFestas.Attributes["src"] = Page.ResolveUrl("~/imagens/locais_festas.png");
      //  imgDecoracao.ImageUrl = Page.ResolveUrl("~/imagens/decor_festas.png");
      //  imgAlimentacao.ImageUrl = Page.ResolveUrl("~/imagens/alimentacao_festas.png");
      //  imgDiversao.ImageUrl = Page.ResolveUrl("~/imagens/diversao_festas.png");
      //  imgServicos.ImageUrl = Page.ResolveUrl("~/imagens/servicos_festas.png");
      //  imgTipoFesta.ImageUrl = Page.ResolveUrl("~/imagens/tipo_festa.png");
        //imgLogo.ImageUrl = Page.ResolveUrl("~/imagens/logo.png");
        //btnBusca.ImageUrl = Page.ResolveUrl("~/imagens/ok.png");
        //btnClubOfertas.ImageUrl = Page.ResolveUrl("~/imagens/BT_GUIA.png");
        //btnVitrineTema.ImageUrl = Page.ResolveUrl("~/imagens/BT_TEMA.png");
        //btnVitrineConvite.ImageUrl = Page.ResolveUrl("~/imagens/BT_CONVITE.png");
        //btnOrcamento.ImageUrl = Page.ResolveUrl("~/imagens/BT_ORCAMENTO.png");
       // medidor2.ImageUrl = Page.ResolveUrl("~/imagens/medidor_verde.png");
        //imgPlayDir.ImageUrl = Page.ResolveUrl("~/imagens/play_dir.png");
        //imgPlayEsq.ImageUrl = Page.ResolveUrl("~/imagens/play_esq.png");
       // CssSite2.Attributes["href"] = Page.ResolveUrl("~/css/bootstrap.css");
      //  CssSite3.Attributes["href"] = Page.ResolveUrl("~/css/font-awesome.css");
      //  CssSite4.Attributes["href"] = Page.ResolveUrl("~/css/docs.css");
      //  CssSite5.Attributes["href"] = Page.ResolveUrl("~/css/bootstrap-social.css");
      //  CssSite6.Attributes["href"] = Page.ResolveUrl("~/css/bootstrap.min.css");
      //  CssSite7.Attributes["href"] = Page.ResolveUrl("~/css/bootstrap-responsive.css");
      //  CssSite8.Attributes["href"] = Page.ResolveUrl("~/css/font-awesome-animation.min.css");
        CssSite5.Attributes["href"] = Page.ResolveUrl("~/css/bootstrap-social.css");
//        CssSite9.Attributes["href"] = Page.ResolveUrl("~/css/font-awesome.css");


        imgLogo.Attributes["src"] = Page.ResolveUrl("~/imagens/logo.png");
        //menucore.Attributes["href"] = Page.ResolveUrl("~/css/sm-core-css.css");
        //menumint.Attributes["href"] = Page.ResolveUrl("~/css/sm-mint.css");
        

        
        
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }

    protected void dlPublicidade_ItemCommand(object source, DataListCommandEventArgs e)
    {      

        if (e.CommandName == "MostraBanner")
        {
            //ifBanner.Attributes["src"] = Page.ResolveUrl("~/mapa.aspx?endereco=" + endereco + "&t=" + lblNomeFantasia.Text);
//            Label lblCodigoBanner = (Label)e.Item.FindControl("lblCodigoBanner");

//            DataSet dados = ws.ConsultaBanner(Convert.ToInt32(lblCodigoBanner.Text),"");
//            string codigo_cliente = dados.Tables[0].Rows[0]["codigo_cliente"].ToString();

//            DataSet dadosT = ws.ConsultaAnuncioCliente(Convert.ToInt32(codigo_cliente));
//            lnkPublicidadeGrande.NavigateUrl = "http://"+ dadosT.Tables[0].Rows[0]["site"].ToString();


           // imgBannerPublicidadeGrande.ImageUrl = Page.ResolveUrl("~/banners/" + dados.Tables[0].Rows[0]["descricao"].ToString());
           // AjaxControlToolkit.ModalPopupExtender mp = (AjaxControlToolkit.ModalPopupExtender)e.Item.FindControl("btnBanner_ModalPopupExtender");
           // mp.Show();
            //upBanner.Update();
        }
    }

    protected void dlVideo_ItemCommand(object source, DataListCommandEventArgs e)
    {

        if (e.CommandName == "MostraVideo")
        {

            WebControl wc = ((WebControl)e.CommandSource);
            DataListItem row = ((DataListItem)wc.NamingContainer);

            //LinkButton lkbItem = ((LinkButton)dlVideo.Items[row.ItemIndex].FindControl("aBanner"));
            //Session["idVideo"] = lkbItem.CommandArgument;
            //Response.Redirect("tvOnline.aspx");

        }
    }

    protected void dlVideo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView dbr = (DataRowView)e.Item.DataItem;

        if (Convert.ToString(DataBinder.Eval(dbr, "miniatura")) != "")
        {
            string imagem = Page.ResolveUrl("~/video/" + Convert.ToString(DataBinder.Eval(dbr, "miniatura")) );
            Image imgVideo = (Image)e.Item.FindControl("imgVideo");
            imgVideo.ImageUrl = imagem;
        }

    }

    protected string montaVisualizadorG(string nomeArquivo)
    {
        string tipo_arquivo = nomeArquivo.Substring(nomeArquivo.Length - 3, 3);
        string player = "";

        switch (tipo_arquivo.ToUpper())
        {
            case "JPG":
                {
                    string imagePath = MapPath("~/album/") + nomeArquivo;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
                    player = "<img id='imgFotoG' src='album/" + nomeArquivo + "' style='border-width:0px;'>";
                    if (img.Height > 450)
                    {
                        player = "<img id='imgFotoG' src='album/" + nomeArquivo + "' style='height:450px;border-width:0px;'>";
                    }
                    if (img.Height > 600)
                    {
                        player = "<img id='imgFotoG' src='album/" + nomeArquivo + "' style='height:650px;border-width:0px;'>";
                    }
                    if (img.Height < 450)
                    {
                        player = "<img id='imgFotoG' src='album/" + nomeArquivo + "' style='height:100%;border-width:0px;'>";
                    }
                    img.Dispose();
                    break;
                }

            case "WMV":
                {
                    player = "<asp:MediaPlayer ID='mp1' runat='server' MediaSource='album/" + nomeArquivo + "' /> ";
                    break;
                }

            case "MP4":
                {
                    player = "<iframe ID='ifVideo' runat='server' align='middle' frameborder='0' " +
                             "height='480px' marginheight='0' src='video.aspx?a=" + nomeArquivo + "' marginwidth='0' scrolling='auto' " +
                             "width='640px'>" +
                             "</iframe>";

                    break;
                }
        }

        if (nomeArquivo.Substring(0, 4).ToUpper() == "HTTP")
        {
            player = " <iframe ID='ifVideo' runat='server' align='middle' frameborder='0' " +
                     "height='480px' marginheight='0' src='video.aspx?a=" + nomeArquivo + "' marginwidth='0' scrolling='auto' " +
                     "width='640px'>" +
                     "</iframe>";

        }

        return player;

    }


    private void MostraVideo()
    {
        //DataSet dados = ws.ConsultaVideo(0, "0");

        //pds.DataSource = dados.Tables[0].DefaultView;
        //pds.AllowPaging = true;
        //pds.PageSize = 4;
        //pds.CurrentPageIndex = CurrentPage;
      //  lnkProximo.Enabled = !pds.IsLastPage;
      //  lnkAnterior.Enabled = !pds.IsFirstPage;

        //dlVideo.DataSource = pds;
        //dlVideo.DataBind();
    }

    public int CurrentPage
    {
        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }
    }

    protected void lnkAnterior_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        MostraVideo();
    }

    protected void lnkProximo_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        MostraVideo();
    }


    protected void btnVitrineTema_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/tema.aspx");
    }

    protected void btnVitrineConvite_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/convite.aspx");
    }

    protected void btnCloseVideo_Click(object sender, EventArgs e)
    {
        MostraVideo();
    }

    protected void lkbHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }

    protected void btnClubOfertas_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    protected void btnBusca_Click1(object sender, EventArgs e)
    {

        Session["busca"] = txtBusca.Text;
        Response.Redirect("categoria.aspx");
    }
}
