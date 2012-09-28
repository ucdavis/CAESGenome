$(function () {
    // changing of plate types
    $("#platetypes input").change(function () {
        if ($(this).val() == qtray) {
            $("#numQTrays").show('normal');
            $("#concentration").hide('normal');
            $("#concentration input").val('');
        }
        else if ($(this).val() == glycerol) {
            $("#numQTrays").hide('normal');
            $("#numQTrays input").val('');
            $("#concentration").show('normal');
        } else {
            $("#numQTrays").hide('normal');
            $("#numQTrays input").val('');

            $("#concentration").hide('normal');
            $("#concentration input").val('');
        }
    });
});