<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Default.master" CodeFile="categoria.aspx.cs" Inherits="categoria" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">  
    <!-- Inclua esta tag na seção head ou logo antes da tag de fechamento da seção body -->
<script type="text/javascript" src="https://apis.google.com/js/plusone.js">
    { lang: 'pt-BR' }
</script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1"  runat="server">
    </asp:ScriptManagerProxy>   

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
    
<div class="container-fluid">          

<ul class="list-group">
  <li class="list-group-item">
    <span class="badge"><asp:Label ID="lblTituloCat" runat="server" ></asp:Label></span>
    Categoria Selecionada
  </li>
</ul>
        

<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
<ContentTemplate>

<div class="dropdown">
    <ul class="list-group">
      <li class="list-group-item">

     <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        Ordenar por:
        <span class="caret"></span>
      </button>
      <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
        <li><asp:LinkButton ID="lnkAnuncio" runat="server" OnClick="lnkAnuncio_Click">Anuncio</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkBairro" runat="server" OnClick="lnkBairro_Click">Bairro</asp:LinkButton></li>
      </ul>

      </li>
    </ul>

</div>
                    
</ContentTemplate>
</asp:UpdatePanel>

                    
        

<div id="dvInfo" runat="server" class="alert alert-info" role="alert">
  <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
  <span class="sr-only">Informação:</span>
  Sem anuncios para esta categoria
</div>


    <asp:DataList ID="dlAnunciante" runat="server" ShowFooter="False" Width="100%"
        ShowHeader="False" onitemdatabound="dlAnunciante_ItemDataBound" 
        BorderWidth="0px" CellPadding="0" onitemcommand="dlAnunciante_ItemCommand">
        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" 
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"  />
        <ItemTemplate>            
            <div class="panel panel-default" >
               <div class="panel-heading">
                      <i class="fa fa-arrow-circle-right"></i> 
                      <asp:Label ID="lblNomeFantasia" runat="server" Text=""></asp:Label>
               </div>
                  <div class="panel-body">                                               
                     <div class="row">
                        <div class="container-fluid">
                        <div class="col-md-2" >
    		               <a id="dLabel" href="#" role="button" aria-expanded="false" data-toggle="modal" data-target='#myModal<%# DataBinder.Eval(Container.DataItem, "codigo") %>'>
				              <asp:Image border="0" ID="btnImgLogo"  class="img-responsive" runat="server" Width="136px" Height="96px"  />                        
				           </a>
                       </div>
                       <div class="col-md-6" style="vertical-align:middle;">
                          <h6>
       	                      <a href='tel:+<%# DataBinder.Eval(Container.DataItem, "telefone") %>' style="text-decoration: none;color:gray;" >
                                <span class="fa-stack fa-lg">
                                  <i class="fa fa-circle fa-stack-2x"></i>
                                  <i class="fa fa-phone fa-stack-1x fa-inverse"></i>
                                </span>
                                <%# DataBinder.Eval(Container.DataItem, "telefone") %> 
       	                      </a>
                          </h6>
                       </div>

                       </div>

                  <div class="container-fluid">

                                <p><asp:Label ID="lblCodigoAnuncio" runat="server" Text="" Visible="false"></asp:Label></p>
                                <p style="text-decoration: none;color:gray;">
                                    <span class="fa-stack fa-lg">
                                      <i class="fa fa-circle fa-stack-2x"></i>
                                      <i class="fa fa-circle-thin fa-stack-1x fa-inverse"></i>
                                    </span>
                                    <asp:Label ID="lblDescricao" runat="server" Text=""></asp:Label>

                                </p>
                                <p style="text-decoration: none;color:gray;">
                                   <span class="fa-stack fa-lg">
                                      <i class="fa fa-circle fa-stack-2x"></i>
                                      <i class="fa fa-map-marker fa-stack-1x fa-inverse"></i>
                                    </span>                                       
                                   <asp:Label ID="lblEndereco" runat="server" Text=""></asp:Label>
                                   <asp:Label ID="lblBairroCidade" runat="server" Text=""></asp:Label>

                                </p>
				                <p></p>
                                <p>
                                   <asp:HyperLink ID="aEmail" runat="server" style="text-decoration: none;color:gray;">
                                      <span class="fa-stack fa-lg">
                                         <i class="fa fa-circle fa-stack-2x"></i>
                                         <i class="fa fa-envelope-o fa-stack-1x fa-inverse"></i>
                                      </span>                       
                                       E-mail
                                   </asp:HyperLink>

                                </p>
                
                                <p>
                                   <a href='http://maps.google.com/maps?q=<%# DataBinder.Eval(Container.DataItem, "endereco") %>+<%# DataBinder.Eval(Container.DataItem, "bairro") %>+<%# DataBinder.Eval(Container.DataItem, "cidade") %>' target="_blank" style="text-decoration: none;color:gray;">
                                    <span class="fa-stack fa-lg">
                                      <i class="fa fa-circle fa-stack-2x"></i>
                                      <i class="fa fa-flag fa-stack-1x fa-inverse"></i>
                                    </span> Ver Mapa
                                   </a>
                                </p>

                                <p>
                                   <a href='http://<%# DataBinder.Eval(Container.DataItem, "site") %>' target="_blank" style="text-decoration: none;color:gray;">
                                      <span class="fa-stack fa-lg">
                                         <i class="fa fa-circle fa-stack-2x"></i>
                                         <i class="fa fa-globe fa-stack-1x fa-inverse"></i>
                                      </span>
                                       Site
                                   </a>

                                </p>
               
               

                  </div>

                   </div>

               <h6 >
                  <a role="button" data-toggle="collapse" data-parent="#accordion" href='#collapse<%# DataBinder.Eval(Container.DataItem, "codigo") %>' aria-expanded="false" aria-controls='collapse<%# DataBinder.Eval(Container.DataItem, "codigo") %>' style="text-decoration: none;color:gray;">
                     <p>
                        <span class="fa-stack fa-lg">
                           <i class="fa fa-circle fa-stack-2x"></i>
                           <i class="fa fa-circle-thin fa-stack-1x fa-inverse"></i>
                        </span>
                        <asp:Label ID="Label1" runat="server" Text="Ver Detalhes "></asp:Label>
                     </p>                      
                  </a>
               </h6>

                <div class="collapse" id='collapse<%# DataBinder.Eval(Container.DataItem, "codigo") %>'>
                                <p>
                                   <asp:Label ID="lblInformacoes" runat="server" Text=""></asp:Label>
                                </p>              

                </div>


                                <asp:HyperLink ID="aFacebook" CssClass="" runat="server" Target="_blank" > 
                                   <i class="fa fa-facebook"></i>
                                </asp:HyperLink>
                                <asp:HyperLink ID="aInsta" CssClass="" runat="server" Target="_blank"> 
                                   <i class="fa fa-instagram"></i>
                                </asp:HyperLink>
                                <asp:HyperLink ID="aTwitter" CssClass="" runat="server" Target="_blank"> 
                                   <i class="fa fa-twitter"></i>
                                </asp:HyperLink>
                                <asp:HyperLink ID="aGmais"  CssClass="" runat="server" Target="_blank"> 
                                   <i class="fa fa-google-plus"></i>
                                </asp:HyperLink>



               </div>                
            </div>
            




	
        </ItemTemplate>
    </asp:DataList>
 
   

    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
           <div class="overlay" />
            <div class="overlayContent">
                <h2>Aguarde...</h2>
                <asp:Image CssClass="aguarde" ID="imgAguarde" ImageUrl='<% Page.ResolveUrl("~/imagens/wait.gif") %>' runat="server" />
            </div>
            
        </ProgressTemplate>
    </asp:UpdateProgress>



</div>

</asp:Content>

