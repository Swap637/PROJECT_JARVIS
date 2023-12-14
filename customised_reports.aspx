<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/Base.master" AutoEventWireup="false"
    CodeFile="customised_reports.aspx.vb" Inherits="customised_reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 content">
        <div class="col-md-12 box">
            <div class="row  h-cont">
                <div class="col-xs-12">
                    <asp:Label ID="uxReportTitle" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="row  h-cont">
                <div class="c-fixed-height-table table-responsive">
                    <asp:GridView ID="uxParamList" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered c-data-table"
                        HeaderStyle-CssClass="gridhead">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Param Name">
                                <ItemTemplate>
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Bind("Caption") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Param Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="uxParamValue" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="uxParamName" runat="server"
                                        Value='<%# Bind("ParamName") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </div>
            <div class="row  h-cont">
                <div class="col-xs-12">
                    <asp:Button ID="uxDisplay" runat="server" Text="Display" CssClass="btn c-btn" />
                </div>
            </div>
        </div>
    </div>

    <%--<div style="width:800px">

   <br />
   <asp:Label ID="uxReportTitle" runat="server" Font-Bold="true"></asp:Label>
   <br />
   <br />
    <asp:GridView ID="uxParamList" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="Param Name">
                <ItemTemplate>
                    <asp:Literal ID="Literal1" runat="server" Text='<%# Bind("Caption") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Param Value">
                <ItemTemplate>
                    <asp:TextBox ID="uxParamValue" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="uxParamName" runat="server"
                    Value='<%# Bind("ParamName") %>'></asp:HiddenField>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:Button ID="uxDisplay" runat="server" Text="Display" />
    </div>
    <br />  --%>
</asp:Content>
