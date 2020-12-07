<%@ Control Language="VB" AutoEventWireup="false" CodeFile="menu.ascx.vb" Inherits="WebUserControl" %>
<link rel="stylesheet" href="Menu3.css" type="text/css" />
<div id="header" align="center" >
<table width="100%" cellpadding ="0" cellspacing ="0" align="center">

<tr>
<td  align="center">


    <asp:Menu ID="Menu1" runat="server" Orientation ="Horizontal" CssClass ="Menu" Font-Names="Verdana" ForeColor ="White" Width="800px"> 
    <StaticMenuItemStyle Height ="40px"/>
    <DynamicMenuItemStyle CssClass ="Menu" Height="40px" HorizontalPadding="25px"  />
    <dynamichoverstyle  CssClass ="menuhover" />
    <StaticHoverStyle CssClass ="menuhover" />
          
    <Items>
    <asp:MenuItem Text="My Menu">
    <asp:MenuItem Text ="My profile" Enabled ="true" ></asp:MenuItem>
    <asp:MenuItem Text="my Forum" Enabled ="true"></asp:MenuItem>
         
    </asp:MenuItem>
    <asp:MenuItem Text="Classfields" >
    <asp:MenuItem Text="car" Enabled ="true" ></asp:MenuItem>
    <asp:MenuItem Text="Item For sale" Enabled ="true" ></asp:MenuItem>    
    
    </asp:MenuItem>
    <asp:MenuItem Text="Forum">
    <asp:MenuItem Text="Create a Forum">
        </asp:MenuItem>
        
    </asp:MenuItem>
    
    <asp:MenuItem Text="Answers">
    <asp:MenuItem Text="post Answer">
    <asp:MenuItem Text="Ask Question"></asp:MenuItem>
    <asp:MenuItem Text="FAQ"></asp:MenuItem>
    </asp:MenuItem>
    </asp:MenuItem>
    
    <asp:MenuItem Text="Guide">
    <asp:MenuItem Text="Contact us"></asp:MenuItem>
    </asp:MenuItem>
    <asp:MenuItem Text ="Help" >
    <asp:MenuItem Text="About Us" Enabled ="true"></asp:MenuItem> 
        </asp:MenuItem>
    </Items>
    </asp:Menu>

</td>
</tr>



</table>






</div>