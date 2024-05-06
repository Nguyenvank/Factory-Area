<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wip-scan-in.aspx.cs" Inherits="InventoryWeb.wip_scan_in" %>
  <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCAN IN W.I.P</title>
    <link href="css/style.css" rel="stylesheet" />
    <style>
        body, input {font-size:14pt}
        input, label {vertical-align:middle}
        .qrcode-text {padding-right:1.7em; margin-right:0}
        .qrcode-text-btn {display:inline-block; background:url(//dab1nmslvvntp.cloudfront.net/wp-content/uploads/2017/07/1499401426qr_icon.svg) 50% 50% no-repeat; height:1em; width:1.7em; margin-left:-1.7em; cursor:pointer}
        .qrcode-text-btn > input[type=file] {position:absolute; overflow:hidden; width:1px; height:1px; opacity:0}
    </style>
    <script language="javascript">
        function openQRCamera(node) {
            var reader = new FileReader();
            reader.onload = function () {
                node.value = "";
                qrcode.callback = function (res) {
                    if (res instanceof Error) {
                        alert("No QR code found. Please make sure the QR code is within the camera's frame and try again.");
                    } else {
                        node.parentNode.previousElementSibling.value = res;
                    }
                };
                qrcode.decode(reader.result);
            };
            reader.readAsDataURL(node.files[0]);
        }

        function showQRIntro() {
            return confirm("Use your camera to take a picture of a QR code.");
        }
    </script>
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
    <h1>
        SCAN IN W.I.P
    </h1>
    </div>
    <div class="row">
        <div class="col-3 menu">
          <ul>
            <li><a href="./">&rsaquo; Home</a></li>
          </ul>
        </div>

        <div class="col-6">
          <h3>Partname:</h3>
          <p><asp:DropDownList ID="ddlPartname" runat="server" Width="100%" Height="50" AutoPostBack="true" Font-Size="Larger" OnSelectedIndexChanged="ddlPartname_SelectedIndexChanged"></asp:DropDownList></p>

          <h3>Partcode:</h3>
          <p><asp:Label ID="lblPartcode" runat="server" Text="N/A" Width="100%" Font-Size="Larger"></asp:Label></p>

          <h3>Location:</h3>
          <p><asp:Label ID="lblLocation" runat="server" Text="N/A" Font-Size="Larger"></asp:Label></p>

          <h3>LOT number:</h3>
          <p><asp:Label ID="lblLOT" runat="server" Text="N/A" Font-Size="Larger"></asp:Label></p>

          <h3>Packing:</h3>
          <p>
            <table style="width: 100%; border:thin solid gray;">
                <tr style="height: 30px;">
                    <td style="width:25%; text-align:center;"><h3>PCS</h3></td>
                    <td style="width:25%; text-align:center;"><h3>BOX</h3></td>
                    <td style="width:25%; text-align:center;"><h3>CAR</h3></td>
                    <td style="width:25%; text-align:center;"><h3>PAL</h3></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width:25%; text-align:center;">
                        <h1><asp:Label ID="lblPCS" runat="server" Text="0"></asp:Label></h1>
                    </td>
                    <td style="width:25%; text-align:center;">
                        <h1><asp:Label ID="lblBOX" runat="server" Text="0"></asp:Label></h1>
                    </td>
                    <td style="width:25%; text-align:center;">
                        <h1><asp:Label ID="lblCAR" runat="server" Text="0"></asp:Label></h1>
                    </td>
                    <td style="width:25%; text-align:center;">
                        <h1><asp:Label ID="lblPAL" runat="server" Text="0"></asp:Label></h1>
                    </td>
                </tr>
            </table>
          </p>

          <h3>Packing code:</h3>
          <p><asp:FileUpload ID="flePackcode" runat="server" /></p>
            <p>
                <input type=text size=16 placeholder="Tracking Code" class=qrcode-text>
                <label class=qrcode-text-btn><input type=file accept="image/*" capture=environment onclick="return showQRIntro();" onchange="openQRCamera(this);" tabindex=-1></label> 
                <input type=button value="Go" disabled>

            </p>

          <p>&nbsp;</p>
          <div style="text-align: right;"><asp:Button ID="btnSave" runat="server" Text="Save" Width="150" Height="50" onclick="btnSave_Click" /></div>
        </div>

    </div>

    </form>
</body>
</html>
