$(function () {
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