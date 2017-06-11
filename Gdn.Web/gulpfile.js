/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var concat = require('gulp-concat');
var cssmin = require('gulp-cssmin');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');

var paths = {
    webroot: "./"
};

paths.jsDest = paths.webroot + "js";
paths.cssDest = paths.webroot + "css";
paths.imgDest = paths.webroot + "images";

// json2
paths.json2 = "./Scripts/json2.min.js";

// jQuery
paths.jQuery = "./scripts/jquery-3.1.1.min.js";
paths.jQueryUI = "./scripts/jquery-ui-1.12.1.min.js";
paths.jQueryValidate = "./scripts/jquery.validate.min.js";

// Bootstrap
paths.bootstrapCss = "./content/bootstrap.css";
paths.bootstrapJs = "./scripts/bootstrap.min.js";

// Admin LTE
paths.adminLteCss = "./content/adminlte/AdminLTE.css";
paths.adminLteSkinBlueCss = "./content/adminlte/skin-blue.css";
paths.adminLteJs = "./scripts/adminlte.js";

// Font Awesome
paths.faCss = "./content/font-awesome.min.css";

// iCheck
paths.iCheckCss = "./node_modules/admin-lte/plugins/iCheck/square/blue.css";
paths.iCheckJs = "./node_modules/admin-lte/plugins/iCheck/icheck.min.js";

// Fast Click
paths.fastClickJs = "./node_modules/admin-lte/plugins/fastclick/fastclick.js";

// Slim Scroll
paths.slimScrollJs = "./node_modules/admin-lte/plugins/slimScroll/jquery.slimscroll.min.js";

// jVectorMap
paths.jVectorMapCss = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-1.2.2.css";
paths.jVectorMapJs = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js";
paths.jVectorMapWorldJs = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-world-mill-en.js";

// Spark Line
paths.sparkLineJs = "./node_modules/admin-lte/plugins/sparkline/jquery.sparkline.min.js";

// Chart js
paths.chartjsJs = "./node_modules/admin-lte/plugins/chartjs/Chart.min.js";

// css included in the layout page
paths.commonCss = [
    paths.bootstrapCss
    , paths.faCss
    , "./content/lobiadmin/animate.css"
    , "./content/lobiadmin/weather-icons.min.css"
    , "./content/lobiadmin/plugins/lobibox.css"
    , "./content/lobiadmin/plugins/lobilist.css"
    , "./content/lobiadmin/plugins/lobipanel.css"
    , "./content/lobiadmin/lobiadmin.css"
    , "./content/site.css"
];

// css included in simple page such as login
paths.basicCss = [
    paths.bootstrapCss
    , paths.faCss
    , "./content/lobiadmin/plugins/lobibox.css"
    , "./content/lobiadmin/animate.css"
    , "./content/site.css"
];

// js included in the top of layout page
paths.commonJsTop = [
    "./Abp/Framework/scripts/utils/ie10fix.js"
];

// js included in the bottom of layout page
paths.commonJsBottom = [paths.json2
    , paths.jQuery
    , paths.jQueryUI
    , paths.jQueryValidate
    , paths.bootstrapJs
    , paths.fastClickJs
    , paths.slimScrollJs
    , "./Scripts/moment-with-locales.min.js"
    , "./js/lobiadmin/plugins/lobibox.js"
    , "./js/lobiadmin/plugins/lobilist.min.js"
    , "./js/lobiadmin/plugins/lobipanel.min.js"
    , "./js/lobiadmin/config.js"                  //Make sure that config.js file is loaded before LobiAdmin.js
    , "./js/lobiadmin/lobiadmin.js"
    , "./js/lobiadmin/app.js"

    , "./Scripts/jquery.blockUI.js"
    , "./Scripts/others/spinjs/spin.js"
    , "./Scripts/others/spinjs/jquery.spin.js"
    , "./Abp/Framework/scripts/abp.js",
    , "./Abp/Framework/scripts/libs/abp.jquery.js",
    , "./Abp/Framework/scripts/libs/abp.blockUI.js",
    , "./Abp/Framework/scripts/libs/abp.spin.js",
    , "./Abp/Framework/scripts/libs/abp.lobibox.js"
    , "./Abp/Framework/scripts/libs/abp.lobiadmin.js"
    , "./js/main.js"
];

// js included in simple page such as login.
paths.basicJs = [paths.json2
    , paths.jQuery
    , paths.jQueryUI
    , paths.jQueryValidate
    , paths.bootstrapJs
    , "./Scripts/moment-with-locales.min.js"
    , "./js/lobiadmin/plugins/lobibox.js"
    , "./Scripts/jquery.blockUI.js",
    , "./Scripts/others/spinjs/spin.js"
    , "./Scripts/others/spinjs/jquery.spin.js"
    , "./Abp/Framework/scripts/abp.js",
    , "./Abp/Framework/scripts/libs/abp.jquery.js",
    , "./Abp/Framework/scripts/libs/abp.blockUI.js",
    , "./Abp/Framework/scripts/libs/abp.spin.js",
    , "./Abp/Framework/scripts/libs/abp.lobibox.js"
];

// other package bundles for specific page
// to be here...

paths.forDashboard = [
    , "./js/lobiadmin/plugins/chart.js"
    , "./js/lobiadmin/plugins/jquery.sparkline.js"
    , "./js/lobiadmin/plugins/fullcalendar.js"
];

// tasks
gulp.task("min:basicJs", function () {
    gulp.src(paths.basicJs)
        .pipe(concat(paths.jsDest + "/basic.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:commonJs", function () {
    gulp.src(paths.commonJsTop)
        .pipe(concat(paths.jsDest + "/commonTop.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));

    gulp.src(paths.commonJsBottom)
        .pipe(concat(paths.jsDest + "/commonBottom.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:viewsJs", function () {
    gulp.src("./views/**/*.js")
        .pipe(rename({ suffix: ".min" }))
        .pipe(uglify())
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task("min:basicCss", function () {
    gulp.src(paths.basicCss)
        .pipe(concat(paths.cssDest + "/basic.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));

    // Specially for login.css since it is large.
    gulp.src("./views/account/login.css")
        .pipe(rename({ suffix: ".min" }))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.cssDest));
});

gulp.task("min:commonCss", function () {
    gulp.src(paths.commonCss)
        .pipe(concat(paths.cssDest + "/common.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:viewsCss", function () {
    // Install Web Compiler extension in VS, it will compile and minify for us, we only copy to css folder...
    gulp.src("./views/**/*.min.css")
        //.pipe(rename({ suffix: ".min" }))
        //.pipe(cssmin())
        .pipe(gulp.dest(paths.cssDest));
});

gulp.task("min:misc", function () {
    gulp.src(paths.forDashboard)
        .pipe(concat(paths.jsDest + "/dashboard.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("CopyImagesFromAdminLte", function () {
    gulp.src("./node_modules/admin-lte/dist/img/**/*")
        .pipe(gulp.dest(paths.imgDest));
});

gulp.task("min", ["min:basicJs", "min:commonJs", "min:viewsJs", "min:basicCss", "min:commonCss", "min:viewsCss", "min:misc"]);

gulp.task("default", function () {
    // place code for your default task here
});