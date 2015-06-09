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

            // user login and logout
            $("#loginlink").on("click", function () {
                ga('send', 'event', 'user', 'login', 'headertopbar');
            });

            $("#logoutlink").on("click", function () {
                ga('send', 'event', 'user', 'logout', 'headertopbar');
            });

            // newsletter singups
            $("#newslettersubscribetopbar").on("click", function () {
                ga('send', 'event', 'newsletter', 'subscribe', 'headertopbar');
            });

            $("#newslettersubscribefooter").on("click", function () {
                ga('send', 'event', 'newsletter', 'subscribe', 'footer');
            });
        });

        // The DOM may not be ready
    })
);
