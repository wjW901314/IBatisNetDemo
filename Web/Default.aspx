<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Service.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IBatisNet演示</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllPerson" 
            TypeName="Service.BLL.PersonService" DataObjectTypeName="Service.Model.Person" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAllPerson2" 
            TypeName="Service.BLL.PersonService" DataObjectTypeName="Service.Model.Person" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>

        <br />
        GridView展示数据
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataSourceID="ObjectDataSource1" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                 <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                 <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                 <asp:BoundField DataField="WeightInKilograms" HeaderText="WeightInKilograms" SortExpression="WeightInKilograms" />
                 <asp:BoundField DataField="HeightInMeters" HeaderText="HeightInMeters" SortExpression="HeightInMeters" />
                <asp:BoundField DataField="BirthDate"  HeaderText="BirthDate" SortExpression="BirthDate" />
                <asp:TemplateField HeaderText="Select">
     <ItemTemplate>
       <asp:LinkButton ID="LinkButton1" 
         CommandArgument='<%# Eval("Id") %>' 
         CommandName="Delete" runat="server">
         删除</asp:LinkButton>
     </ItemTemplate>
   </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        DetailsView展示数据
        
        <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" AutoGenerateRows="False"
            DataSourceID="ObjectDataSource2" Height="50px" Width="125px">
            <Fields>
                 <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
               <asp:BoundField DataField="WeightInKilograms" HeaderText="WeightInKilograms" SortExpression="WeightInKilograms" />
                <asp:BoundField DataField="HeightInMeters" HeaderText="HeightInMeters" SortExpression="HeightInMeters" />
                <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" SortExpression="BirthDate" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
    </form>
</body>
</html>
