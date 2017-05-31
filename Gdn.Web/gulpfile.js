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

// jQuery
paths.jQuery = "./scripts/jquery-3.1.1.min.js";

// Bootstrap
paths.bootstrapCss = "./content/bootstrap.css";
paths.bootstrapJs = "./scripts/bootstrap.min.js";

// Admin LTE
paths.adminLteCss = "./node_modules/admin-lte/dist/css/AdminLTE.css";
paths.adminLteSkinBlueCss = "./node_modules/admin-lte/dist/css/skins/skin-blue.css";
paths.adminLteJs = "./node_modules/admin-lte/dist/js/app.js";

// Font Awesome
paths.faCss = "./content/font-awesome.min.css";

// iCheck
paths.iCheckCss = "./node_modules/admin-lte/plugins/iCheck/square/blue.css";
paths.iCheckJs = "./node_modules/admin-lte/plugins/iCheck/icheck.min.js";

// jVectorMap
paths.jVectorMapCss = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-1.2.2.css";
paths.jVectorMapJs = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js";
paths.jVectorMapWorldJs = "./node_modules/admin-lte/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"

// Fast Click
paths.fastClickJs = "./node_modules/admin-lte/plugins/fastclick/fastclick.min.js";

// Spark Line
paths.sparkLineJs = "./node_modules/admin-lte/plugins/sparkline/jquery.sparkline.min.js";

// Slim Scroll
paths.slimScrollJs = "./node_modules/admin-lte/plugins/slimScroll/jquery.slimscroll.min.js";

// Chart js
paths.chartjsJs = "./node_modules/admin-lte/plugins/chartjs/Chart.min.js";

// css included in the layout page
paths.commonCss = [
    paths.bootstrapCss
    , paths.faCss
    , paths.jVectorMapCss
    , paths.adminLteCss
    , paths.adminLteSkinBlueCss
    , paths.iCheckCss
];

// css included in simple page such as login
paths.basicCss = [
    paths.bootstrapCss
    , paths.faCss
    , paths.adminLteCss
    , paths.iCheckCss
];

// js included in the top of layout page
paths.commonJsTop = [
    "./Abp/Framework/scripts/utils/ie10fix.js"
];

// js included in the bottom of layout page
paths.commonJsBottom = [
    paths.jQuery
    , paths.bootstrapJs
    , paths.fastClickJs
    , paths.adminLteJs
    , paths.sparkLineJs
    , paths.jVectorMapJs
    , paths.jVectorMapWorldJs
    , paths.slimScrollJs
    , paths.chartjsJs
    , paths.iCheckJs
    , "./node_modules/admin-lte/dist/js/pages/dashboard2.js" //for demo purposes
    , "./node_modules/admin-lte/dist/js/demo.js" //for demo purposes
];

paths.basicJs = [
    paths.jQuery
    , paths.bootstrapJs
    , paths.iCheckJs
];

// other package bundles
// to be here...

// tasks
gulp.task("min:basicJs", function () {
    gulp.src(paths.basicJs)
        .pipe(concat(paths.jsDest + "/basic.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:commonJs", function () {
    gulp.src(paths.commonJsTop)
        .pipe(concat(paths.jsDest + "/commonJsTop.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));

    gulp.src(paths.commonJsBottom)
        .pipe(concat(paths.jsDest + "/commonJsBottom.min.js"))
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

gulp.task("CopyImagesFromAdminLte", function () {
    gulp.src("./node_modules/admin-lte/dist/img/**/*")
        .pipe(gulp.dest(paths.imgDest));
});

gulp.task("min", ["min:basicJs", "min:commonJs", "min:viewsJs", "min:basicCss", "min:commonCss", "min:viewsCss"]);

gulp.task("default", function () {
    // place code for your default task here
});