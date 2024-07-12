const path = require('path');

module.exports = {
    mode: 'development',
    entry: {
        visits: './React Components/visitsComponent.js',
        visitsEdit: './React Components/visitsEditComponent.js'
    },
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].js', // This will generate visits.js and visitsEdit.js
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                },
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader'],
            },
        ],
    },
    resolve: {
        extensions: ['.js', '.jsx'],
    },
    externals: {
        react: 'React',
        'react-dom': 'ReactDOM',
    },
};
