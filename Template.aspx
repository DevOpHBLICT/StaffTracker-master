<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewState="true" CodeFile="Template.aspx.cs" Inherits="_Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
    
  <script type="text/javascript">
      function() {
          $(".target-active").find("[href='/']").parent().addClass("active");
      });
  </script>


   <script type="text/javascript">
       function onlyNumbersWithDot(e) {
           var charCode;
           if (e.keyCode > 0) {
               charCode = e.which || e.keyCode;
           }
           else if (typeof (e.charCode) != "undefined") {
               charCode = e.which || e.keyCode;
           }
           if (charCode == 46)
               return true
           if (charCode > 31 && (charCode < 48 || charCode > 57))
               return false;
           return true;
       }
    </script>


     <div class="container"> 
         
       <div class="row">
           <div class="form-group">
              <div class="col-sm-6">
             <asp:Label ID="Error_Label" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                  </div>

        </div>
       <div class="row">
           <div class="form-group">
              <div class="col-sm-6"> 
                   <strong>
                         Create an Template for :       <asp:Label  ID="Name" runat="server" BorderStyle="None" ></asp:Label>
                     </strong>
              </div>
            
           </div>
       </div>
         <br />
  <div class="row">
           <div class="form-group">
           
              <div class="col-sm-6"> 
                  Date:<asp:TextBox class="form-control" AutoCompleteType="Disabled" runat="server" autocomplete="false" id="Entry_Date" Autopostback="true"  ontextchanged="TextBox1_TextChanged" />
                       <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="Entry_Date" errormessage="Please enter date!" />
              </div>
           </div>
       </div>
         <div class="row">
             <div class="form-group">
              <div class="col-sm-6"> 
              
                  Directorate<asp:DropDownList onselectedindexchanged="DirectorateSelected" ID="Directorate"  autocomplete="false" AutoPostBack="True"   class="form-control" runat="server"  AppendDataBoundItems="True"  
   >
                   <asp:ListItem Text="--Please Select--" Value="" />   </asp:DropDownList>

              
                  </div>
                 <div class="col-sm-6"> 
                            
   <br />

                   
                  </div>
                 </div>
         </div>
         <br />
            <div class="row">
             <div class="form-group">
              <div class="col-sm-6"> 
            Cost Centre:<asp:DropDownList  ID="CostCentre" class="form-control"  autocomplete="false"  runat="server"  AppendDataBoundItems="True" AutoPostBack="True"   OnSelectedIndexChanged="CostCentre_SelectedIndexChanged"  
   ><asp:ListItem Text="--Please Select--" Value="" />    </asp:DropDownList>
            <asp:SqlDataSource ID="CostCenterConnect" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [CostCentre]"></asp:SqlDataSource>
     
                        
                  </div>
                   <div class="col-sm-6"> 
                     
Sub Code:<asp:DropDownList  ID="SubCostCode" class="form-control"  autocomplete="false"  runat="server"  AppendDataBoundItems="False" AutoPostBack="True"   OnSelectedIndexChanged="SubCostCentre_SelectedIndexChanged"  
   ><asp:ListItem Text="--Please Select--" Value="" />    </asp:DropDownList>
        




                       <asp:SqlDataSource ID="Status1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Status]"></asp:SqlDataSource>
                       <br />
                        
                  </div>
                 </div>
         </div>
         <br />
         <div class="row">
            <div class="form-group">
               <div class="col-sm-6">
                   
               </div>
                
                 <div class="col-sm-6">
                    
               </div>
            </div>
         <div class="row">
             <div class="form-group">
                 <div class="col-sm-6" >
                 </div>
                  <div class="col-sm-6">
                   <div>  <asp:Button style="color:white;Background-Color: #005EB8" ID="Button2" runat="server"  Width="100px" OnClick="Button1_Click" CssClass="btn btn-success" Text="Confirm"    /></div>
                     <br />
                    <div>  <asp:Button ID="Cancel" runat="server"  OnClick="Cancel_Click" CssClass="btn btn-danger" Width="100px" Text="Cancel"   /></div>
                 </div>
             </div>
         </div>
           <br />
 <div>

     




           <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"    >
               <AlternatingRowStyle BackColor="White" />
              
              
                      <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#0072CE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
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

          
           </div>
      </div>
    
  
    </div>   


                   

  
 
  



  <script src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>




      <script type="text/javascript">

         


          $(document).ready(function () {
              $("[id*=Entry_Date]").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0' });



          });
    </script>



    </div>
</asp:Content>
