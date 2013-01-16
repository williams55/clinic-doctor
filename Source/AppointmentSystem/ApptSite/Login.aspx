<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <!-- stylesheets -->
    <link rel="stylesheet" type="text/css" href="resources/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="resources/css/style.css" media="screen" />
    <link id="color" rel="stylesheet" type="text/css" href="resources/css/colors/blue.css" />
    <!-- scripts (jquery) -->

    <script src="resources/scripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="resources/scripts/jquery-ui-1.8.custom.min.js" type="text/javascript"></script>

    <script src="resources/scripts/smooth.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("form").keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();
                    $("input:submit").click();
                }
            });
            $("input:submit, input:reset").button();
        });
    </script>

</head>
<body>
    <div id="login">
        <!-- login -->
        <div class="title">
            <h5>
                Login</h5>
            <div class="corner tl">
            </div>
            <div class="corner tr">
            </div>
        </div>
        <div class="messages">
            <div id="messageError" class="message message-error" runat="server" visible="False">
                <div class="image">
                    <img src="resources/images/icons/error.png" alt="Error" height="32" />
                </div>
                <div class="text">
                    <h6>
                        Error</h6>
                    <span>Invalid Username or Password.</span>
                </div>
            </div>
        </div>
        <div class="inner">
            <form action="" method="POST" runat="server">
            <div class="form">
                <!-- fields -->
                <div class="fields">
                    <div class="field">
                        <div class="label" style="width: 150px;">
                            <label for="username">
                                Username</label>
                        </div>
                        <div class="input" style="width: 120px;">
                            <dx:ASPxTextBox runat="server" ID="txtUsn" MaxLength="50" TabIndex="1" Text="" CssClass="text-form"
                                Width="100px">
                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                    ErrorText="Error">
                                    <RequiredField IsRequired="True" ErrorText="Username is required" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="label" style="width: 150px;">
                            <label for="password">
                                Password</label>
                        </div>
                        <div class="input" style="width: 120px;">
                            <dx:ASPxTextBox runat="server" ID="txtPsw" MaxLength="50" TabIndex="2" Text="" CssClass="text-form"
                                Width="100px" Password="True">
                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                    ErrorText="Error">
                                    <RequiredField IsRequired="True" ErrorText="Password is required" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="checkbox">
                        </div>
                    </div>
                    <div class="buttons">
                        <dx:ASPxButton ID="btnNative" runat="server" EnableTheming="False" Native="True"
                            Width="80px" Text="Login" AutoPostBack="false" EnableViewState="False" OnClick="btnNative_OnClick">
                        </dx:ASPxButton>
                    </div>
                </div>
                <!-- end fields -->
            </div>
            </form>
        </div>
    </div>
</body>
</html>
