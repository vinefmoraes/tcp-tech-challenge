const PROXY_CONFIG = {
    "/api-tcp/*": {
      "target": "https://localhost:44345/",
      "secure": false,
      "logLevel": "debug",
      "changeOrigin": true,
      "pathRewrite": {
        "^/api-tcp": ""
      }
    }
}
module.exports = PROXY_CONFIG;