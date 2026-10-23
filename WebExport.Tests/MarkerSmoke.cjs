// Run after generating WEBEXPORT_MARKER_SMOKE_PATH with the headless tests. Requires Playwright.
const {chromium}=require('playwright');
const {pathToFileURL}=require('node:url');
(async()=>{
 const browser=await chromium.launch({headless:true,...(process.env.WEBEXPORT_BROWSER_PATH ? {executablePath:process.env.WEBEXPORT_BROWSER_PATH} : {})});
 try {
  const page=await browser.newPage(); const errors=[];
  page.on('pageerror',e=>errors.push(e.message));
  await page.goto(pathToFileURL(process.argv[2]).href);
  await page.waitForFunction(()=>document.querySelectorAll('.scatterlayer .point').length===2);
  const markers=await page.locator('.scatterlayer .point').evaluateAll(ps=>ps.map(p=>({width:p.getBBox().width,fill:getComputedStyle(p).fillOpacity,stroke:getComputedStyle(p).stroke,border:getComputedStyle(p).strokeWidth})));
  if(errors.length || markers.some(m=>Math.abs(m.width-17)>0.01||m.fill!=='0'||m.stroke!=='rgb(255, 0, 0)'||m.border!=='3px'))throw new Error(JSON.stringify({errors,markers}));
  console.log('PASS: both hollow circle markers render at 17px with transparent fill and 3px red borders; no JavaScript errors.');
 } finally{await browser.close();}
})().catch(e=>{console.error(e);process.exitCode=1;});
