$(function () {
    $("#PostModel_Strain").change(function (event) {
        if ($(this).find("option:selected").html().toLowerCase() == "other") {
            $("#newstrain").show('normal');
        } else {
            $("#newstrain").hide('normal');
            $("#newstrain input").val('');
        }
    });
});