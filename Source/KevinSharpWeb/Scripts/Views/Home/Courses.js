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
            var selectedCourseCode = "";
            var selectedTimeSlotGroupCode = "";

            $("#loginwarninglink").on("click", function () {
                $("#loginlink").trigger("click");

                addSessionEvent('user', 'login', 'coursewarninglink');
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
            $("#csfundamentals").on("click", function (e) {
                selectedCourseCode = e.target.attributes["data-coursecode"].value;
                $("#progresscourse>label").text(e.target.attributes["data-coursename"].value);

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

                getCourseTimeSlots(selectedCourseCode).done(function (data) {
                    var efficient = data[0];
                    var intense = data[1];

                    $('#efficientdates').empty();
                    $('#efficientdates').append($("<option></option>").attr("value", "N/A").text("Select your prefered date"));
                    $.each(efficient, function (key, value) {
                        $('#efficientdates')
                            .append($("<option></option>")
                            .attr("value", value[0])
                            .text(value[1]));
                    });

                    $('#intensedates').empty();
                    $('#intensedates').append($("<option></option>").attr("value", "N/A").text("Select your prefered date"));
                    $.each(intense, function (key, value) {
                        $('#intensedates')
                            .append($("<option></option>")
                            .attr("value", value[0])
                            .text(value[1]));
                    });
                })

                $("#schedulepanel").removeClass("optiondisabled");
                $("#progressschedule>a").trigger("click");

                addSessionEvent('courses', 'courseselected', selectedCourseCode, 2);
            });

            // schedule clicks
            $("div.dateselection>select").on("change", function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;
                selectedTimeSlotGroupCode = valueSelected;

                $("#progressschedule>label").text(valueSelected);

                $("#billingpanel").removeClass("optiondisabled");
                $("#progressbilling>a").trigger("click");

                addSessionEvent('courses', 'dateselected', this.value);
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

                addSessionEvent('courses', 'billingcontinue');
            });


            // payment
            $("#paymentmethods input").on("change", function () {
                $("#progresspayment>label").text(this.value);

                addSessionEvent('courses', 'paymentmethod', this.value);
            });

            $("#paymentcontinue").on("click", function () {
                $("#reminderspanel").removeClass("optiondisabled");
                $("#progressreminders>a").trigger("click");

                $("#thankyou").show();

                function orderCompleted(courseCode, timeSlotGroupCode) {
                    var dynamicData = {};
                    dynamicData["courseCode"] = selectedCourseCode,
                    dynamicData["timeSlotGroupCode"] = timeSlotGroupCode;
                    return $.ajax({
                        url: urlOrderCompleted,
                        type: "POST",
                        dataType: 'json',
                        data: dynamicData
                    })
                }

                orderCompleted(selectedCourseCode, selectedTimeSlotGroupCode);

                addSessionEvent('courses', 'paymentcontinue', $("#paymenttotal")[0].innerText);
            });

            // reminders
            $("#reminderscontinue").on("click", function () {
                $("#progressreminders>a").trigger("click");

                addSessionEvent('courses', 'reminderscontinue');
            });
        });

        // The DOM may not be ready
    })
);


function addSessionEvent(category, action, label, value) {
    ga('send', 'event', category, action, label, value);

    var dynamicData = {};
    dynamicData["category"] = category,
    dynamicData["action"] = action,
    dynamicData["label"] = label,
    dynamicData["value"] = value;
    return $.ajax({
        url: urlAddSessionEvent,
        type: "POST",
        dataType: 'json',
        data: dynamicData
    })
};
