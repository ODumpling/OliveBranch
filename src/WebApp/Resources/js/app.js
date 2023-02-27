import {createApp, h} from "vue";
import {createInertiaApp, Head, Link} from "@inertiajs/vue3";
import Layout from "./Shared/Layout.vue";

createInertiaApp({
    resolve: name => {
        let page = require(`./Pages/${name}`).default

        if (page.layout === undefined) {
            page.layout = page.layout || Layout
        }

        return page

    },


    setup({el, App, props, plugin}) {
        createApp({render: () => h(App, props)})
            .use(plugin)
            .component("Link", Link)
            .component("Head", Head)
            .mount(el);
    },

    title: title => `OliveBranch - ${title}`,

    progress: {
        // The delay after which the progress bar will appear
        // during navigation, in milliseconds.
        delay: 250,

        // The color of the progress bar.
        color: '#29d',

        // Whether to include the default NProgress styles.
        includeCSS: true,

        // Whether the NProgress spinner will be shown.
        showSpinner: true,
    },
});

