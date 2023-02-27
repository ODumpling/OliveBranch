const mix = require('laravel-mix')

mix.js("Resources/js/app.js", "js")
    .vue({version: 3})
    .postCss("Resources/css/app.css", "css", [
        require("tailwindcss"),
    ])
    .alias({'@': 'resources/js'})
    .setPublicPath('wwwroot')
    .disableNotifications();