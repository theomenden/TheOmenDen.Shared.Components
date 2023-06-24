const path = require("path");
const TerserPlugin = require('terser-webpack-plugin');
const CompressionPlugin = require('compression-webpack-plugin');
module.exports = env => {
    return {
        entry: {
            "omenjs.min": "./index.ts"
        },
        devtool: env?.production ? "none" : "source-map",
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: "ts-loader",
                    exclude: /node_modules/
                }
            ]
        },
        resolve: {
            extensions: [".tsx", ".ts", ".js", "..."]
        },
        plugins: [
            new CompressionPlugin()
        ],
        optimization: {
            minimize: true,
            minimizer: [
                new TerserPlugin({
                    parallel: true,
                    terserOptions: {
                        compress: true,
                        ecma: 2022,
                        mangle: true,
                        module: true,
                    }
                })
            ]
        },
        output: {
            filename: "[name].js"
        }
    }
};