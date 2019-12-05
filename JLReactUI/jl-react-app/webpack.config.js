const webpack = require('webpack');
module.exports = {
  entry: ['./src/index.js'],
  output: {
    path: __dirname,
    publicPath: '/',
    filename: 'bundle.js'
  },
    plugins: [
        new webpack.DefinePlugin({
            HOST:'https://localhost:44327',
            FILMS:'/api/Films'
          })
    ]  
};