import {defineConfig} from 'vite';
import vue from '@vitejs/plugin-vue';
import laravel from "laravel-vite-plugin";
import path from "path";
import {mkdirSync} from "fs";

const outDir = "wwwroot/build";

mkdirSync(outDir, {recursive: true});

export default defineConfig({
    plugins: [
        laravel({
            input: ["Frontend/js/app.js"],
            publicDirectory: outDir,
            refresh: true,
        }),
        vue({
            template: {
                transformAssetUrls: {
                    base: null,
                    includeAbsolute: false,
                },
            },
        }),
    ],
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "Frontend/js/"),
        },
    },
    build: {
        outDir,
        emptyOutDir: true,
    },
});