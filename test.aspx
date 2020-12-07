<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-sm-6"> 
            Cost Centre:<asp:DropDownList  ID="CostCentre" class="form-control"  autocomplete="false"  runat="server"  AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="CostCenterConnect" DataTextField="CostCentreName" DataValueField="CostCentreCode" OnSelectedIndexChanged="CostCentre_SelectedIndexChanged"  
   ><asp:ListItem Text="--Please Select--" Value="" />    </asp:DropDownList>
            <asp:SqlDataSource ID="CostCenterConnect" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [CostCentre]"></asp:SqlDataSource>
            </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="CostCentre" HeaderText="CostCentre" SortExpression="CostCentre" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="SMT" HeaderText="SMT" SortExpression="SMT" />
                 
                <asp:TemplateField HeaderText="Status">
                     <ItemTemplate>
                        
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>

                 
            </Columns>
        </asp:GridView>

             



           <asp:SqlDataSource ID="staff" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Staff]"></asp:SqlDataSource>

             



           </asp:SqlDataSource>

          <div>  <asp:Button style="color:white;Background-Color: #005EB8" ID="Button2" runat="server"  Width="100px" OnClick="Button1_Click" CssClass="btn btn-success" Text="Confirm"    /></div>
                     <br />
                    <div>  <asp:Button ID="Cancel" runat="server"  OnClick="Cancel_Click" CssClass="btn btn-danger" Width="100px" Text="Cancel"   /></div>
            
      </form>
</body>
</html>
