var abp = abp || {};
(function ($) {
    if (!Lobibox || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.lobibox = {
        config: {
            messageboxes: {
                'default': {
                    iconSource: 'fontAwesome',
                    draggable: false
                },
                options: {
                    buttons: {
                        ok: {
                            'class': 'lobibox-btn lobibox-btn-default',
                            text: 'OK',
                            closeOnClick: true
                        },
                        cancel: {
                            'class': 'lobibox-btn lobibox-btn-cancel',
                            text: 'Cancel',
                            closeOnClick: true
                        },
                        yes: {
                            'class': 'lobibox-btn lobibox-btn-yes',
                            text: 'Yes',
                            closeOnClick: true
                        },
                        no: {
                            'class': 'lobibox-btn lobibox-btn-no',
                            text: 'No',
                            closeOnClick: true
                        }
                    }
                },
                confirm: {
                    title: 'Are you sure?'
                }
            },
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            }
        }
    };
    /* Override defaults options for lobibox *********************/
    Lobibox.base.DEFAULTS = $.extend({}, Lobibox.base.DEFAULTS, abp.libs.lobibox.config.messageboxes.default);


    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {

        return $.Deferred(function ($dfd) {
            Lobibox.alert(type,
                {
                    title: title,
                    msg: message,
                    shown: function (lobibox) {// TODO centralizer the confirm modal.
                        //lobibox.$el.css('margin', '0 auto');
                        //lobibox.$el.css('left', '50%').css('margin-left', '-' + (lobibox.$el.width() / 2) + 'px');
                    },
                    callback: function () {
                        $dfd.resolve();
                    }
                });
        });
    };

    abp.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    abp.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    abp.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    abp.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    abp.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            msg: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var isConfirmed = false;
        var opts = $.extend({}, userOpts,
            {
                callback: function (lobibox, type) {
                    if (type === 'no') {
                        isConfirmed = false;
                    } else if (type === 'yes') {
                        isConfirmed = true;
                    } else if (type === 'ok') {
                        isConfirmed = true;
                    } else if (type === 'cancel') {
                        isConfirmed = false;
                    }
                    callback && callback(isConfirmed);
                }
            });

        return $.Deferred(function ($dfd) {
            Lobibox.confirm(opts);
            $dfd.resolve(isConfirmed);
        });
    };

    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.libs.lobibox.config.messageboxes.confirm.title = abp.localization.abpWeb('AreYouSure');
        abp.libs.lobibox.config.messageboxes.options.buttons.no.text = abp.localization.abpWeb('No');
        abp.libs.lobibox.config.messageboxes.options.buttons.yes.text = abp.localization.abpWeb('Yes');
        abp.libs.lobibox.config.messageboxes.options.buttons.cancel.text = abp.localization.abpWeb('Cancel');
        abp.libs.lobibox.config.messageboxes.options.buttons.ok.text = abp.localization.abpWeb('OK');

        // Updates L10N strings to lobibox.
        Lobibox.confirm.DEFAULTS = $.extend({}, Lobibox.confirm.DEFAULTS, abp.libs.lobibox.config.messageboxes.confirm);
        Lobibox.base.OPTIONS = $.extend({}, Lobibox.base.OPTIONS, abp.libs.lobibox.config.messageboxes.options);
    });

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        var opts = $.extend({},
            {
                title: title,
                msg: message
            }, options);

        Lobibox.notify(type, opts);
    };

    abp.notify.success = function (message, title, options) {
        showNotification('success', message, title, options);
    };

    abp.notify.info = function (message, title, options) {
        showNotification('info', message, title, options);
    };

    abp.notify.warn = function (message, title, options) {
        showNotification('warning', message, title, options);
    };

    abp.notify.error = function (message, title, options) {
        showNotification('error', message, title, options);
    };

})(jQuery);
