const proxy = [
  {
    context: '/api',
    target: 'http://127.0.0.1:5000',
    pathRewrite: { '^/api': ''}
  }
];
module.exports = proxy;
