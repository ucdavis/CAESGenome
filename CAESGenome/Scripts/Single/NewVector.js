$(function () {
    $("#PostModel_Vector").change(function (event) {
        if ($(this).find("option:selected").html().toLowerCase() == "other") {
            $("#newvector").show('normal');
        } else {
            $("#newvector").hide('normal');
            $("#newvector input").val('');
        }
    });
});