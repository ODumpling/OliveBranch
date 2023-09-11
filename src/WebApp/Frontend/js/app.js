import '../css/app.css';

import {createApp, h} from "vue";
import {createInertiaApp, Head, Link} from "@inertiajs/vue3";
import {resolvePageComponent} from "laravel-vite-plugin/inertia-helpers";

const appName = 'OliveBranch';

createInertiaApp({
    title: (title) => `${title} - ${appName}`,
    resolve: (name) => resolvePageComponent(`./Pages/${name}.vue`, import.meta.glob('./Pages/**/*.vue')),
    setup({ el, App, props, plugin }) {
        return createApp({ render: () => h(App, props) })
            .use(plugin)
            .component('Head', Head)
            .component('Link', Link)
            .mount(el);
    },
    progress: {
        color: '#4B5563',
    },
});