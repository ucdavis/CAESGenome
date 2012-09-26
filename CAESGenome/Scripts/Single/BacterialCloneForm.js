$(function () {
    $("#PostModel_Strain").change(function (event) {
        if ($(this).find("option:selected").html() == "other") {
            $("#otherHost").show('normal');
        } else {
            $("#otherHost").hide('normal');
            $("#PostModel_NewStrain").val('');
            $("#PostModel_Bacteria").val('-1');
        }
    });

    // show/hide sequence direction
    $("#sequencedirections input").change(function () {
        if ($(this).val() == sequenceDirectionTwo) {
            $("#primer2").show('normal');
        } else {
            $("#primer2").val('');
            $("#primer2").hide('normal');
        }
    });
});