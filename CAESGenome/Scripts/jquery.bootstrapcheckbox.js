/*
    Checkbox replacement, using bootstrap styling buttons
*/

(function($) {

    $.widget("ui.bootstrapcheckbox", {
        options: {
            affirmativeTxt: "Yes",
            negativeTxt: "No",
            showIcons: true,
            affirmativeIcon: "icon-ok-circle",
            negativeIcon: "icon-remove-circle"
        },
        _create: function () {

            var $cb = $(this.element).hide();
            var $btn = $('<a>').attr('href', '#').addClass('btn');

            // initially checked
            if ($cb.attr('checked')) {
                $btn.addClass('btn-success').html(this.options.affirmativeTxt);
            }
            // initially not checked
            else {
                $btn.addClass('btn-danger').html(this.options.negativeTxt);
            }

            $btn.insertAfter($cb);

            this._attachClick($btn);
        },
        _attachClick: function ($btn) {

            var base = this;

            $btn.click(function (event) {
                // currently active
                if ($(this).hasClass('btn-success')) {
                    $(this).html(base.options.negativeTxt);
                    $(this).siblings('input').removeAttr('checked');
                }
                    // currently inactive
                else {
                    $(this).html(base.options.affirmativeTxt);
                    $(this).siblings('input').attr('checked', 'checked');
                }

                $(this).toggleClass('btn-success');
                $(this).toggleClass('btn-danger');

                event.preventDefault();
            });
        }
    });

})(jQuery);