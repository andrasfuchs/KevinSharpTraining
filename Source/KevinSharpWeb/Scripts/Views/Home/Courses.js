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

                ga('send', 'event', 'user', 'login', 'coursewarninglink');
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

                function getCourseTimeSlots(courseCode) {
                    var dynamicData = {};
                    dynamicData["courseCode"] = courseCode;
                    return $.ajax({
                        url: urlGetCourseTimeSlots,
                        type: "POST",
                        dataType: 'json',
                        data: dynamicData
                    })
                }

                getCourseTimeSlots("CS02").done(function (data) {
                    $("#progresscourse>label").text(data);
                })

                $("#schedulepanel").removeClass("optiondisabled");
                $("#progressschedule>a").trigger("click");

                ga('send', 'event', 'courses', 'courseselected', 'cs02', 2);
            });

            // schedule clicks
            $("div.dateselection>select").on("change", function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;

                $("#progressschedule>label").text(valueSelected);

                $("#billingpanel").removeClass("optiondisabled");
                $("#progressbilling>a").trigger("click");

                ga('send', 'event', 'courses', 'dateselected', this.value);
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

                ga('send', 'event', 'courses', 'billingcontinue');
            });


            // payment
            $("#paymentmethods input").on("change", function () {
                $("#progresspayment>label").text(this.value);

                ga('send', 'event', 'courses', 'paymentmethod', this.value);
            });

            $("#paymentcontinue").on("click", function () {
                $("#reminderspanel").removeClass("optiondisabled");
                $("#progressreminders>a").trigger("click");

                $("#thankyou").show();

                ga('send', 'event', 'courses', 'paymentcontinue', $("#paymenttotal")[0].innerText);
            });

            // reminders
            $("#reminderscontinue").on("click", function () {
                $("#progressreminders>a").trigger("click");

                ga('send', 'event', 'courses', 'reminderscontinue');
            });
        });

        // The DOM may not be ready
    })
);
