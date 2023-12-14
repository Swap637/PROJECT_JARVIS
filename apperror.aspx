<%@ Page Language="VB" MasterPageFile="~/MasterPages/Base.master" AutoEventWireup="false"
    CodeFile="apperror.aspx.vb" Inherits="apperror" Title="Application Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p style="color: red; font-weight: bold">
        An error was occured in application. Please contact your System Administator.
    </p>
    <div class="scrollableDiv" style="width: 700px; color: red; border-style: solid;
        border-width: 1px;">
        <table>
            <tr>
                <td>
                    <asp:Literal ID="uxError" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
