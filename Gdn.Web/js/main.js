(function ($) {

    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

    // Show welcome notification.
    $(document).ready(function () {
        setTimeout(function () {
            //All demo scripts go here
            Lobibox.notify('info', {
                img: '/img/logo/logo.png',
                sound: false,
                position: 'top right',
                delay: 8000,
                showClass: 'fadeInDown',
                title: 'Welcome to GetDotNet.',
                msg: 'GetDotNet is a demo web app based on ASP Boilerplate and LobiAdmin template.'
            });
        }, 3000);

        $(document).on('submit', 'form', function (ev) {
            ev.preventDefault();
        });
    });
})(jQuery);