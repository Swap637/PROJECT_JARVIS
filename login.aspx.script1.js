//PAGE TOP
localStorage.report_db_source = undefined;
localStorage.report_db_sys_status = undefined;
//
//<prevent multiple form submission>
var _is_form_submitted =0;
$('#aspnetForm').submit(function()
 {
	 if(_is_form_submitted==1)return false;
	 _is_form_submitted = 1;	
 });
//<prevent multiple form submission>
//<_open_new_win_module>
$(function () {
    var script_b64 = getAppPath() + "/js/lib/base64.js";
    $.getScript(script_b64, function () {
        var _open_new_win_module = {
            init: function () {
                if ($("#ctl00_uxMessageBoard").text() == "") {
                    var key = ("00" + (new Date()).getMonth().toString()).substr(-2) + ("00" + (new Date()).getHours().toString()).substr(-2);
                    var key_value = ("hash=" + btoa(key)).toLowerCase();
                    var url = window.location.href.toLowerCase();
                    if (url.indexOf(key_value) == -1) {
                        window.location = "index.html?rnd=" + Math.random().toString();
                    }
                }
            }
        };
        //_open_new_win_module.init();
    });
});
//</_open_new_win_module>
//<biometric authentication>
var _biometric_auth = {
    init: function () {
        //create button and its event
        $("#ctl00_ContentPlaceHolder1_uxChangePasswordlnk").after(
            "<br/><br/><br/><input id='ux_biometric_login' type='button' style='display:none' class='btn c-btn' value='Biometric Login' style=\"color:#284E98;background-color:White;border-color:#507CD1;border-width:1px;border-style:Solid;font-family:Verdana;font-size:0.9em;\"/>");
        $("body").append();
        //
        $("#ux_biometric_login").click(function () {
            $("body").append("<form action='fp/mfs100/fpui.aspx' method='post'>\
                    <div style='display:none'><input type='text' name='action' id='action' value='verify_existing'/>\
                    <input type='submit' name='submit' id='submit'/></div>\
                </form>");
            $("#submit").click();
        });
        //
        $.getScript("js/lib/json2.js", function () {
            $("#ctl00_ContentPlaceHolder1_Login1_LoginButton").click(function () {				
                var logintype = 0;
                _biometric_auth_model.check_login_type($("#ctl00_ContentPlaceHolder1_Login1_UserName").val()
                    , function (logintype_1) {
                        logintype = logintype_1;
                    });
                if (logintype == 2) {
                    $("body").append("<form action='fp/mfs100/fpui.aspx' method='post'>\
                        <input type='text' name='action' id='action' value='verify'  class='form-control'/>\
                        <input type='text' name='username' id='username'  class='form-control' value='" + $("#ctl00_ContentPlaceHolder1_Login1_UserName").val() + "'/>\
                        <input type='text' name='password' id='password'  class='form-control' value='" + $("#ctl00_ContentPlaceHolder1_Login1_Password").val() + "'/>\
                        <input type='text' name='rc4_rnd_key' id='rc4_rnd_key'  class='form-control' value='" + $("#ctl00_ContentPlaceHolder1_ux_rc4_rnd_key").val() + "'/>\
                        <input type='submit' name='submit' id='submit'  class='form-control'/>\
                    </form>");
                    $("#submit").click();
                    return false;
                }
            });
            //$("#ctl00_ContentPlaceHolder1_Login1_LoginButton").click(function () {
            //    var logintype = 0;
            //    _auth.check_login_type($("#ctl00_ContentPlaceHolder1_Login1_UserName").val()
            //        , function (logintype_1) {
            //            logintype = logintype_1;
            //        });
            //    if (logintype == 2) {
            //        alert("User allowed for only Biometric authentication.");
            //        unFreezeScreen();
            //        return false;
            //    }
            //});
        }
        );
    }
};
var _biometric_auth_model = {
    check_login_type: function (username, callback) {
        this.get_login_type(username
            , function (d) {
                if (d.table.rows.length == 0) {
                    callback(-1); return;
                }
                callback(parseInt(d.table.rows[0].logintype)); return;
            }
            , function (e) {
                alert(e);
                callback(-1); return;
            });
    },
    get_login_type: function (username, success, error) {
        _biometric_auth_ajax.webMethodV1({
            url: "ajax/common.aspx/execute"
            , parameters: {
                sp_name: "pr_a_getUserLoginType",
                sp_params: { "p_username": username }
                , db_options: { connection_string_name: "master" }
            }
            , async: false
            , success: function (d) { success(d); }
            , error: function (e) { error(e); }
        });
    }
};
var _biometric_auth_ajax = {
    webMethodV1: function (options) {
        var settings = $.extend({
            'url': '',
            'methodName': '',
            'async': false,//false,
            'cache': false,
            timeout: 10 * 60000,
            debug: false,
            'parameters': {},
            success: function (response) { },
            error: function (response) { }
        }, options); var parameterjson = "{}";
        var result = null;
        if (settings.url == '')
            settings.url = location.pathname + "/" + settings.methodName;
        log(settings.url);

        //if (settings.parameters != null) { parameterjson = $.toJSON(settings.parameters); }
        //parameterjson = "{ 'client': '3' }";//JSON.stringify(settings.parameters);
        log(options);
        parameterjson = JSON.stringify(settings.parameters);
        $.ajax({
            type: "POST",
            url: settings.url,//location.pathname + "/" + settings.methodName,
            data: parameterjson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: settings.async,
            cache: settings.cache,
            timeout: settings.timeout,
            success: function (value) {
                log(value);
                //result = value.d;
                //settings.success(result);
                settings.success(_biometric_auth_ajax.toJson(value.d));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var last_error = { jqXHR: jqXHR, textStatus: textStatus, errorThrown: errorThrown };
                //Ajax.last_error = last_error;
                if (last_error.errorThrown == "timeout") {
                    settings.error(last_error.errorThrown);
                } else {
                    log(jqXHR.responseText);
                    //var error = Ajax.toJson(jqXHR.responseText);
                    //error.responseText = error.Message;
                    //settings.error(error);//response);
                    settings.error(JSON.parse(jqXHR.responseText).Message);
                    if (settings.debug) { alert("Error Calling Method \"" + settings.methodName + "\"\n\n+" + jqXHR.responseText); }
                }
            }
        });
        return result;
    }
    , toJson: function (jsonString) {
        ////if (jsonString == "") {        
        ////    return {};
        ////}
        //log('toJson:' + jsonString);
        //g_jsonString = jsonString;
        //jsonString = Ajax.parseText(jsonString);//escapeNewLineChars(jsonString);
        //log("replaced: " + jsonString);
        //return eval('(' + jsonString + ')');

        g_jsonString = jsonString;
        jsonString = _biometric_auth_ajax.escapeNewLineChars(jsonString);
        log("replaced: " + jsonString);
        return eval('(' + jsonString + ')');
    }
    , escapeNewLineChars: function (valueToEscape) {
        if (valueToEscape != null && valueToEscape != "") {
            return valueToEscape.replace(/(\r\n|\n|\r)/gm, "\\n"); //valueToEscape.replace(/\n/g, "\\n");
        } else {
            return valueToEscape;
        }
    }
};
$(function () {
    //_biometric_auth.init();
});
function log(t) { }
//</biometric authentication>

//<pwd-hashing>
$(function () { 
    $("#ctl00_ContentPlaceHolder1_Login1_LoginButton").click(function () {		
        if (!(typeof (RC4) === "undefined")) {
            var rc4EncryptValue = RC4.rc4Encrypt($("#ctl00_ContentPlaceHolder1_ux_rc4_rnd_key").val(), $("#ctl00_ContentPlaceHolder1_Login1_Password").val());
            $("#ctl00_ContentPlaceHolder1_Login1_Password").val(RC4.encode64(rc4EncryptValue));
        }
    });
});
//<pwd-hashing>
//<__avoid_copy_paste>
$(function () {
    __avoid_copy_paste_autocomplete.init($('#ctl00_ContentPlaceHolder1_Login1_UserName'));
    __avoid_copy_paste_autocomplete.init($('#ctl00_ContentPlaceHolder1_Login1_Password'));
});
//</__avoid_copy_paste>
//<redirect logged in user to home page>
$(function () {
    if (window.location.href.toLocaleLowerCase().indexOf("login.aspx") > -1
        && $("#ctl00_uxUserName").text() != "Guest"
        && $("#ctl00_uxUserName").text() != "") {
        alert("User already logged in.");
        window.location = getAppPath() + "/authentication/mainmenu.aspx";
    }
})
//</redirect logged in user to home page>
$(function () {
    var lang = "";
    document.cookie.split(";").map(function (item) {
        if (item.split("=")[0].indexOf("cbs_app_lang_cookie") > -1) {
            lang = item.split("=")[1];
        }
    });
    if (lang !== "") {
        $("#uxlang").val(lang);
    }
    //
    $("#uxlang").change(function () {
        var lang = $("#uxlang").val();
        var expDate = new Date();
        expDate.setDate(expDate.getDate() + (365 * 5)); // Expiration 20 days from today
        document.cookie = "cbs_app_lang_cookie=" + lang + "; expires=" + expDate.toUTCString() + "; path=/";
        window.location.reload();
    });
});
