
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  baseHref: '/',
  criticalCssPlans: [[1,"/styles-RUOCY2X6.css",3799,["","hr","h1","h2","h3","h4","h5","h6","a","b","strong","{font-weight:bolder}","code","kbd","samp","pre","small","sub","sup","table","progress","summary","ol","ul","menu","img","svg","video","canvas","audio","iframe","embed","object","button","input","select","optgroup","textarea"],[["@layer theme{"],["@layer base{"],["@layer utilities{"]],[[10,"@layer theme,base,components,utilities;"],[26,1,0],[21,":root","{--font-sans:-apple-system, BlinkMacSystemFont, \"Segoe UI\", Roboto, \"Helvetica Neue\", \"Noto Sans\", Arial, sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\";--font-mono:ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"Liberation Mono\", \"Courier New\", monospace;--default-font-family:var(--font-sans);--default-mono-font-family:var(--font-mono)}",[0],0],[26,1,1],[21,["*",":after",":before"],"{box-sizing:border-box;margin:0;padding:0;border:0 solid}",[0,0,0],1],[53,"html","{line-height:1.5;-webkit-text-size-adjust:100%;tab-size:4;font-family:var(--default-font-family, -apple-system, BlinkMacSystemFont, \"Segoe UI\", Roboto, \"Helvetica Neue\", \"Noto Sans\", Arial, sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\");font-feature-settings:var(--default-font-feature-settings, normal);font-variation-settings:var(--default-font-variation-settings, normal);-webkit-tap-highlight-color:transparent}",[0],1,["var(--default-font-family, -apple-system, blinkmacsystemfont, \"segoe ui\", roboto, \"helvetica neue\", \"noto sans\", arial, sans-serif, \"apple color emoji\", \"segoe ui emoji\", \"segoe ui symbol\", \"noto color emoji\")"]],[21,2,"{height:0;color:inherit;border-top-width:1px}",[3],1],[21,"abbr:where([title])","{-webkit-text-decoration:underline dotted;text-decoration:underline dotted}",[[0,0,["abbr"],0]],1],[21,[3,4,5,6,7,8],"{font-size:inherit;font-weight:inherit}",[3,3,3,3,3,3],1],[21,9,"{color:inherit;-webkit-text-decoration:inherit;text-decoration:inherit}",[3],1],[21,[10,11],12,[3,3],1],[53,[13,14,15,16],"{font-family:var(--default-mono-font-family, ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"Liberation Mono\", \"Courier New\", monospace);font-feature-settings:var(--default-mono-font-feature-settings, normal);font-variation-settings:var(--default-mono-font-variation-settings, normal);font-size:1em}",[3,3,3,3],1,["var(--default-mono-font-family, ui-monospace, sfmono-regular, menlo, monaco, consolas, \"liberation mono\", \"courier new\", monospace)"]],[21,17,"{font-size:80%}",[3],1],[21,[18,19],"{font-size:75%;line-height:0;position:relative;vertical-align:baseline}",[3,3],1],[21,18,"{bottom:-.25em}",[3],1],[21,19,"{top:-.5em}",[3],1],[21,20,"{text-indent:0;border-color:inherit;border-collapse:collapse}",[3],1],[21,21,"{vertical-align:baseline}",[3],1],[21,22,"{display:list-item}",[3],1],[21,[23,24,25],"{list-style:none}",[3,3,3],1],[21,[26,27,28,29,30,31,32,33],"{display:block;vertical-align:middle}",[3,3,3,3,3,3,3,3],1],[21,[26,28],"{max-width:100%;height:auto}",[3,3],1],[21,[34,35,36,37,38],"{font:inherit;font-feature-settings:inherit;font-variation-settings:inherit;letter-spacing:inherit;color:inherit;border-radius:0;background-color:transparent;opacity:1}",[3,3,3,3,3],1],[21,":where(select:is([multiple],[size])) optgroup",12,[[0,0,[37],0]],1],[21,":where(select:is([multiple],[size])) optgroup option","{padding-inline-start:20px}",[[" ",[[37,0,0,0],["option",0,0,0]]]],1],[21,38,"{resize:vertical}",[3],1],[21,[34,"input:where([type=button],[type=reset],[type=submit])"],"{appearance:button}",[3,[0,0,[35],0]],1],[21,"[hidden]:where(:not([hidden=until-found]))","{display:none!important}",[[0,0,0,["hidden"]]],1],[26,1,2],[21,".static","{position:static}",[1],2],[21,".lowercase","{text-transform:lowercase}",[1],2],[21,".uppercase","{text-transform:uppercase}",[1],2]]]],
  locale: undefined,
  routes: [
  {
    "renderMode": 2,
    "redirectTo": "/login",
    "route": "/"
  },
  {
    "renderMode": 2,
    "route": "/login"
  },
  {
    "renderMode": 2,
    "route": "/register"
  },
  {
    "renderMode": 2,
    "route": "/home"
  }
],
  entryPointToBrowserMapping: undefined,
  assets: {
    'index.csr.html': {size: 1759, hash: 'd04ed4e54d6d3592', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 953, hash: '25550638c1f025ba', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'home/index.html': {size: 4775, hash: '15a6a3615fd6fadc', text: () => import('./assets-chunks/home_index_html.mjs').then(m => m.default)},
    'login/index.html': {size: 4471, hash: '0334453e40851cac', text: () => import('./assets-chunks/login_index_html.mjs').then(m => m.default)},
    'register/index.html': {size: 7375, hash: '21a0032e6daa8160', text: () => import('./assets-chunks/register_index_html.mjs').then(m => m.default)}
  },
};
