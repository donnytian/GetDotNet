var abp = abp || {};
(function ($) {
    if (!window.LobiAdminConfig || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.lobiadmin = {
        config: {
            defaultPage: "/"
        },
        routes: {
            // keep routes empty, so the router is not found the same hash is used for page loading.
        }
    };
    /* Override defaults options for lobiadmin *********************/
    $.extend(window.LobiAdminConfig, abp.libs.lobiadmin.config);
    window.LobiAdminRoutes = abp.libs.lobiadmin.routes;
})(jQuery);
