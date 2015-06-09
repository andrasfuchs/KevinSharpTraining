// IIFE - Immediately Invoked Function Expression
(function (yourcode) {

    // The global jQuery object is passed as a parameter
    yourcode(window.jQuery, window, document);

}
    (function ($, window, document) {

        // The $ is now locally scoped 

        // Listen for the jQuery ready event on the document
        $(function () {
            // The DOM is ready!


            $("#loginwarninglink").on("click", function () {
                $("#loginlink").trigger("click");
            });

            // edit buttons
            $("#progresscourse>a").on("click", "", function () {
                $("#coursespanel>a").trigger("click");

                $("ul.progress-list li").removeClass("active");
                $("#progresscourse").addClass("active");
            });

            $("#progressschedule>a").on("click", "", function () {
                $("#schedulepanel>a").trigger("click");

                $("ul.progress-list li").removeClass("active");
                $("#progressschedule").addClass("active");
            });

            $("#progressbilling>a").on("click", "", function () {
                $("#billingpanel>a").trigger("click");

                $("ul.progress-list li").removeClass("active");
                $("#progressbilling").addClass("active");
            });

            $("#progresspayment>a").on("click", "", function () {
                $("#paymentpanel>a").trigger("click");

                $("ul.progress-list li").removeClass("active");
                $("#progresspayment").addClass("active");
            });

            $("#progressreminders>a").on("click", "", function () {
                $("#reminderspanel>a").trigger("click");

                $("ul.progress-list li").removeClass("active");
                $("#progressreminders").addClass("active");
            });

            // course clicks
            $("#csfundamentals").on("click", function () {
                $("#progresscourse>label").text("C# Fundamentals");

                $("#schedulepanel").removeClass("optiondisabled");
                $("#progressschedule>a").trigger("click");
            });

            // schedule clicks
            $("div.dateselection>select").on("change", function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;

                $("#progressschedule>label").text(valueSelected);

                $("#billingpanel").removeClass("optiondisabled");
                $("#progressbilling>a").trigger("click");
            });

            // billing continue
            $("#billingcontinue").on("click", function () {
                if ($("#billingcompany")[0].value == "") {
                    $("#progressbilling>label").text($("#billingfirstname")[0].value + " " + $("#billinglastname")[0].value);
                } else {
                    $("#progressbilling>label").text($("#billingcompany")[0].value);
                }

                $("#paymentpanel").removeClass("optiondisabled");
                $("#progresspayment>a").trigger("click");
            });
        });

        // The DOM may not be ready
    })
);
