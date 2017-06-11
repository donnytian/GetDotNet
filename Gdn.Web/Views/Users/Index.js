(function() {
    $(function() {

        var _userService = abp.services.app.user;
        var _$modal = $('#UserCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            abp.ui.setBusy(_$modal);
            var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            _userService.createUser(user).done(function () {
                // _$modal.find("[data-dismiss=modal]:first").click();
                _$modal.modal('hide');
                // Model hide animation will takes some time, we have to reload until it is hidden(in its hidden event).
                _$modal.on('hidden.bs.modal', function () {
                    $('body').data('lobiAdmin').reload();
                });
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });

        });
        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });
    });
})();