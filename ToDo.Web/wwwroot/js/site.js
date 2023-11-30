(function () {
    "use strict";

    //Init datepickers
    if ($('[mw-datepicker]').length) {
        datepicker('[mw-datepicker]', {
            formatter: function (input, date) {
                var value = getFormattedDate(date);
                input.setAttribute('value', value);
            }
        });
    }

    //Disable form buttons on submit
    $('form').on("submit", function () {
        //Find submit button and disable it           
        $(this).find('[type=submit]').attr("disabled", "disabled");
    });

    //Confirmation dialogs
    $('[mw-open-modal-link]').on("click", function (event) {
        event.preventDefault();
        var link = $(this);

        var href = link.attr('href');

        $.get(href)
            .done(function (html) {
                //Insert modal
                $("body").append(html);

                subscribeOnModalClose();

                $(".conflict-message").text();
            })
            .fail(function (jqXHR, textStatus) {
                if (jqXHR.status === 409) {
                    $(".conflict-message").text("You cannot delete this Agency because it’s already linked with some News.")
                }
            });
    });

    function subscribeOnModalClose() {
        //Find close modal button
        $('[mw-close-modal]').on("click", function () {
            //Remove dialog
            $('[mw-modal-container]').remove()
        });
    }
})();