var url_text_file = window.location.search.replace("?file=", ""); // "text_to_print.txt";
var to_print_file = "c:\\trustbank_print_data.txt";
var log_file = "c:\\trustbank_print_log.txt";
var g_data = null;
var g_c_printer_name = "printer_name";
$(function () {

    if (getCookie(g_c_printer_name) != undefined) {
        $("#printer_name").val(getCookie(g_c_printer_name));
    }

    $(":button#close").click(function () {
        window.close();
    });
    $(":button#print").click(function () {
        dos_print_log(url_text_file);
        if (g_data != null) {
            write_file();
            return;
        }
        $(":button#print").attr("disabled", "disabled");
        var jqxhr = $.get(url_text_file, function (data) {
            g_data = data;
            write_file();
        })
        //.success(function () { alert("second success"); })
        .error(function () { alert("Error in loading file. " + url_text_file); })
        .complete(function () {
            $(":button#print").attr("disabled", false);
        });
    });
});
function write_file() {    
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var s = fso.CreateTextFile(to_print_file, true);
    s.Write(g_data);
    s.Close();
    print_file();
}
function print_file() {
    var printer_name = $("#printer_name").val();
    var launcher = new ActiveXObject("WScript.Shell");
    //launcher.Run("cmd /K CD C:\ & Dir");
    var command_text = "cmd /C print /d:" + printer_name + " " + to_print_file + " > " + log_file;
    dos_print_log(command_text);
    launcher.Run(command_text, 1, true);

    //red log file and show
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var tempFile = fso.OpenTextFile(log_file, 1);
    var line = tempFile.ReadLine();
    alert(line);
    setCookie(g_c_printer_name, printer_name, 365);
}

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}
function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function dos_print_log(text) {
 //   console.log(text);
}