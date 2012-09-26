$(function () {
    $("#PostModel_NumPlates").change(function (event) {
        var count = $("#plate-container").children().length;
        var value = $(this).val();

        // decrease the # of slots
        if (count > value) {
            var numToRemove = count - value;
            for (var i = 0; i < numToRemove; i++) {
                $("#plate-container input:last-child").remove();
            }
        }
            // increase the # of slots                    
        else if (count < value) {
            var numToAdd = value - count;
            for (var i = 0; i < numToAdd; i++) {
                var tb = $("<input>").attr("type", "text").attr("id", "PostModel_PlateNames").attr("name", "PostModel.PlateNames");
                $("#plate-container").append(tb);
            }
        }
    });
});