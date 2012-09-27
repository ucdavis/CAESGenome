$(function () {
    $("#samples input").change(function () {
        if ($(this).val() == bac) {
            $("#bacvector").show('normal');

            $("#concentration input").val('');
            $("#concentration").hide('normal');
        }
        else if ($(this).val() == dna) {
            $("#concentration").show('normal');

            $("#bacvector input").val('');
            $("#bacvector").hide('normal');
        } else {
            $("#concentration").hide('normal');
            $("#bacvector").hide('normal');
        }
    });
});