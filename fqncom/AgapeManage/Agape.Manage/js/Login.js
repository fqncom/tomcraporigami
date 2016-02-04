$(document).ready(function() {

    $("input").keypress(function(e) {

        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            OnLogin();
        }
    });
});

function OnLogin() {

    var LoginName = $("#txtLoginName").val();
    if (LoginName == "") {

        alert("请输入登录名！");
        return;
    }

    var LoginPassword = $("#txtLoginPassword").val();
    if (LoginPassword == "") {

        alert("请输入登录密码！");
        return;
    }

    $.post("BasicService.aspx",
            {
                "TransCode": "OperatorLogin",
                "LoginName": LoginName,
                "LoginPassword": LoginPassword
            },
            function(data) {

                var returnCode = $("ReturnCode", data).text();
                var returnMessage = $("ReturnMessage", data).text();

                if (returnCode != "0000") {
                    alert(returnMessage);
                    return;
                }

                window.location = "MainFrame.htm";
            });
}