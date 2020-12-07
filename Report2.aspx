<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewState="true" CodeFile="Report2.aspx.cs" Inherits="_Default" %>



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

  <br />
    <asp:Button ID="btntoExcel" runat="server" Text="Export to Excel" onclick="btntoExcel_Click" />
    <div class="row">
           <div class="form-group">
           
              <div class="col-sm-6"> 
                  Date:<asp:TextBox class="form-control" AutoCompleteType="Disabled" runat="server" autocomplete="false" id="Entry_Date" Autopostback="true"  ontextchanged="TextBox1_TextChanged"/>
                       <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="Entry_Date" errormessage="Please enter date!" />
              </div>
           </div>
       </div>
     

           <asp:GridView ID="GridView1" runat="server" OnRowDataBound="grv_RowDataBound" CellPadding="4"  ForeColor="#333333" GridLines="None" Width="100%"    >
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

           </asp:GridView>

          
     
                   

  
 
  



  <script src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>




      <script type="text/javascript">

         


          $(document).ready(function () {
              $("[id*=Entry_Date]").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0' });



          });
    </script>



    </div>
</asp:Content>
